using System.Linq;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
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
                    info.OutboundCalls.Add(new HttpClientCall(httpMethod, route, line));
                }
            }
        }

        _httpClients[fqdn] = info;
    }

    private void EmitHttpClients()
    {
        foreach (var client in _httpClients.Values)
        {
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
                Tags = new[] { "integration" }
            };

            if (_httpClientBaseUrls.TryGetValue(client.Name, out var baseAddress) && !_httpClientBaseUrls.ContainsKey(client.Fqdn))
            {
                _httpClientBaseUrls[client.Fqdn] = baseAddress with { ClientType = client.Fqdn };
            }

            foreach (var call in client.OutboundCalls)
            {
                _httpCalls.Add(new HttpCallInfo(client, call));
            }
        }
    }

    private void EmitHttpCalls()
    {
        foreach (var call in _httpCalls)
        {
            if (call.EndpointRoute is null)
            {
                continue;
            }

            var key = $"{call.Call.HttpMethod}:{call.EndpointRoute}";
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

            if (targetService is null && baseUrl is not null)
            {
                var normalized = NormalizeBaseUrlKey(baseUrl);
                if (normalized is not null && _baseUrlServiceAliases.TryGetValue(normalized, out var serviceName))
                {
                    targetService = serviceName;
                }
            }

            if (_minimalEndpoints.TryGetValue(key, out var endpoint))
            {
                var clientId = StableId.For("http.client", call.Client.Fqdn, call.Client.Assembly, call.Client.SymbolId);
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
                    var clientId = StableId.For("http.client", call.Client.Fqdn, call.Client.Assembly, call.Client.SymbolId);
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
