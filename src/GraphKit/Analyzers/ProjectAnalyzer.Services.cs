using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private static bool IsServiceClass(ClassDeclarationSyntax classDeclaration, string filePath, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var className = classDeclaration.Identifier.Text;
        var inServicesPath = filePath.Contains("/Services/", StringComparison.OrdinalIgnoreCase);

        if (!className.EndsWith("Service", StringComparison.Ordinal) && !inServicesPath)
        {
            return false;
        }

        if (classDeclaration.Modifiers.Any(m => m.IsKind(SyntaxKind.StaticKeyword)))
        {
            return false;
        }

        if (IsController(classDeclaration) ||
            ImplementsInterface(classDeclaration, "IRequestHandler") ||
            ImplementsInterface(classDeclaration, "IAsyncRequestHandler") ||
            ImplementsInterface(classDeclaration, "INotificationHandler") ||
            ImplementsInterface(classDeclaration, "IPipelineBehavior") ||
            ImplementsInterface(classDeclaration, "IRequestPreProcessor") ||
            ImplementsInterface(classDeclaration, "IRequestPostProcessor") ||
            IsHttpClient(classDeclaration, fieldTypes) ||
            IsRepository(classDeclaration) ||
            ExtendsType(classDeclaration, "Profile") ||
            ExtendsType(classDeclaration, "DbContext"))
        {
            return false;
        }

        return true;
    }

    private void AnalyzeService(
        ProjectInfo project,
        SyntaxTree tree,
        ClassDeclarationSyntax classDeclaration,
        string namespaceName,
        IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        var serviceInfo = new ServiceInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className);

        var fieldLookup = fieldTypes.ToDictionary(pair => pair.Key.TrimStart('_'), pair => pair.Value, StringComparer.OrdinalIgnoreCase);

        foreach (var method in classDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
            var serviceMethodName = method.Identifier.Text;
            var parameterTypes = method.ParameterList.Parameters
                .Where(p => !string.IsNullOrWhiteSpace(p.Identifier.Text))
                .ToDictionary(
                    p => p.Identifier.Text,
                    p => p.Type is null ? null : QualifyTypeName(p.Type.ToString()),
                    StringComparer.OrdinalIgnoreCase);

            var localVariables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var local in method.DescendantNodes().OfType<LocalDeclarationStatementSyntax>())
            {
                var declaredType = local.Declaration.Type.ToString();
                foreach (var variable in local.Declaration.Variables)
                {
                    var resolvedType = declaredType;
                    if (string.Equals(resolvedType, "var", StringComparison.OrdinalIgnoreCase) &&
                        variable.Initializer?.Value is ObjectCreationExpressionSyntax creation)
                    {
                        resolvedType = creation.Type.ToString();
                    }

                    resolvedType = QualifyTypeName(resolvedType);
                    localVariables[variable.Identifier.Text] = resolvedType;
                }
            }

            foreach (var memberAccess in method.DescendantNodes().OfType<MemberAccessExpressionSyntax>())
            {
                if (memberAccess.Expression is not IdentifierNameSyntax identifier)
                {
                    continue;
                }

                var fieldName = identifier.Identifier.Text.TrimStart('_');
                if (!fieldLookup.TryGetValue(fieldName, out var descriptor))
                {
                    continue;
                }

                var typeName = descriptor.Type;
                var resolvedType = ResolveImplementationType(typeName) ?? typeName;
                var invocation = memberAccess.Parent as InvocationExpressionSyntax;

                if (IsConfigurationType(resolvedType) || IsConfigurationType(typeName))
                {
                    if (invocation is not null && TryCaptureConfigurationUsage(memberAccess, invocation, resolvedType ?? typeName, tree) is { } configurationUsage)
                    {
                        serviceInfo.ConfigurationUsages.Add(configurationUsage);
                    }

                    continue;
                }

                if (IsCacheService(resolvedType) || IsCacheService(typeName))
                {
                    var cacheType = IsCacheService(resolvedType) ? resolvedType : typeName;
                    if (TryCaptureCacheInvocation(memberAccess, invocation, cacheType, tree) is { } cacheInvocation)
                    {
                        serviceInfo.CacheInvocations.Add(cacheInvocation);
                    }
                    continue;
                }

                var invocationLineNode = (SyntaxNode?)invocation ?? memberAccess;
                var line = GetLineNumber(tree, invocationLineNode);
                var methodName = GetMemberName(memberAccess.Name);
                var recordedUsage = false;
                var baseTypeName = GetTypeNameWithoutGenerics(typeName);
                var resolvedBaseType = GetTypeNameWithoutGenerics(resolvedType);

                if (IsLoggerType(resolvedBaseType) || IsLoggerType(baseTypeName))
                {
                    if (TryExtractLogLevel(methodName) is { } level)
                    {
                        serviceInfo.LogInvocations.Add(new HandlerLogInvocation(level, line));
                        recordedUsage = true;
                    }
                }

                if (invocation is not null && IsGuardInvocation(memberAccess))
                {
                    serviceInfo.ValidationCalls.Add(new HandlerValidationCall(memberAccess.Expression.ToString(), methodName ?? string.Empty, line));
                    recordedUsage = true;
                }

                if (IsClientType(baseTypeName) || IsClientType(resolvedBaseType))
                {
                    var clientType = !string.Equals(resolvedBaseType, baseTypeName, StringComparison.Ordinal)
                        ? resolvedBaseType
                        : baseTypeName;
                    var httpMethod = methodName?.ToUpperInvariant() ?? string.Empty;
                    serviceInfo.HttpClientInvocations.Add(new HandlerClientInvocation(clientType, httpMethod, null, line));
                    recordedUsage = true;
                }

                if (TryResolveOptionsType(resolvedType) is { } resolvedOptionsType)
                {
                    serviceInfo.OptionsUsages.Add(new OptionsUsage(resolvedOptionsType, line));
                    recordedUsage = true;
                }
                else if (TryResolveOptionsType(typeName) is { } fieldOptionsType)
                {
                    serviceInfo.OptionsUsages.Add(new OptionsUsage(fieldOptionsType, line));
                    recordedUsage = true;
                }

                if (resolvedType.EndsWith("Repository", StringComparison.Ordinal))
                {
                    var operation = DetermineRepositoryOperation(methodName ?? string.Empty);
                    serviceInfo.RepositoryCalls.Add(new HandlerRepositoryCall(resolvedType, methodName ?? string.Empty, line, operation));
                    continue;
                }

                if (typeName.Contains("IMapper", StringComparison.Ordinal) && memberAccess.Name is GenericNameSyntax mapperGeneric && mapperGeneric.Identifier.Text == "Map")
                {
                    var destination = mapperGeneric.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                    var sourceExpression = invocation?.ArgumentList.Arguments.FirstOrDefault()?.Expression?.ToString();
                    var sourceType = sourceExpression is not null && parameterTypes.TryGetValue(sourceExpression, out var resolved)
                        ? resolved
                        : null;
                    serviceInfo.MapperCalls.Add(new HandlerMapperCall(sourceType, destination, line));
                    recordedUsage = true;
                }

                var normalizedServiceType = NormalizeServiceType(resolvedType ?? typeName);
                if (!recordedUsage || !normalizedServiceType.EndsWith("Repository", StringComparison.Ordinal))
                {
                    serviceInfo.ServiceUsages.Add(new ServiceUsage(normalizedServiceType, line, serviceMethodName, methodName));
                }
            }

            foreach (var elementAccess in method.DescendantNodes().OfType<ElementAccessExpressionSyntax>())
            {
                if (elementAccess.Expression is not IdentifierNameSyntax identifier)
                {
                    continue;
                }

                var fieldName = identifier.Identifier.Text.TrimStart('_');
                if (!fieldLookup.TryGetValue(fieldName, out var descriptor))
                {
                    continue;
                }

                var resolvedType = ResolveImplementationType(descriptor.Type) ?? descriptor.Type;
                if (!IsConfigurationType(resolvedType) && !IsConfigurationType(descriptor.Type))
                {
                    continue;
                }

                if (TryCaptureConfigurationIndexer(elementAccess, resolvedType ?? descriptor.Type, tree) is { } configurationUsage)
                {
                    serviceInfo.ConfigurationUsages.Add(configurationUsage);
                }
            }

            foreach (var invocation in method.DescendantNodes().OfType<InvocationExpressionSyntax>())
            {
                if (invocation.Expression is not MemberAccessExpressionSyntax extensionAccess)
                {
                    continue;
                }

                if (extensionAccess.Name is GenericNameSyntax { Identifier.Text: "ProjectTo" } projectTo)
                {
                    var destination = projectTo.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                    var sourceType = TryResolveProjectionSource(extensionAccess.Expression, parameterTypes, localVariables, fieldLookup);
                    if (!string.IsNullOrWhiteSpace(destination))
                    {
                        serviceInfo.MapperCalls.Add(new HandlerMapperCall(sourceType, destination, GetLineNumber(tree, invocation)));
                    }
                }
            }
        }

        _services[fqdn] = serviceInfo;
    }

    private void EmitServices()
    {
        foreach (var service in _services.Values)
        {
            var id = StableId.For("app.service", service.Fqdn, service.Assembly, service.SymbolId);
            Dictionary<string, object>? serviceProps = null;
            if (service.LogInvocations.Count > 0)
            {
                serviceProps = new Dictionary<string, object>
                {
                    ["log_levels"] = service.LogInvocations
                        .Select(l => l.Level)
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .OrderBy(level => level, StringComparer.OrdinalIgnoreCase)
                        .ToArray()
                };
            }

            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "app.service",
                Name = service.Name,
                Fqdn = service.Fqdn,
                Assembly = service.Assembly,
                Project = service.Project,
                FilePath = service.FilePath,
                Span = service.Span,
                SymbolId = service.SymbolId,
                Tags = new[] { "app" },
                Props = serviceProps
            };

            foreach (var repositoryCall in service.RepositoryCalls)
            {
                var targetType = ResolveImplementationType(repositoryCall.RepositoryType) ?? repositoryCall.RepositoryType;
                var repositoryName = targetType.Split('.').Last();

                if (_repositories.Values.FirstOrDefault(r => r.Name.Equals(repositoryName, StringComparison.Ordinal)) is { } repository)
                {
                    var repositoryId = StableId.For("app.repository", repository.Fqdn, repository.Assembly, repository.SymbolId);
                    _edges.Add(new GraphEdge
                    {
                        From = id,
                        To = repositoryId,
                        Kind = "calls",
                        Source = "static",
                        Confidence = 1.0,
                        Transform = new GraphTransform
                        {
                            Type = "service.call",
                            Location = new GraphLocation { File = service.FilePath, Line = repositoryCall.Line }
                        },
                        Props = new Dictionary<string, object>
                        {
                            ["method"] = repositoryCall.Method,
                            ["operation"] = repositoryCall.Operation
                        },
                        Evidence = CreateEvidence(service.FilePath, repositoryCall.Line)
                    });
                }
            }

            foreach (var mapper in service.MapperCalls)
            {
                if (string.IsNullOrWhiteSpace(mapper.DestinationType))
                {
                    continue;
                }

                if (!TryResolveNodeReference(mapper.DestinationType, out var destination))
                {
                    continue;
                }

                var props = new Dictionary<string, object>
                {
                    ["destination_type"] = mapper.DestinationType
                };

                if (!string.IsNullOrWhiteSpace(mapper.SourceType))
                {
                    props["source_type"] = mapper.SourceType!;
                }

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = destination.Id,
                    Kind = "maps_to",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "automapper.map",
                        Location = new GraphLocation { File = service.FilePath, Line = mapper.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(service.FilePath, mapper.Line)
                });
            }

            foreach (var clientInvocation in service.HttpClientInvocations
                .GroupBy(c => c.ClientType, StringComparer.OrdinalIgnoreCase)
                .Select(group => group.OrderBy(c => c.Line).First()))
            {
                var props = new Dictionary<string, object>();
                if (!string.IsNullOrWhiteSpace(clientInvocation.HttpMethod))
                {
                    props["method"] = clientInvocation.HttpMethod!;
                }

                var propsOrNull = props.Count > 0 ? props : null;

                if (TryResolveHttpClient(clientInvocation.ClientType, out var clientInfo))
                {
                    var clientId = StableId.For("http.client", clientInfo.Fqdn, clientInfo.Assembly, clientInfo.SymbolId);
                    _edges.Add(new GraphEdge
                    {
                        From = id,
                        To = clientId,
                        Kind = "uses_client",
                        Source = "static",
                        Confidence = 1.0,
                        Transform = new GraphTransform
                        {
                            Type = "httpclient.request",
                            Location = new GraphLocation { File = service.FilePath, Line = clientInvocation.Line }
                        },
                        Props = propsOrNull,
                        Evidence = CreateEvidence(service.FilePath, clientInvocation.Line)
                    });
                }
                else
                {
                    var clientId = EnsureHttpClientNode(clientInvocation.ClientType);
                    _edges.Add(new GraphEdge
                    {
                        From = id,
                        To = clientId,
                        Kind = "uses_client",
                        Source = "static",
                        Confidence = 0.7,
                        Transform = new GraphTransform
                        {
                            Type = "httpclient.request",
                            Location = new GraphLocation { File = service.FilePath, Line = clientInvocation.Line }
                        },
                        Props = propsOrNull,
                        Evidence = CreateEvidence(service.FilePath, clientInvocation.Line)
                    });
                }
            }

            foreach (var usage in service.ServiceUsages
                .GroupBy(u => u.ServiceType, StringComparer.OrdinalIgnoreCase)
                .Select(group => group.OrderBy(u => u.Line).First()))
            {
                if (!TryEnsureServiceNode(usage.ServiceType, out var serviceId, out var registration))
                {
                    continue;
                }

                if (IsLoggerType(usage.ServiceType) && service.LogInvocations.Count > 0)
                {
                    foreach (var log in service.LogInvocations
                        .GroupBy(l => l.Level, StringComparer.OrdinalIgnoreCase)
                        .Select(group => group.OrderBy(l => l.Line).First()))
                    {
                        _edges.Add(new GraphEdge
                        {
                            From = id,
                            To = serviceId!,
                            Kind = "logs",
                            Source = "static",
                            Confidence = 1.0,
                            Transform = new GraphTransform
                            {
                                Type = "logging",
                                Location = new GraphLocation { File = service.FilePath, Line = log.Line }
                            },
                            Props = new Dictionary<string, object>
                            {
                                ["level"] = log.Level
                            },
                            Evidence = CreateEvidence(service.FilePath, log.Line)
                        });
                    }
                }

                if (IsStorageService(usage.ServiceType))
                {
                    var storageProps = new Dictionary<string, object>
                    {
                        ["service_type"] = usage.ServiceType
                    };

                    if (!string.IsNullOrWhiteSpace(usage.Method))
                    {
                        storageProps["method"] = usage.Method!;
                    }

                    _edges.Add(new GraphEdge
                    {
                        From = id,
                        To = serviceId!,
                        Kind = "uses_storage",
                        Source = "static",
                        Confidence = 1.0,
                        Transform = new GraphTransform
                        {
                            Type = "storage.access",
                            Location = new GraphLocation { File = service.FilePath, Line = usage.Line }
                        },
                        Props = storageProps,
                        Evidence = CreateEvidence(service.FilePath, usage.Line)
                    });
                }

                var props = new Dictionary<string, object>
                {
                    ["service_type"] = usage.ServiceType
                };

                if (registration is not null)
                {
                    props["lifetime"] = registration.Lifetime;
                }

                if (!string.IsNullOrWhiteSpace(usage.Method))
                {
                    props["method"] = usage.Method!;
                }

                if (!string.IsNullOrWhiteSpace(usage.InvocationMethod))
                {
                    props["invoked_method"] = usage.InvocationMethod!;
                }

                if (serviceId is not null)
                {
                    _edges.Add(new GraphEdge
                    {
                        From = id,
                        To = serviceId,
                        Kind = "uses_service",
                        Source = "static",
                        Confidence = 1.0,
                        Transform = new GraphTransform
                        {
                            Type = "ioc.resolve",
                            Location = new GraphLocation { File = service.FilePath, Line = usage.Line }
                        },
                        Props = props,
                        Evidence = CreateEvidence(service.FilePath, usage.Line)
                    });
                }
            }

            foreach (var cache in service.CacheInvocations)
            {
                var cacheId = EnsureCacheNode(cache.CacheType);
                var props = new Dictionary<string, object>
                {
                    ["method"] = cache.Method,
                    ["operation"] = cache.Operation
                };

                if (!string.IsNullOrWhiteSpace(cache.Key))
                {
                    props["key"] = cache.Key!;
                }

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = cacheId,
                    Kind = "uses_cache",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "cache.operation",
                        Location = new GraphLocation { File = service.FilePath, Line = cache.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(service.FilePath, cache.Line)
                });
            }

            foreach (var validation in service.ValidationCalls)
            {
                var guardId = EnsureGuardNode(validation.GuardType);
                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = guardId,
                    Kind = "validation",
                    Source = "static",
                    Confidence = 0.9,
                    Transform = new GraphTransform
                    {
                        Type = "validation.guard",
                        Location = new GraphLocation { File = service.FilePath, Line = validation.Line }
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["method"] = validation.Method
                    },
                    Evidence = CreateEvidence(service.FilePath, validation.Line)
                });
            }

            foreach (var optionsUsage in service.OptionsUsages)
            {
                var optionsId = EnsureOptionsNode(optionsUsage.OptionsType);
                if (optionsId is null)
                {
                    continue;
                }

                var props = new Dictionary<string, object>
                {
                    ["options_type"] = optionsUsage.OptionsType
                };

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = optionsId,
                    Kind = "uses_options",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "options.access",
                        Location = new GraphLocation { File = service.FilePath, Line = optionsUsage.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(service.FilePath, optionsUsage.Line)
                });
            }

            EmitConfigurationEdges(id, service.ConfigurationUsages);
        }
    }
}





