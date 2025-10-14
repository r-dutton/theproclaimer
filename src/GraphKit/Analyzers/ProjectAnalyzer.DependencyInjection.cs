using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private void LoadFlowMap()
    {
        var mapPath = Path.Combine(_workspaceRoot, "flow.map.json");
        if (!File.Exists(mapPath))
        {
            return;
        }

        using var stream = File.OpenRead(mapPath);
        using var document = JsonDocument.Parse(stream);
        var root = document.RootElement;

        if (root.TryGetProperty("services", out var servicesElement))
        {
            foreach (var serviceProperty in servicesElement.EnumerateObject())
            {
                if (serviceProperty.Value.TryGetProperty("base_urls", out var baseUrls))
                {
                    foreach (var baseUrlProperty in baseUrls.EnumerateObject())
                    {
                        var value = baseUrlProperty.Value.GetString();
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            continue;
                        }

                        var normalized = NormalizeBaseUrlKey(value);
                        if (!string.IsNullOrWhiteSpace(normalized))
                        {
                            _baseUrlServiceAliases[normalized] = serviceProperty.Name;
                        }
                    }
                }
            }
        }

        if (root.TryGetProperty("bindings", out var bindingsElement))
        {
            foreach (var binding in bindingsElement.EnumerateArray())
            {
                if (!binding.TryGetProperty("client", out var clientElement) || !binding.TryGetProperty("target_service", out var targetElement))
                {
                    continue;
                }

                var client = clientElement.GetString();
                var target = targetElement.GetString();
                if (string.IsNullOrWhiteSpace(client) || string.IsNullOrWhiteSpace(target))
                {
                    continue;
                }

                _clientTargetServices[client] = target;
            }
        }
    }

    private void LoadConfigurationValues(ProjectInfo project)
    {
        var projectDirectory = Path.GetDirectoryName(project.ProjectPath);
        if (projectDirectory is null)
        {
            return;
        }

        var configFiles = Directory
            .EnumerateFiles(projectDirectory, "appsettings*.json", SearchOption.TopDirectoryOnly)
            .OrderBy(path => path, StringComparer.OrdinalIgnoreCase);

        foreach (var file in configFiles)
        {
            try
            {
                using var stream = File.OpenRead(file);
                using var document = JsonDocument.Parse(stream);
                var relative = GetRelativePath(file);
                var lines = File.ReadAllLines(file);

                FlattenConfiguration(document.RootElement, string.Empty, (key, value) =>
                {
                    var lineIndex = Array.FindIndex(lines, l => l.Contains(value, StringComparison.Ordinal));
                    var span = new GraphSpan
                    {
                        StartLine = lineIndex >= 0 ? lineIndex + 1 : 1,
                        EndLine = lineIndex >= 0 ? lineIndex + 1 : 1
                    };

                    _configurationValues[key] = new ConfigurationValue(key, value, relative, span);
                });
            }
            catch (JsonException)
            {
                // Ignore malformed configuration files; best-effort hints only.
            }
        }

        UpdateOptionsFromConfiguration();
    }

    private static void FlattenConfiguration(JsonElement element, string prefix, Action<string, string> capture)
    {
        foreach (var property in element.EnumerateObject())
        {
            var key = string.IsNullOrEmpty(prefix)
                ? property.Name
                : $"{prefix}:{property.Name}";

            switch (property.Value.ValueKind)
            {
                case JsonValueKind.Object:
                    FlattenConfiguration(property.Value, key, capture);
                    break;
                case JsonValueKind.String:
                    var value = property.Value.GetString();
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        capture(key, value);
                    }
                    break;
            }
        }
    }

    private void AnalyzeServiceRegistrations(ProjectInfo project, SyntaxTree tree)
    {
        foreach (var invocation in tree.GetRoot().DescendantNodes().OfType<InvocationExpressionSyntax>())
        {
            if (invocation.Expression is not MemberAccessExpressionSyntax memberAccess)
            {
                continue;
            }

            var methodName = memberAccess.Name switch
            {
                GenericNameSyntax generic => generic.Identifier.Text,
                IdentifierNameSyntax identifier => identifier.Identifier.Text,
                _ => null
            };

            if (methodName is null)
            {
                if (TryCaptureAutofacRegistration(project, tree, invocation, out var autofacRegistrations))
                {
                    foreach (var autoRegistration in autofacRegistrations)
                    {
                        AddServiceRegistration(autoRegistration.ServiceType, autoRegistration);
                        var simpleName = autoRegistration.ServiceType.Split('.').Last();
                        if (!string.Equals(simpleName, autoRegistration.ServiceType, StringComparison.Ordinal))
                        {
                            AddServiceRegistration(simpleName, autoRegistration);
                        }
                    }
                }

                continue;
            }

            if (!IsServiceRegistrationMethod(methodName))
            {
                if (string.Equals(methodName, "AddHostedService", StringComparison.Ordinal) &&
                    TryRegisterHostedService(project, tree, invocation, memberAccess))
                {
                    continue;
                }

                if (IsDbContextRegistrationMethod(methodName) &&
                    TryRegisterDbContext(project, tree, invocation, memberAccess, methodName))
                {
                    continue;
                }

                if (string.Equals(methodName, "RegisterType", StringComparison.Ordinal) &&
                    TryCaptureAutofacRegistration(project, tree, invocation, out var autofacRegistrations))
                {
                    foreach (var autoRegistration in autofacRegistrations)
                    {
                        AddServiceRegistration(autoRegistration.ServiceType, autoRegistration);
                        var simpleName = autoRegistration.ServiceType.Split('.').Last();
                        if (!string.Equals(simpleName, autoRegistration.ServiceType, StringComparison.Ordinal))
                        {
                            AddServiceRegistration(simpleName, autoRegistration);
                        }
                    }
                }

                continue;
            }

            (string? serviceType, string? implementationType) = memberAccess.Name switch
            {
                GenericNameSyntax genericName => ResolveGenericRegistration(genericName, invocation),
                _ => ResolveNonGenericRegistration(invocation)
            };

            if (string.IsNullOrWhiteSpace(serviceType) || string.IsNullOrWhiteSpace(implementationType))
            {
                continue;
            }

            var line = GetLineNumber(tree, invocation);
            var span = new GraphSpan { StartLine = line, EndLine = line };
            var registration = new ServiceRegistrationInfo(
                serviceType!,
                implementationType!,
                methodName,
                GetRelativePath(tree.FilePath),
                span,
                project.AssemblyName,
                project.RelativeDirectory);

            AddServiceRegistration(serviceType!, registration);
            CaptureMediatorRegistration(serviceType!, implementationType!);

            var simple = serviceType!.Split('.').Last();
            if (!string.Equals(simple, serviceType, StringComparison.Ordinal))
            {
                AddServiceRegistration(simple, registration);
            }
        }
    }

    private void CaptureMediatorRegistration(string serviceType, string implementationType)
    {
        var serviceBase = GetTypeNameWithoutGenerics(serviceType);
        if (serviceBase.EndsWith("IPipelineBehavior", StringComparison.Ordinal))
        {
            var arguments = SplitGenericArguments(serviceType);
            if (arguments.Count == 0)
            {
                return;
            }

            var requestType = QualifyTypeName(arguments[0]);
            RegisterPipelineRequest(requestType, implementationType);
            return;
        }

        if (serviceBase.EndsWith("IRequestPreProcessor", StringComparison.Ordinal) ||
            serviceBase.EndsWith("IRequestPostProcessor", StringComparison.Ordinal) ||
            serviceBase.EndsWith("IRequestProcessor", StringComparison.Ordinal))
        {
            var arguments = SplitGenericArguments(serviceType);
            if (arguments.Count == 0)
            {
                return;
            }

            var requestType = QualifyTypeName(arguments[0]);
            RegisterProcessorRequest(requestType, implementationType);
        }
    }

    private void RegisterPipelineRequest(string requestType, string behaviorType)
    {
        if (string.IsNullOrWhiteSpace(requestType))
        {
            return;
        }

        var bag = _requestPipelineRegistrations.GetOrAdd(requestType, _ => new ConcurrentBag<string>());
        bag.Add(behaviorType);

        if (FindPipelineBehavior(behaviorType) is { } behavior)
        {
            behavior.RegisteredRequestTypes.Add(requestType);
        }
        else
        {
            var baseType = GetTypeNameWithoutGenerics(behaviorType);
            if (FindPipelineBehavior(baseType) is { } baseBehavior)
            {
                baseBehavior.RegisteredRequestTypes.Add(requestType);
            }
        }
    }

    private void RegisterProcessorRequest(string requestType, string processorType)
    {
        if (string.IsNullOrWhiteSpace(requestType))
        {
            return;
        }

        var bag = _requestProcessorRegistrations.GetOrAdd(requestType, _ => new ConcurrentBag<string>());
        bag.Add(processorType);

        if (FindRequestProcessor(processorType) is { } processor)
        {
            processor.RegisteredRequestTypes.Add(requestType);
        }
        else
        {
            var baseType = GetTypeNameWithoutGenerics(processorType);
            if (FindRequestProcessor(baseType) is { } baseProcessor)
            {
                baseProcessor.RegisteredRequestTypes.Add(requestType);
            }
        }
    }

    private void AnalyzeHttpClientRegistrations(ProjectInfo project, SyntaxTree tree)
    {
        foreach (var invocation in tree.GetRoot().DescendantNodes().OfType<InvocationExpressionSyntax>())
        {
            if (invocation.Expression is not MemberAccessExpressionSyntax { Name: GenericNameSyntax generic } memberAccess)
            {
                continue;
            }

            if (!string.Equals(generic.Identifier.Text, "AddHttpClient", StringComparison.Ordinal))
            {
                continue;
            }

            var clientType = generic.TypeArgumentList.Arguments.FirstOrDefault()?.ToString();
            if (string.IsNullOrWhiteSpace(clientType))
            {
                continue;
            }

            var resolved = ResolveHttpClientBaseUrl(invocation);
            if (resolved is null)
            {
                continue;
            }

            var (baseUrl, configurationKey, configuration) = resolved.Value;
            var line = GetLineNumber(tree, invocation);
            var address = new HttpClientBaseAddress(clientType!, baseUrl, GetRelativePath(tree.FilePath), line, configurationKey, configuration);
            _httpClientBaseUrls[clientType!] = address;
        }
    }

    private (string BaseUrl, string? ConfigurationKey, ConfigurationValue? Configuration)? ResolveHttpClientBaseUrl(InvocationExpressionSyntax invocation)
    {
        string? explicitUrl = null;
        string? configurationKey = null;

        foreach (var literal in invocation.DescendantNodes().OfType<LiteralExpressionSyntax>())
        {
            if (!literal.IsKind(SyntaxKind.StringLiteralExpression))
            {
                continue;
            }

            var value = literal.Token.ValueText;
            if (value.Contains("://", StringComparison.Ordinal) && Uri.TryCreate(value, UriKind.Absolute, out _))
            {
                explicitUrl = value;
                break;
            }

            if (value.Contains(':', StringComparison.Ordinal))
            {
                configurationKey ??= value;
            }
        }

        if (!string.IsNullOrWhiteSpace(explicitUrl))
        {
            return (explicitUrl!, configurationKey, configurationKey is not null && _configurationValues.TryGetValue(configurationKey, out var config) ? config : null);
        }

        if (!string.IsNullOrWhiteSpace(configurationKey) && _configurationValues.TryGetValue(configurationKey!, out var configuration))
        {
            return (configuration.Value, configurationKey, configuration);
        }

        return null;
    }

    private void AddServiceRegistration(string key, ServiceRegistrationInfo registration)
    {
        var bag = _serviceRegistrations.GetOrAdd(key, _ => new ConcurrentBag<ServiceRegistrationInfo>());
        bag.Add(registration);
    }

    private static bool IsServiceRegistrationMethod(string methodName)
        => methodName is "AddScoped" or "AddSingleton" or "AddTransient";

    private static bool IsDbContextRegistrationMethod(string methodName)
        => methodName is "AddDbContext" or "AddDbContextPool" or "AddDbContextFactory" or "AddPooledDbContextFactory";

    private bool TryRegisterHostedService(ProjectInfo project, SyntaxTree tree, InvocationExpressionSyntax invocation, MemberAccessExpressionSyntax memberAccess)
    {
        if (memberAccess.Name is not GenericNameSyntax generic || generic.TypeArgumentList.Arguments.Count == 0)
        {
            return false;
        }

        var hostedType = generic.TypeArgumentList.Arguments[0].ToString();
        if (string.IsNullOrWhiteSpace(hostedType))
        {
            return false;
        }

        var span = ToGraphSpan(tree, invocation);
        var filePath = GetRelativePath(tree.FilePath);
        RegisterServiceRegistration(hostedType, hostedType, "Hosted", filePath, span, project);
        return true;
    }

    private bool TryRegisterDbContext(ProjectInfo project, SyntaxTree tree, InvocationExpressionSyntax invocation, MemberAccessExpressionSyntax memberAccess, string methodName)
    {
        if (memberAccess.Name is not GenericNameSyntax generic || generic.TypeArgumentList.Arguments.Count == 0)
        {
            return false;
        }

        var contextType = generic.TypeArgumentList.Arguments[0].ToString();
        if (string.IsNullOrWhiteSpace(contextType))
        {
            return false;
        }

        var implementationType = generic.TypeArgumentList.Arguments.Count > 1
            ? generic.TypeArgumentList.Arguments[1].ToString()
            : contextType;

        var span = ToGraphSpan(tree, invocation);
        var filePath = GetRelativePath(tree.FilePath);
        var lifetime = methodName switch
        {
            "AddDbContextFactory" => "Singleton",
            "AddPooledDbContextFactory" => "Singleton",
            _ => "Scoped"
        };

        RegisterServiceRegistration(contextType, implementationType, lifetime, filePath, span, project);

        if (methodName is "AddDbContextFactory" or "AddPooledDbContextFactory")
        {
            var factoryType = $"IDbContextFactory<{contextType}>";
            RegisterServiceRegistration(factoryType, factoryType, "Singleton", filePath, span, project);
        }

        return true;
    }

    private void RegisterServiceRegistration(string serviceType, string implementationType, string lifetime, string filePath, GraphSpan span, ProjectInfo project)
    {
        var registration = new ServiceRegistrationInfo(serviceType, implementationType, lifetime, filePath, span, project.AssemblyName, project.RelativeDirectory);
        AddServiceRegistration(serviceType, registration);

        var simple = serviceType.Split('.').Last();
        if (!string.Equals(simple, serviceType, StringComparison.Ordinal))
        {
            AddServiceRegistration(simple, registration);
        }
    }

    private bool TryCaptureAutofacRegistration(ProjectInfo project, SyntaxTree tree, InvocationExpressionSyntax registerInvocation, out List<ServiceRegistrationInfo> registrations)
    {
        registrations = new List<ServiceRegistrationInfo>();

        if (registerInvocation.Expression is not MemberAccessExpressionSyntax { Name: GenericNameSyntax registerGeneric } registerMember ||
            !string.Equals(registerGeneric.Identifier.Text, "RegisterType", StringComparison.Ordinal))
        {
            return false;
        }

        var implementationType = registerGeneric.TypeArgumentList.Arguments.FirstOrDefault()?.ToString();
        if (string.IsNullOrWhiteSpace(implementationType))
        {
            return false;
        }

        var chain = new List<InvocationExpressionSyntax> { registerInvocation };
        var current = registerInvocation;
        while (current.Parent is MemberAccessExpressionSyntax parentMember &&
               parentMember.Parent is InvocationExpressionSyntax parentInvocation)
        {
            current = parentInvocation;
            chain.Add(current);
        }

        var serviceTypes = new HashSet<string>(StringComparer.Ordinal);
        var lifetime = "Transient";

        foreach (var chainInvocation in chain.Skip(1))
        {
            if (chainInvocation.Expression is not MemberAccessExpressionSyntax member)
            {
                continue;
            }

            var identifier = member.Name switch
            {
                GenericNameSyntax generic => generic.Identifier.Text,
                IdentifierNameSyntax identifierName => identifierName.Identifier.Text,
                _ => null
            };

            if (string.IsNullOrWhiteSpace(identifier))
            {
                continue;
            }

            if (string.Equals(identifier, "As", StringComparison.Ordinal))
            {
                switch (member.Name)
                {
                    case GenericNameSyntax generic when generic.TypeArgumentList.Arguments.Count > 0:
                        serviceTypes.Add(generic.TypeArgumentList.Arguments[0].ToString());
                        break;
                    case IdentifierNameSyntax:
                        if (chainInvocation.ArgumentList.Arguments.FirstOrDefault()?.Expression is TypeOfExpressionSyntax typeOf)
                        {
                            serviceTypes.Add(typeOf.Type.ToString());
                        }
                        break;
                }
            }
            else if (string.Equals(identifier, "AsSelf", StringComparison.Ordinal) ||
                     string.Equals(identifier, "AsImplementedInterfaces", StringComparison.Ordinal))
            {
                serviceTypes.Add(implementationType);
            }
            else if (TryMapAutofacLifetime(identifier, out var mappedLifetime))
            {
                lifetime = mappedLifetime;
            }
        }

        if (serviceTypes.Count == 0)
        {
            serviceTypes.Add(implementationType);
        }

        var span = ToGraphSpan(tree, registerInvocation);
        var filePath = GetRelativePath(tree.FilePath);

        foreach (var serviceType in serviceTypes.Where(s => !string.IsNullOrWhiteSpace(s)))
        {
            registrations.Add(new ServiceRegistrationInfo(
                serviceType!,
                implementationType!,
                lifetime,
                filePath,
                span,
                project.AssemblyName,
                project.RelativeDirectory));
        }

        return registrations.Count > 0;
    }

    private static bool TryMapAutofacLifetime(string methodName, out string lifetime)
    {
        lifetime = methodName;

        return methodName is "SingleInstance" or
            "InstancePerLifetimeScope" or
            "InstancePerMatchingLifetimeScope" or
            "InstancePerRequest" or
            "InstancePerOwned" or
            "InstancePerDependency";
    }

    private static (string? ServiceType, string? ImplementationType) ResolveGenericRegistration(GenericNameSyntax generic, InvocationExpressionSyntax invocation)
    {
        if (generic.TypeArgumentList.Arguments.Count >= 2)
        {
            var serviceType = generic.TypeArgumentList.Arguments[0].ToString();
            var implementationType = generic.TypeArgumentList.Arguments[1].ToString();
            return (serviceType, implementationType);
        }

        if (generic.TypeArgumentList.Arguments.Count == 1 && invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression is LambdaExpressionSyntax lambda)
        {
            var created = lambda.Body switch
            {
                ObjectCreationExpressionSyntax creation => creation.Type.ToString(),
                BlockSyntax block => block.DescendantNodes().OfType<ObjectCreationExpressionSyntax>().FirstOrDefault()?.Type.ToString(),
                _ => null
            };

            return (generic.TypeArgumentList.Arguments[0].ToString(), created);
        }

        return (null, null);
    }

    private static (string? ServiceType, string? ImplementationType) ResolveNonGenericRegistration(InvocationExpressionSyntax invocation)
    {
        var arguments = invocation.ArgumentList.Arguments;
        if (arguments.Count < 2)
        {
            return (null, null);
        }

        var serviceType = ExtractTypeFromExpression(arguments[0].Expression);
        var implementationType = ExtractTypeFromExpression(arguments[1].Expression);
        return (serviceType, implementationType);
    }

    private static string? ExtractTypeFromExpression(ExpressionSyntax expression)
    {
        return expression switch
        {
            TypeOfExpressionSyntax typeOf => typeOf.Type.ToString(),
            ObjectCreationExpressionSyntax creation => creation.Type.ToString(),
            IdentifierNameSyntax identifier => identifier.Identifier.Text,
            _ => null
        };
    }

    private void EmitServiceRegistrations()
    {
        foreach (var pair in _serviceRegistrations)
        {
            var registrations = pair.Value.ToArray();
            if (registrations.Length == 0)
            {
                continue;
            }

            var primary = registrations
                .OrderBy(r => r.FilePath, StringComparer.OrdinalIgnoreCase)
                .ThenBy(r => r.Span.StartLine)
                .First();

            if (!TryEnsureServiceNode(primary.ServiceType, out var serviceId, out _))
            {
                continue;
            }

            foreach (var registration in registrations
                .OrderBy(r => r.FilePath, StringComparer.OrdinalIgnoreCase)
                .ThenBy(r => r.Span.StartLine))
            {
                if (!TryResolveNodeReference(registration.ImplementationType, out var implementation))
                {
                    continue;
                }

                var props = new Dictionary<string, object>
                {
                    ["lifetime"] = registration.Lifetime,
                    ["implementation_type"] = registration.ImplementationType
                };

                _edges.Add(new GraphEdge
                {
                    From = serviceId!,
                    To = implementation.Id,
                    Kind = "implemented_by",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "ioc.registration",
                        Location = new GraphLocation { File = registration.FilePath, Line = registration.Span.StartLine }
                    },
                    Props = props,
                    Evidence = CreateEvidence(registration.FilePath, registration.Span)
                });
            }
        }
    }

    private bool TryEnsureServiceNode(string serviceType, out string? nodeId, out ServiceRegistrationInfo? registration)
    {
        registration = FindServiceRegistration(serviceType);
        var effectiveServiceType = registration?.ServiceType ?? serviceType;
        var symbolId = $"T:{effectiveServiceType}";
        var assembly = registration?.Assembly ?? GuessAssemblyName(effectiveServiceType);
        var project = registration?.Project ?? string.Empty;
        var filePath = registration?.FilePath ?? string.Empty;
        var span = registration?.Span;

        var id = StableId.For("app.service_contract", effectiveServiceType, assembly, symbolId);
        if (!_nodes.ContainsKey(id))
        {
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "app.service_contract",
                Name = effectiveServiceType.Split('.').Last(),
                Fqdn = effectiveServiceType,
                Assembly = assembly,
                Project = project,
                FilePath = filePath,
                Span = span,
                SymbolId = symbolId,
                Tags = new[] { "app" }
            };
        }

        nodeId = id;
        return true;
    }

    private static string GuessAssemblyName(string serviceType)
    {
        if (string.IsNullOrWhiteSpace(serviceType))
        {
            return string.Empty;
        }

        var firstSegment = serviceType.Split('.').FirstOrDefault();
        return string.IsNullOrWhiteSpace(firstSegment) ? serviceType : firstSegment;
    }

    private ServiceRegistrationInfo? FindServiceRegistration(string serviceType)
    {
        if (_serviceRegistrations.TryGetValue(serviceType, out var registrations) && !registrations.IsEmpty)
        {
            return registrations
                .OrderBy(r => r.FilePath, StringComparer.OrdinalIgnoreCase)
                .ThenBy(r => r.Span.StartLine)
                .FirstOrDefault();
        }

        var simple = serviceType.Split('.').Last();
        if (_serviceRegistrations.TryGetValue(simple, out var simpleRegistrations) && !simpleRegistrations.IsEmpty)
        {
            return simpleRegistrations
                .OrderBy(r => r.FilePath, StringComparer.OrdinalIgnoreCase)
                .ThenBy(r => r.Span.StartLine)
                .FirstOrDefault();
        }

        if (serviceType.StartsWith("I", StringComparison.Ordinal) && serviceType.Length > 1)
        {
            var trimmed = serviceType.TrimStart('I');
            if (_serviceRegistrations.TryGetValue(trimmed, out var altRegistrations) && !altRegistrations.IsEmpty)
            {
                return altRegistrations
                    .OrderBy(r => r.FilePath, StringComparer.OrdinalIgnoreCase)
                    .ThenBy(r => r.Span.StartLine)
                    .FirstOrDefault();
            }
        }

        return null;
    }

    private HttpClientBaseAddress? TryGetHttpClientBaseAddress(string clientType)
    {
        if (_httpClientBaseUrls.TryGetValue(clientType, out var address))
        {
            return address;
        }

        var simple = clientType.Split('.').Last();
        if (_httpClientBaseUrls.TryGetValue(simple, out var simpleAddress))
        {
            return simpleAddress;
        }

        return null;
    }

    private static string? NormalizeBaseUrlKey(string? baseUrl)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            return null;
        }

        return baseUrl.TrimEnd('/').ToLowerInvariant();
    }
}
