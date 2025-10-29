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
        var candidates = EnumerateFlowMapCandidates(_workspaceRoot).ToList();
        if (candidates.Count == 0)
        {
            return;
        }

        foreach (var mapPath in candidates)
        {
            try
            {
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

                        RegisterClientTargetService(client, target);
                    }
                }
            }
            catch
            {
                // Ignore malformed flow maps so one bad file does not break analysis.
            }
        }
    }

    private static IEnumerable<string> EnumerateFlowMapCandidates(string workspaceRoot)
    {
        if (string.IsNullOrWhiteSpace(workspaceRoot))
        {
            yield break;
        }

        var current = Path.GetFullPath(workspaceRoot);
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        while (!string.IsNullOrWhiteSpace(current))
        {
            var candidate = Path.Combine(current, "flow.map.json");
            if (File.Exists(candidate) && seen.Add(candidate))
            {
                yield return candidate;
            }

            var parent = Directory.GetParent(current)?.FullName;
            if (parent is null || string.Equals(parent, current, StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            current = parent;
        }
    }

    private void RegisterClientTargetService(string client, string target)
    {
        _clientTargetServices[client] = target;

        foreach (var alternate in DeriveClientBindingKeys(client))
        {
            if (string.IsNullOrWhiteSpace(alternate))
            {
                continue;
            }

            if (!_clientTargetServices.TryGetValue(alternate, out var existing) || string.Equals(existing, target, StringComparison.OrdinalIgnoreCase))
            {
                _clientTargetServices[alternate] = target;
            }
        }
    }

    private static IEnumerable<string> DeriveClientBindingKeys(string client)
    {
        if (string.IsNullOrWhiteSpace(client))
        {
            yield break;
        }

        var keys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        void Add(string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                keys.Add(value);
            }
        }

        var normalized = TrimGlobalAlias(client.Replace('+', '.'));
        Add(normalized);
        Add(GetTypeNameWithoutGenerics(normalized));

        var candidateTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            normalized,
            GetTypeNameWithoutGenerics(normalized)
        };

        foreach (var typeName in candidateTypes.ToArray())
        {
            foreach (var argument in SplitGenericArguments(typeName))
            {
                candidateTypes.Add(argument);
                candidateTypes.Add(GetTypeNameWithoutGenerics(argument));
            }
        }

        foreach (var typeName in candidateTypes)
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                continue;
            }

            var topLevel = GetTopLevelSimpleIdentifier(typeName);
            Add(topLevel);
            if (!string.IsNullOrWhiteSpace(topLevel) && topLevel.Length > 1 && topLevel[0] == 'I' && char.IsUpper(topLevel[1]))
            {
                Add(topLevel[1..]);
            }

            var innermost = GetSimpleIdentifier(typeName);
            Add(innermost);
            if (!string.IsNullOrWhiteSpace(innermost) && innermost.Length > 1 && innermost[0] == 'I' && char.IsUpper(innermost[1]))
            {
                Add(innermost[1..]);
            }
        }

        foreach (var key in keys)
        {
            yield return key;
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
                using var document = JsonDocument.Parse(stream, new JsonDocumentOptions { CommentHandling = JsonCommentHandling.Skip});
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
                        var simpleName = GetTopLevelSimpleIdentifier(autoRegistration.ServiceType);
                        if (!string.IsNullOrWhiteSpace(simpleName) &&
                            !string.Equals(simpleName, autoRegistration.ServiceType, StringComparison.Ordinal))
                        {
                            AddServiceRegistration(simpleName, autoRegistration);
                        }
                    }
                }

                continue;
            }

            if (!IsServiceRegistrationMethod(methodName))
            {
                if (string.Equals(methodName, "Scan", StringComparison.Ordinal) &&
                    TryCaptureServiceScan(project, tree, invocation))
                {
                    continue;
                }

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
                        var simpleName = GetTopLevelSimpleIdentifier(autoRegistration.ServiceType);
                        if (!string.IsNullOrWhiteSpace(simpleName) &&
                            !string.Equals(simpleName, autoRegistration.ServiceType, StringComparison.Ordinal))
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

            var simple = GetTopLevelSimpleIdentifier(serviceType!);
            if (!string.IsNullOrWhiteSpace(simple) &&
                !string.Equals(simple, serviceType, StringComparison.Ordinal))
            {
                AddServiceRegistration(simple, registration);
            }
        }
    }

    private void CaptureMediatorRegistration(string serviceType, string implementationType)
    {
        var serviceBase = GetTypeNameWithoutGenerics(serviceType);
        var assemblyHint = GuessAssemblyName(implementationType);
        if (serviceBase.EndsWith("IPipelineBehavior", StringComparison.Ordinal))
        {
            var arguments = SplitGenericArguments(serviceType);
            if (arguments.Count == 0)
            {
                return;
            }

            var requestType = QualifyTypeName(arguments[0], assemblyHint);
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

            var requestType = QualifyTypeName(arguments[0], assemblyHint);
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
            // IServiceProvider.GetRequiredService<T>() or GetService<T>() detection (service locator usage)
            if (invocation.Expression is MemberAccessExpressionSyntax mas)
            {
                if (mas.Name is GenericNameSyntax gname &&
                    (gname.Identifier.Text == "GetRequiredService" || gname.Identifier.Text == "GetService") &&
                    gname.TypeArgumentList.Arguments.Count == 1)
                {
                    var contractType = gname.TypeArgumentList.Arguments[0].ToString();
                    if (!string.IsNullOrWhiteSpace(contractType))
                    {
                        if (TryEnsureServiceNode(contractType, out var serviceId, out _))
                        {
                            var span = ToGraphSpan(tree, invocation);
                            _edges.Add(new GraphEdge
                            {
                                From = serviceId!,
                                To = serviceId!,
                                Kind = "service_located",
                                Source = "static",
                                Confidence = 0.7,
                                Transform = new GraphTransform
                                {
                                    Type = "ioc.locator",
                                    Location = new GraphLocation { File = GetRelativePath(tree.FilePath), Line = span.StartLine }
                                },
                                Props = new Dictionary<string, object>
                                {
                                    ["method"] = gname.Identifier.Text,
                                    ["service_type"] = contractType
                                },
                                Evidence = CreateEvidence(GetRelativePath(tree.FilePath), span)
                            });
                        }
                    }
                }
            }
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

            foreach (var alias in DeriveClientBindingKeys(clientType!))
            {
                if (!_httpClientBaseUrls.ContainsKey(alias))
                {
                    _httpClientBaseUrls[alias] = address;
                }
            }
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

        var simple = GetTopLevelSimpleIdentifier(serviceType);
        if (!string.IsNullOrWhiteSpace(simple))
        {
            if (!string.Equals(simple, serviceType, StringComparison.Ordinal))
            {
                AddServiceRegistration(simple, registration);
            }

            if (simple.Length > 1 && simple[0] == 'I' && char.IsUpper(simple[1]))
            {
                var trimmedSimple = simple[1..];
                AddServiceRegistration(trimmedSimple, registration);

                var namespacePart = GetTypeNamespace(serviceType);
                if (!string.IsNullOrWhiteSpace(namespacePart))
                {
                    var qualifiedTrimmed = $"{namespacePart}.{trimmedSimple}";
                    AddServiceRegistration(qualifiedTrimmed, registration);
                }
            }
        }
    }

    private bool TryCaptureServiceScan(ProjectInfo project, SyntaxTree tree, InvocationExpressionSyntax invocation)
    {
        var typeFilters = invocation
            .DescendantNodes()
            .OfType<TypeOfExpressionSyntax>()
            .Select(expr => expr.Type.ToString())
            .ToList();

        if (!typeFilters.Any(type => type.StartsWith("IControlledRepository", StringComparison.Ordinal)))
        {
            return false;
        }

        var span = ToGraphSpan(tree, invocation);
        var filePath = GetRelativePath(tree.FilePath);
        var registered = false;

        foreach (var repository in _repositories.Values)
        {
            if (!string.Equals(repository.Assembly, project.AssemblyName, StringComparison.OrdinalIgnoreCase) ||
                repository.ControlledEntities.Count == 0)
            {
                continue;
            }

            foreach (var entity in repository.ControlledEntities)
            {
                var trimmedEntity = TrimGlobalAlias(entity);
                var simpleEntity = GetSimpleIdentifier(trimmedEntity);
                if (string.IsNullOrWhiteSpace(simpleEntity))
                {
                    continue;
                }

                var variants = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    $"IControlledRepository<{simpleEntity}>"
                };

                if (!string.IsNullOrWhiteSpace(trimmedEntity) &&
                    !string.Equals(trimmedEntity, simpleEntity, StringComparison.OrdinalIgnoreCase))
                {
                    variants.Add($"IControlledRepository<{trimmedEntity}>");
                }

                foreach (var variant in variants)
                {
                    RegisterServiceRegistration(variant, repository.Fqdn, "Scoped (scan)", filePath, span, project);
                    registered = true;
                }
            }
        }

        return registered;
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
            var serviceType = generic.TypeArgumentList.Arguments[0].ToString();
            var created = TryExtractImplementationType(lambda.Body);
            if (string.IsNullOrWhiteSpace(created))
            {
                created = serviceType;
            }
            return (serviceType, created);
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
        if (string.IsNullOrWhiteSpace(implementationType))
        {
            implementationType = serviceType;
        }
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

    private static string? TryExtractImplementationType(CSharpSyntaxNode? body)
    {
        switch (body)
        {
            case null:
                return null;
            case ExpressionSyntax expr:
                return TryExtractTypeFromLambdaExpression(expr);
            case BlockSyntax block:
                foreach (var returnStatement in block.DescendantNodes().OfType<ReturnStatementSyntax>())
                {
                    var returned = TryExtractTypeFromLambdaExpression(returnStatement.Expression);
                    if (!string.IsNullOrWhiteSpace(returned))
                    {
                        return returned;
                    }
                }

                var creation = block.DescendantNodes().OfType<ObjectCreationExpressionSyntax>().FirstOrDefault();
                if (creation is not null)
                {
                    return creation.Type.ToString();
                }

                var invocation = block.DescendantNodes().OfType<InvocationExpressionSyntax>().FirstOrDefault();
                if (invocation is not null)
                {
                    return TryExtractTypeFromLambdaExpression(invocation);
                }
                break;
        }

        return null;
    }

    private static string? TryExtractTypeFromLambdaExpression(ExpressionSyntax? expression)
    {
        switch (expression)
        {
            case null:
                return null;
            case ObjectCreationExpressionSyntax creation:
                return creation.Type.ToString();
            case AwaitExpressionSyntax awaitExpression:
                return TryExtractTypeFromLambdaExpression(awaitExpression.Expression);
            case CastExpressionSyntax castExpression:
                return castExpression.Type.ToString();
            case InvocationExpressionSyntax invocation:
                if (invocation.Expression is MemberAccessExpressionSyntax { Name: GenericNameSyntax genericMember } && genericMember.TypeArgumentList.Arguments.Count > 0)
                {
                    return genericMember.TypeArgumentList.Arguments.Last().ToString();
                }

                if (invocation.Expression is GenericNameSyntax genericInvocation && genericInvocation.TypeArgumentList.Arguments.Count > 0)
                {
                    return genericInvocation.TypeArgumentList.Arguments.Last().ToString();
                }

                if (invocation.Expression is MemberAccessExpressionSyntax memberAccess &&
                    memberAccess.Name is IdentifierNameSyntax identifierName &&
                    invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression is TypeOfExpressionSyntax typeOfExpression)
                {
                    var method = identifierName.Identifier.Text;
                    if (string.Equals(method, "CreateInstance", StringComparison.OrdinalIgnoreCase))
                    {
                        return typeOfExpression.Type.ToString();
                    }
                }

                if (invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression is TypeOfExpressionSyntax typeOfArgument)
                {
                    return typeOfArgument.Type.ToString();
                }

                return null;
            case ParenthesizedExpressionSyntax parenthesized:
                return TryExtractTypeFromLambdaExpression(parenthesized.Expression);
        }

        return null;
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
                string? implementationId;
                if (TryResolveNodeReference(registration.ImplementationType, out var implementation))
                {
                    implementationId = implementation.Id;
                }
                else
                {
                    implementationId = EnsureServiceImplementationNode(registration);
                    if (implementationId is null)
                    {
                        continue;
                    }
                }

                var props = new Dictionary<string, object>
                {
                    ["lifetime"] = registration.Lifetime,
                    ["implementation_type"] = registration.ImplementationType
                };

                _edges.Add(new GraphEdge
                {
                    From = serviceId!,
                    To = implementationId,
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

        // Open generic closure synthesis & options expansion
        SynthesizeGenericClosuresAndOptions();
    }

    private void SynthesizeGenericClosuresAndOptions()
    {
        // Build lookup of open generic simple name -> open service node ids
        var openGenericServiceNodes = _nodes.Values
            .Where(n => n.Type == "app.service_contract" && n.Fqdn is { } f && f.Contains("<", StringComparison.Ordinal) && f.Contains(">", StringComparison.Ordinal))
            .GroupBy(n => n.Name.Split('<')[0], StringComparer.Ordinal)
            .ToDictionary(g => g.Key, g => g.ToList(), StringComparer.Ordinal);

        // Map open generic service node -> its implemented_by targets
        var implementedByLookup = _edges
            .Where(e => e.Kind == "implemented_by")
            .GroupBy(e => e.From)
            .ToDictionary(g => g.Key, g => g.ToList(), StringComparer.Ordinal);

        foreach (var service in _nodes.Values.Where(n => n.Type == "app.service_contract"))
        {
            if (string.IsNullOrWhiteSpace(service.Fqdn)) continue;

            // Options expansion: IOptions<T> / IOptionsSnapshot<T>
            if (service.Fqdn.StartsWith("Microsoft.Extensions.Options.IOptions<", StringComparison.Ordinal) ||
                service.Fqdn.StartsWith("Microsoft.Extensions.Options.IOptionsSnapshot<", StringComparison.Ordinal))
            {
                var innerStart = service.Fqdn.IndexOf('<');
                var innerEnd = service.Fqdn.LastIndexOf('>');
                if (innerStart > 0 && innerEnd > innerStart)
                {
                    var inner = service.Fqdn.Substring(innerStart + 1, innerEnd - innerStart - 1).Trim();
                    if (!_nodes.Values.Any(n => string.Equals(n.Fqdn, inner, StringComparison.Ordinal)))
                    {
                        // Create a node representing the options POCO if not present
                        var symbolId = $"T:{inner}";
                        var id = StableId.For("config.options_poco", inner, GuessAssemblyName(inner), symbolId);
                        if (!_nodes.ContainsKey(id))
                        {
                            _nodes[id] = new GraphNode
                            {
                                Id = id,
                                Type = "config.options_poco",
                                Name = GetTopLevelSimpleIdentifier(inner),
                                Fqdn = inner,
                                Assembly = GuessAssemblyName(inner),
                                Project = string.Empty,
                                FilePath = string.Empty,
                                SymbolId = symbolId,
                                Tags = new[] { "config" }
                            };
                        }
                        // Edge from IOptions<T> contract to inner POCO implementation
                        _edges.Add(new GraphEdge
                        {
                            From = service.Id,
                            To = id,
                            Kind = "implemented_by",
                            Source = "synthetic",
                            Confidence = 0.6,
                            Transform = new GraphTransform { Type = "options.expansion" },
                            Props = new Dictionary<string, object> { ["options_type"] = inner }
                        });
                    }
                    else
                    {
                        var existing = _nodes.Values.First(n => string.Equals(n.Fqdn, inner, StringComparison.Ordinal));
                        if (!_edges.Any(e => e.From == service.Id && e.To == existing.Id && e.Kind == "implemented_by"))
                        {
                            _edges.Add(new GraphEdge
                            {
                                From = service.Id,
                                To = existing.Id,
                                Kind = "implemented_by",
                                Source = "synthetic",
                                Confidence = 0.6,
                                Transform = new GraphTransform { Type = "options.expansion" },
                                Props = new Dictionary<string, object> { ["options_type"] = inner }
                            });
                        }
                    }
                }
            }

            // Open generic closure mapping
            if (HasConcreteGenericArguments(service.Fqdn))
            {
                var baseName = service.Name.Split('<')[0];
                if (openGenericServiceNodes.TryGetValue(baseName, out var openNodes))
                {
                    foreach (var openNode in openNodes)
                    {
                        if (!implementedByLookup.TryGetValue(openNode.Id, out var implEdges))
                        {
                            continue;
                        }

                        foreach (var implEdge in implEdges)
                        {
                            if (_edges.Any(e => e.From == service.Id && e.To == implEdge.To && e.Kind == "implemented_by"))
                            {
                                continue;
                            }

                            var closedArguments = SplitGenericArguments(service.Fqdn);
                            var closedArgumentText = closedArguments.Count > 0
                                ? string.Join(", ", closedArguments)
                                : string.Empty;

                            _edges.Add(new GraphEdge
                            {
                                From = service.Id,
                                To = implEdge.To,
                                Kind = "implemented_by",
                                Source = "synthetic",
                                Confidence = implEdge.Confidence * 0.9,
                                Transform = new GraphTransform { Type = "generic.closure" },
                                Props = new Dictionary<string, object>
                                {
                                    ["closure_of"] = openNode.Fqdn ?? openNode.Name,
                                    ["closed_args"] = closedArgumentText
                                }
                            });
                        }
                    }
                }
            }
        }
    }

    private bool TryEnsureServiceNode(string serviceType, out string? nodeId, out ServiceRegistrationInfo? registration, string? preferredTargetType = null)
    {
        var targetAwareServiceType = serviceType;
        registration = null;

        if (!string.IsNullOrWhiteSpace(preferredTargetType) && !serviceType.Contains('<'))
        {
            var closedCandidate = $"{GetTypeNameWithoutGenerics(serviceType)}<{preferredTargetType}>";
            var closedRegistration = FindServiceRegistration(closedCandidate, preferredTargetType);
            if (closedRegistration is not null)
            {
                targetAwareServiceType = closedCandidate;
                registration = closedRegistration;
            }
        }

        registration ??= FindServiceRegistration(targetAwareServiceType, preferredTargetType);
        var effectiveServiceType = registration?.ServiceType ?? targetAwareServiceType;
        var assembly = registration?.Assembly ?? GuessAssemblyName(effectiveServiceType);
        var project = registration?.Project ?? string.Empty;
        var filePath = registration?.FilePath ?? string.Empty;
        GraphSpan? span = registration?.Span;

        if (_services.TryGetValue(effectiveServiceType, out var serviceInfo))
        {
            effectiveServiceType = serviceInfo.Fqdn;
            assembly = serviceInfo.Assembly;
            project = serviceInfo.Project;
            filePath = serviceInfo.FilePath;
            span = serviceInfo.Span;
        }
        else
        {
            var simple = GetTopLevelSimpleIdentifier(effectiveServiceType);
            var match = _services.Values.FirstOrDefault(s => s.Name.Equals(simple, StringComparison.OrdinalIgnoreCase));
            if (match is not null)
            {
                effectiveServiceType = match.Fqdn;
                assembly = match.Assembly;
                project = match.Project;
                filePath = match.FilePath;
                span = match.Span;
            }
        }

        var symbolId = $"T:{effectiveServiceType}";

        var id = StableId.For("app.service_contract", effectiveServiceType, assembly, symbolId);
        if (!_nodes.ContainsKey(id))
        {
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "app.service_contract",
                Name = GetTopLevelSimpleIdentifier(effectiveServiceType),
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

        var root = GetTypeAssemblyRoot(serviceType);
        if (!string.IsNullOrWhiteSpace(root))
        {
            return root;
        }

        var arguments = SplitGenericArguments(serviceType);
        foreach (var argument in arguments)
        {
            var argumentRoot = GetTypeAssemblyRoot(argument);
            if (!string.IsNullOrWhiteSpace(argumentRoot))
            {
                return argumentRoot;
            }
        }

        var trimmed = TrimGlobalAlias(serviceType.Trim());
        var withoutGenerics = GetTypeNameWithoutGenerics(trimmed);
        var fallbackRoot = GetTypeAssemblyRoot(withoutGenerics);
        return string.IsNullOrWhiteSpace(fallbackRoot) ? withoutGenerics : fallbackRoot;
    }

    private ServiceRegistrationInfo? FindServiceRegistration(string serviceType, string? preferredTargetType = null)
    {
        if (_serviceRegistrations.TryGetValue(serviceType, out var registrations) && !registrations.IsEmpty)
        {
            var best = SelectBestRegistration(serviceType, registrations, preferredTargetType);
            if (best is not null)
            {
                return best;
            }
        }

        if (TryMakeOpenGenericType(serviceType, out var openServiceType, out _))
        {
            if (_serviceRegistrations.TryGetValue(openServiceType, out var openRegistrations) && !openRegistrations.IsEmpty)
            {
                var bestOpen = SelectBestRegistration(serviceType, openRegistrations, preferredTargetType);
                if (bestOpen is not null)
                {
                    return bestOpen;
                }
            }

            var openSimple = GetTopLevelSimpleIdentifier(openServiceType);
            if (_serviceRegistrations.TryGetValue(openSimple, out var openSimpleRegistrations) && !openSimpleRegistrations.IsEmpty)
            {
                var bestOpenSimple = SelectBestRegistration(serviceType, openSimpleRegistrations, preferredTargetType);
                if (bestOpenSimple is not null)
                {
                    return bestOpenSimple;
                }
            }
        }

        var simple = GetTopLevelSimpleIdentifier(serviceType);
        if (_serviceRegistrations.TryGetValue(simple, out var simpleRegistrations) && !simpleRegistrations.IsEmpty)
        {
            var bestSimple = SelectBestRegistration(serviceType, simpleRegistrations, preferredTargetType);
            if (bestSimple is not null)
            {
                return bestSimple;
            }
        }

        var simpleWithoutInterface = GetTopLevelSimpleIdentifier(serviceType);
        if (!string.IsNullOrWhiteSpace(simpleWithoutInterface) &&
            simpleWithoutInterface.Length > 1 &&
            simpleWithoutInterface[0] == 'I' &&
            char.IsUpper(simpleWithoutInterface[1]))
        {
            var trimmedSimple = simpleWithoutInterface[1..];
            if (_serviceRegistrations.TryGetValue(trimmedSimple, out var altSimpleRegistrations) && !altSimpleRegistrations.IsEmpty)
            {
                var bestTrimmed = SelectBestRegistration(serviceType, altSimpleRegistrations, preferredTargetType);
                if (bestTrimmed is not null)
                {
                    return bestTrimmed;
                }
            }

            var namespacePart = GetTypeNamespace(serviceType);
            if (!string.IsNullOrWhiteSpace(namespacePart))
            {
                var qualifiedTrimmed = $"{namespacePart}.{trimmedSimple}";
                if (_serviceRegistrations.TryGetValue(qualifiedTrimmed, out var qualifiedRegistrations) && !qualifiedRegistrations.IsEmpty)
                {
                    var bestQualified = SelectBestRegistration(serviceType, qualifiedRegistrations, preferredTargetType);
                    if (bestQualified is not null)
                    {
                        return bestQualified;
                    }
                }
            }
        }

        return null;
    }

    private ServiceRegistrationInfo? SelectBestRegistration(string requestedType, IEnumerable<ServiceRegistrationInfo> candidates, string? preferredTargetType = null)
    {
        var list = candidates as IList<ServiceRegistrationInfo> ?? candidates.ToList();
        if (list.Count == 0)
        {
            return null;
        }

        var requestedSimple = GetTopLevelSimpleIdentifier(requestedType);
        var requestedNamespace = GetTypeNamespace(requestedType);
        var requestedAssemblyRoot = GetTypeAssemblyRoot(requestedType);

        ServiceRegistrationInfo? best = null;
        var bestScore = int.MinValue;

        foreach (var candidate in list)
        {
            var score = 0;

            if (string.Equals(candidate.ServiceType, requestedType, StringComparison.OrdinalIgnoreCase))
            {
                score += 1_000;
            }

            var candidateSimple = GetTopLevelSimpleIdentifier(candidate.ServiceType);
            if (!string.IsNullOrWhiteSpace(requestedSimple) &&
                string.Equals(candidateSimple, requestedSimple, StringComparison.OrdinalIgnoreCase))
            {
                score += 200;
            }

            var candidateNamespace = GetTypeNamespace(candidate.ServiceType);
            if (!string.IsNullOrWhiteSpace(requestedNamespace) && !string.IsNullOrWhiteSpace(candidateNamespace))
            {
                score += LongestCommonPrefixLength(requestedNamespace, candidateNamespace);
            }

            if (!string.IsNullOrWhiteSpace(requestedAssemblyRoot))
            {
                var candidateAssemblyRoot = GetAssemblyRoot(candidate.Assembly);
                if (!string.IsNullOrWhiteSpace(candidateAssemblyRoot) &&
                    string.Equals(candidateAssemblyRoot, requestedAssemblyRoot, StringComparison.OrdinalIgnoreCase))
                {
                    score += 150;
                }
            }

            if (!string.IsNullOrWhiteSpace(candidate.Project) &&
                !string.IsNullOrWhiteSpace(requestedNamespace) &&
                requestedNamespace.IndexOf(candidate.Project, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                score += 25;
            }

            if (!string.IsNullOrWhiteSpace(preferredTargetType))
            {
                var targetSimple = GetTopLevelSimpleIdentifier(preferredTargetType);
                if (!string.IsNullOrWhiteSpace(targetSimple))
                {
                    if (candidate.ImplementationType.Contains(targetSimple, StringComparison.OrdinalIgnoreCase) ||
                        candidate.ServiceType.Contains(targetSimple, StringComparison.OrdinalIgnoreCase))
                    {
                        score += 400;
                    }
                }

                if (candidate.ImplementationType.Contains(preferredTargetType, StringComparison.OrdinalIgnoreCase) ||
                    candidate.ServiceType.Contains(preferredTargetType, StringComparison.OrdinalIgnoreCase))
                {
                    score += 250;
                }
            }

            if (best is null || score > bestScore ||
                (score == bestScore && CompareRegistrations(candidate, best) < 0))
            {
                best = candidate;
                bestScore = score;
            }
        }

        return best ?? list.OrderBy(r => r.FilePath, StringComparer.OrdinalIgnoreCase)
            .ThenBy(r => r.Span.StartLine)
            .FirstOrDefault();
    }

    private static int CompareRegistrations(ServiceRegistrationInfo left, ServiceRegistrationInfo right)
    {
        var fileCompare = StringComparer.OrdinalIgnoreCase.Compare(left.FilePath ?? string.Empty, right.FilePath ?? string.Empty);
        if (fileCompare != 0)
        {
            return fileCompare;
        }

        var spanCompare = left.Span.StartLine.CompareTo(right.Span.StartLine);
        if (spanCompare != 0)
        {
            return spanCompare;
        }

        return StringComparer.OrdinalIgnoreCase.Compare(left.ServiceType, right.ServiceType);
    }

    private static string ExtractTypeNamespace(string typeName)
        => GetTypeNamespace(typeName);

    private static string GetAssemblyRootFromTypeName(string typeName)
        => GetTypeAssemblyRoot(typeName);

    private string? EnsureServiceImplementationNode(ServiceRegistrationInfo registration)
    {
        if (registration is null || string.IsNullOrWhiteSpace(registration.ImplementationType))
        {
            return null;
        }

        if (TryResolveNodeReference(registration.ImplementationType, out var reference))
        {
            return reference.Id;
        }

        var implementationType = registration.ImplementationType;
        var assembly = string.IsNullOrWhiteSpace(registration.Assembly)
            ? GuessAssemblyName(implementationType)
            : registration.Assembly;
        var project = string.IsNullOrWhiteSpace(registration.Project) ? string.Empty : registration.Project;
        var symbolId = $"T:{implementationType}";
        var id = StableId.For("app.service", implementationType, assembly, symbolId);

        if (!_nodes.ContainsKey(id))
        {
            // Synthesize a minimal implementation node so flows can resolve the concrete type.
            var simpleName = GetTopLevelSimpleIdentifier(implementationType);
            _nodes.TryAdd(id, new GraphNode
            {
                Id = id,
                Type = "app.service",
                Name = simpleName,
                Fqdn = implementationType,
                Assembly = assembly,
                Project = project,
                FilePath = string.Empty,
                Span = null,
                SymbolId = symbolId,
                Tags = new[] { "app" }
            });
        }

        return id;
    }

    private HttpClientBaseAddress? TryGetHttpClientBaseAddress(string clientType)
    {
        if (_httpClientBaseUrls.TryGetValue(clientType, out var address))
        {
            return address;
        }

        var simple = GetTopLevelSimpleIdentifier(clientType);
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
