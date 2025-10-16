using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private sealed record RouteHint(string Path, HashSet<string> QueryParameters);

    private void AnalyzeHttpClient(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        var fieldLookup = fieldTypes.ToDictionary(pair => pair.Key.TrimStart('_'), pair => pair.Value, StringComparer.OrdinalIgnoreCase);
        var httpClientFieldName = fieldLookup.FirstOrDefault(kvp => kvp.Value.Type.Contains("HttpClient", StringComparison.Ordinal)).Key;
        var httpClientField = string.IsNullOrWhiteSpace(httpClientFieldName)
            ? null
            : httpClientFieldName.TrimStart('_');

        var info = new HttpClientInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className);

        foreach (var method in classDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
            var routeHints = CollectRouteHints(tree, method);

            var declaringMethod = method.Identifier.Text;

            foreach (var invocation in method.DescendantNodes().OfType<InvocationExpressionSyntax>())
            {
                if (httpClientField is not null &&
                    invocation.Expression is MemberAccessExpressionSyntax { Expression: IdentifierNameSyntax identifier } memberAccess &&
                    identifier.Identifier.Text.TrimStart('_') == httpClientField)
                {
                    var methodIdentifier = memberAccess.Name.Identifier.Text;
                    var httpMethod = InferHttpVerb(methodIdentifier);
                    var route = ExtractRouteLiteral(tree, invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression);
                    var line = GetLineNumber(tree, invocation);
                    info.OutboundCalls.Add(new HttpClientCall(declaringMethod, httpMethod, route, line, Array.Empty<string>()));
                    continue;
                }

                if (TryCaptureWrapperHttpCall(tree, invocation, routeHints, declaringMethod, out var wrapperCall))
                {
                    info.OutboundCalls.Add(wrapperCall);
                }
            }
        }

        _httpClients[fqdn] = info;
    }

    private void EmitHttpClients()
    {
        foreach (var client in _httpClients.Values)
        {
            HttpClientBaseAddress? effectiveAddress = null;
            if (_httpClientBaseUrls.TryGetValue(client.Fqdn, out var fqdnAddress))
            {
                effectiveAddress = fqdnAddress;
            }
            else if (_httpClientBaseUrls.TryGetValue(client.Name, out var baseAddress))
            {
                var normalized = baseAddress with { ClientType = client.Fqdn };
                _httpClientBaseUrls[client.Fqdn] = normalized;
                effectiveAddress = normalized;
            }

            // Determine if this client appears to target an external service (has a base address but no explicit mapping)
            var props = new Dictionary<string, object>();
            if (effectiveAddress is not null)
            {
                var hasMapping = _clientTargetServices.ContainsKey(client.Fqdn) || _clientTargetServices.ContainsKey(client.Name);
                if (!hasMapping)
                {
                    props["external"] = true;
                }
            }

            var id = StableId.For("http.client", client.Fqdn, client.Assembly, client.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "http.client",
                Name = client.Name,
                Fqdn = client.Fqdn,
                Assembly = client.Assembly,
                Project = client.Project,
                FilePath = client.FilePath,
                Span = client.Span,
                SymbolId = client.SymbolId,
                Tags = new[] { "integration" },
                Props = props.Count > 0 ? props : null
            };

            if (effectiveAddress is not null)
            {
                EmitHttpClientConfigurationEdge(id, client, effectiveAddress!);
            }

            foreach (var call in client.OutboundCalls)
            {
                _httpCalls.Add(new HttpCallInfo(client, call));
            }
        }
    }

    private void EmitHttpClientConfigurationEdge(string clientId, HttpClientInfo client, HttpClientBaseAddress address)
    {
        if (string.IsNullOrWhiteSpace(address.ConfigurationKey))
        {
            return;
        }

        var usage = new ConfigurationUsage(
            "IConfiguration",
            "httpclient.base_address",
            address.ConfigurationKey,
            address.Line,
            address.SourceFile);

        EmitConfigurationEdges(clientId, new[] { usage });
    }

    private void EmitHttpCalls()
    {
        foreach (var call in _httpCalls)
        {
            // If we couldn't canonicalize the route skip
            var endpointRouteForKey = call.EndpointRoute;
            if (string.IsNullOrWhiteSpace(endpointRouteForKey))
            {
                continue;
            }

            // Remove query string portion for lookup key
            var qmIndex = endpointRouteForKey.IndexOf('?');
            if (qmIndex >= 0)
            {
                endpointRouteForKey = endpointRouteForKey.Substring(0, qmIndex);
            }

            var key = $"{call.Call.HttpMethod}:{endpointRouteForKey}";

            var baseAddress = TryGetHttpClientBaseAddress(call.Client.Fqdn) ?? TryGetHttpClientBaseAddress(call.Client.Name);
            var baseUrl = baseAddress?.BaseUrl;
            var configurationKey = baseAddress?.ConfigurationKey;
            var configuration = baseAddress?.Configuration;

            string? targetService = null;
            if (_clientTargetServices.TryGetValue(call.Client.Fqdn, out var mappedService) ||
                _clientTargetServices.TryGetValue(call.Client.Name, out mappedService))
            {
                targetService = mappedService;
            }

            var clientId = StableId.For("http.client", call.Client.Fqdn, call.Client.Assembly, call.Client.SymbolId);

            if (_minimalEndpoints.TryGetValue(key, out var endpoint))
            {
                var endpointId = StableId.For("endpoint.minimal_api", endpoint.Fqdn, endpoint.Assembly, endpoint.SymbolId);
                var props = CreateHttpCallProps(call, baseUrl, configurationKey, configuration, targetService);

                _edges.Add(new GraphEdge
                {
                    From = clientId,
                    To = endpointId,
                    Kind = "calls",
                    Source = "static",
                    Confidence = 0.9,
                    Transform = new GraphTransform
                    {
                        Type = "httpclient.request",
                        Location = new GraphLocation { File = call.Client.FilePath, Line = call.Call.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(call.Client.FilePath, call.Call.Line)
                });
            }
            else if (_controllerRoutes.TryGetValue(key, out var controllers))
            {
                foreach (var controller in controllers)
                {
                    var controllerId = StableId.For("endpoint.controller", controller.Fqdn, controller.Assembly, controller.SymbolId);
                    var props = CreateHttpCallProps(call, baseUrl, configurationKey, configuration, targetService);

                    _edges.Add(new GraphEdge
                    {
                        From = clientId,
                        To = controllerId,
                        Kind = "calls",
                        Source = "static",
                        Confidence = 0.85,
                        Transform = new GraphTransform
                        {
                            Type = "httpclient.request",
                            Location = new GraphLocation { File = call.Client.FilePath, Line = call.Call.Line }
                        },
                        Props = props,
                        Evidence = CreateEvidence(call.Client.FilePath, call.Call.Line)
                    });
                }
            }
        }
    }

    private static Dictionary<string, object> CreateHttpCallProps(HttpCallInfo call, string? baseUrl, string? configurationKey, ConfigurationValue? configuration, string? targetService)
    {
        var route = call.Call.Route ?? call.EndpointRoute ?? string.Empty;
        var props = new Dictionary<string, object>
        {
            ["verb"] = call.Call.HttpMethod,
            ["route"] = route
        };

        if (!string.IsNullOrWhiteSpace(call.Call.DeclaringMethod))
        {
            props["client_method"] = call.Call.DeclaringMethod;
        }

        if (call.Call.QueryParameters is { Count: > 0 })
        {
            props["query_params"] = call.Call.QueryParameters
                .OrderBy(p => p, StringComparer.OrdinalIgnoreCase)
                .Select(p => $"{p}={{*}}")
                .ToArray();
        }

        if (!string.IsNullOrWhiteSpace(baseUrl))
        {
            props["base_url"] = baseUrl!;
        }

        if (!string.IsNullOrWhiteSpace(configurationKey))
        {
            props["configuration_key"] = configurationKey!;
        }

        if (configuration is not null)
        {
            props["configuration_file"] = configuration.FilePath;
        }

        if (!string.IsNullOrWhiteSpace(targetService))
        {
            props["target_service"] = targetService!;
        }

        return props;
    }

    private static Dictionary<string, RouteHint> CollectRouteHints(SyntaxTree tree, MethodDeclarationSyntax method)
    {
        var hints = new Dictionary<string, RouteHint>(StringComparer.OrdinalIgnoreCase);

        foreach (var local in method.DescendantNodes().OfType<LocalDeclarationStatementSyntax>())
        {
            foreach (var variable in local.Declaration.Variables)
            {
                var initializer = variable.Initializer?.Value;
                if (initializer is null)
                {
                    continue;
                }

                if (TryResolveRouteHint(tree, initializer, hints) is { } hint)
                {
                    hints[variable.Identifier.Text] = hint;
                }
            }
        }

        foreach (var assignment in method.DescendantNodes().OfType<AssignmentExpressionSyntax>())
        {
            if (assignment.Left is IdentifierNameSyntax identifier &&
                TryResolveRouteHint(tree, assignment.Right, hints) is { } hint)
            {
                hints[identifier.Identifier.Text] = hint;
            }
        }

        return hints;
    }

    private static bool TryCaptureWrapperHttpCall(
        SyntaxTree tree,
        InvocationExpressionSyntax invocation,
        IReadOnlyDictionary<string, RouteHint> routeHints,
        string declaringMethod,
        out HttpClientCall call)
    {
        call = null!;
        if (!IsRequestInvocation(invocation.Expression))
        {
            return false;
        }

        var arguments = invocation.ArgumentList.Arguments;
        if (arguments.Count < 2)
        {
            return false;
        }

        var httpMethodExpression = arguments[0].Expression;
        var urlExpression = arguments[1].Expression;
        var httpMethod = TryResolveHttpMethod(httpMethodExpression) ?? httpMethodExpression.ToString().ToUpperInvariant();
        var hint = TryResolveRouteHint(tree, urlExpression, routeHints);
        if (hint is null)
        {
            return false;
        }

        var route = FormatRoute(hint);
        var line = GetLineNumber(tree, invocation);
        call = new HttpClientCall(declaringMethod, httpMethod, route, line, hint.QueryParameters.ToArray());
        return true;
    }

    private static RouteHint? TryResolveRouteHint(SyntaxTree tree, ExpressionSyntax expression, IReadOnlyDictionary<string, RouteHint> routeHints)
    {
        switch (expression)
        {
            case LiteralExpressionSyntax:
            case InterpolatedStringExpressionSyntax:
                return ExtractRouteLiteral(tree, expression) is { } literal
                    ? new RouteHint(literal, new HashSet<string>(StringComparer.OrdinalIgnoreCase))
                    : null;
            case IdentifierNameSyntax identifier when routeHints.TryGetValue(identifier.Identifier.Text, out var value):
                return value;
            case InvocationExpressionSyntax invocation:
                if (TryResolveRouteFromBuilder(tree, invocation) is { } inlineRoute)
                {
                    return inlineRoute;
                }

                if (invocation.Expression is MemberAccessExpressionSyntax member)
                {
                    var methodName = member.Name switch
                    {
                        IdentifierNameSyntax identifierName => identifierName.Identifier.Text,
                        GenericNameSyntax genericName => genericName.Identifier.Text,
                        _ => member.Name.ToString()
                    };

                    if (member.Expression is IdentifierNameSyntax builder &&
                        routeHints.TryGetValue(builder.Identifier.Text, out var builderHint))
                    {
                        if (string.Equals(methodName, "Build", StringComparison.Ordinal))
                        {
                            return builderHint;
                        }

                        if (methodName is "WithRequiredQueryParameter" or "WithOptionalQueryParameter")
                        {
                            var keyExpression = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                            var key = keyExpression is null ? null : ExtractStringValue(keyExpression);
                            return AppendQueryParameter(builderHint, key);
                        }
                    }
                }

                break;
            case ObjectCreationExpressionSyntax creation:
                return TryResolveRouteFromBuilder(tree, creation);
        }

        return null;
    }

    private static RouteHint? TryResolveRouteFromBuilder(SyntaxTree tree, ExpressionSyntax expression)
    {
        var chain = new List<InvocationExpressionSyntax>();
        ExpressionSyntax current = expression;
        while (current is InvocationExpressionSyntax invocation)
        {
            chain.Add(invocation);
            if (invocation.Expression is MemberAccessExpressionSyntax member)
            {
                current = member.Expression;
            }
            else
            {
                break;
            }
        }

        var root = UnwrapFluentCallRoot(current);
        if (root is not ObjectCreationExpressionSyntax creation)
        {
            return null;
        }

        var typeName = creation.Type.ToString();
        if (!typeName.EndsWith("UrlBuilder", StringComparison.Ordinal))
        {
            return null;
        }

        var literal = creation.ArgumentList?.Arguments.FirstOrDefault()?.Expression;
        var route = ExtractRouteLiteral(tree, literal);
        if (string.IsNullOrWhiteSpace(route))
        {
            return null;
        }

        var queryParameters = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        for (var i = chain.Count - 1; i >= 0; i--)
        {
            var invocation = chain[i];
            if (invocation.Expression is not MemberAccessExpressionSyntax member)
            {
                continue;
            }

            var methodName = member.Name.Identifier.Text;
            if (!methodName.StartsWith("With", StringComparison.Ordinal))
            {
                continue;
            }

            if (methodName is "WithRequiredQueryParameter" or "WithOptionalQueryParameter")
            {
                var keyExpression = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                var key = keyExpression is null ? null : ExtractStringValue(keyExpression);
                var normalized = NormalizeQueryKey(key);
                if (!string.IsNullOrWhiteSpace(normalized))
                {
                    queryParameters.Add(normalized);
                }
            }
        }

        if (queryParameters.Count == 0)
        {
            var builderText = expression.ToString();
            foreach (Match match in Regex.Matches(builderText, @"With(?:Required|Optional)QueryParameter\(\s*""([^""]+)"""))
            {
                var name = match.Groups[1].Value;
                var normalized = NormalizeQueryKey(name);
                if (string.IsNullOrWhiteSpace(normalized))
                {
                    continue;
                }

                queryParameters.Add(normalized);
            }
        }

        return new RouteHint(route, queryParameters);
    }

    private static RouteHint AppendQueryParameter(RouteHint hint, string? key)
    {
        var parameters = new HashSet<string>(hint.QueryParameters, StringComparer.OrdinalIgnoreCase);
        var normalized = NormalizeQueryKey(key);
        if (!string.IsNullOrWhiteSpace(normalized))
        {
            parameters.Add(normalized);
        }

        return new RouteHint(hint.Path, parameters);
    }

    private static string FormatRoute(RouteHint hint)
    {
        if (hint.QueryParameters.Count == 0)
        {
            return hint.Path;
        }

        var ordered = hint.QueryParameters
            .OrderBy(p => p, StringComparer.OrdinalIgnoreCase)
            .Select(p => $"{p}={{*}}");

        return $"{hint.Path}?{string.Join("&", ordered)}";
    }

    private static string NormalizeQueryKey(string? key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return "{*}";
        }

        var trimmed = key.Trim();
        return trimmed.Replace(" ", string.Empty);
    }

    private static ExpressionSyntax? UnwrapFluentCallRoot(ExpressionSyntax expression)
    {
        var current = expression;
        while (current is InvocationExpressionSyntax invocation && invocation.Expression is MemberAccessExpressionSyntax member)
        {
            current = member.Expression;
        }

        if (current is ParenthesizedExpressionSyntax parenthesized)
        {
            return UnwrapFluentCallRoot(parenthesized.Expression);
        }

        if (current is CastExpressionSyntax cast)
        {
            return UnwrapFluentCallRoot(cast.Expression);
        }

        return current;
    }

    private static bool IsRequestInvocation(SyntaxNode expression)
        => expression switch
        {
            IdentifierNameSyntax identifier => identifier.Identifier.Text.Equals("Request", StringComparison.Ordinal),
            GenericNameSyntax generic => generic.Identifier.Text.Equals("Request", StringComparison.Ordinal),
            MemberAccessExpressionSyntax member => IsRequestInvocation(member.Name),
            _ => false
        };

    private static string? TryResolveHttpMethod(ExpressionSyntax expression)
    {
        switch (expression)
        {
            case MemberAccessExpressionSyntax { Name: IdentifierNameSyntax name }:
                return name.Identifier.Text.ToUpperInvariant();
            case IdentifierNameSyntax identifier when identifier.Identifier.Text.Equals("HttpMethod", StringComparison.OrdinalIgnoreCase):
                return null;
            case ObjectCreationExpressionSyntax creation when creation.Type.ToString().EndsWith("HttpMethod", StringComparison.Ordinal):
                var argument = creation.ArgumentList?.Arguments.FirstOrDefault()?.Expression;
                var value = argument is null ? null : ExtractStringValue(argument);
                return string.IsNullOrWhiteSpace(value) ? null : value!.ToUpperInvariant();
            default:
                return null;
        }
    }

    private static string InferHttpVerb(string identifier)
    {
        if (identifier.StartsWith("Get", StringComparison.OrdinalIgnoreCase))
        {
            return "GET";
        }

        if (identifier.StartsWith("Post", StringComparison.OrdinalIgnoreCase))
        {
            return "POST";
        }

        if (identifier.StartsWith("Put", StringComparison.OrdinalIgnoreCase))
        {
            return "PUT";
        }

        if (identifier.StartsWith("Delete", StringComparison.OrdinalIgnoreCase))
        {
            return "DELETE";
        }

        if (identifier.StartsWith("Patch", StringComparison.OrdinalIgnoreCase))
        {
            return "PATCH";
        }

        return identifier.ToUpperInvariant();
    }
}
