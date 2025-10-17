using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private void AnalyzeHandler(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var handlerInterface = classDeclaration.BaseList?.Types
            .Select(t => t.Type)
            .OfType<GenericNameSyntax>()
            .FirstOrDefault(g => g.Identifier.Text is "IRequestHandler" or "IAsyncRequestHandler");

        if (handlerInterface is null)
        {
            return;
        }

        var requestType = handlerInterface.TypeArgumentList.Arguments.FirstOrDefault()?.ToString() ?? string.Empty;
        requestType = string.IsNullOrWhiteSpace(requestType) ? requestType : QualifyTypeName(requestType);
        var responseType = handlerInterface.TypeArgumentList.Arguments.Skip(1).FirstOrDefault()?.ToString() ?? "void";
        responseType = string.IsNullOrWhiteSpace(responseType) ? responseType : QualifyTypeName(responseType);

        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        var handlerInfo = new HandlerInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className, requestType, responseType);

        var fieldLookup = fieldTypes.ToDictionary(pair => pair.Key.TrimStart('_'), pair => pair.Value, StringComparer.OrdinalIgnoreCase);

        foreach (var method in classDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
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
                if (memberAccess.Expression is IdentifierNameSyntax identifier)
                {
                var fieldName = identifier.Identifier.Text.TrimStart('_');
                if (fieldLookup.TryGetValue(fieldName, out var descriptor))
                {
                    var typeName = descriptor.Type;
                    var resolvedType = ResolveImplementationType(typeName) ?? typeName;
                    var invocation = memberAccess.Parent as InvocationExpressionSyntax;
                    if (IsConfigurationType(resolvedType) || IsConfigurationType(typeName))
                    {
                        if (invocation is not null && TryCaptureConfigurationUsage(memberAccess, invocation, resolvedType ?? typeName, tree) is { } configurationUsage)
                        {
                            handlerInfo.ConfigurationUsages.Add(configurationUsage);
                        }

                        continue;
                    }
                    if (IsCacheService(resolvedType) || IsCacheService(typeName))
                    {
                        var cacheType = IsCacheService(resolvedType) ? resolvedType : typeName;
                        if (TryCaptureCacheInvocation(memberAccess, invocation, cacheType, tree) is { } cacheInvocation)
                        {
                            handlerInfo.CacheInvocations.Add(cacheInvocation);
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
                            handlerInfo.LogInvocations.Add(new HandlerLogInvocation(level, line));
                            recordedUsage = true;
                        }
                    }

                    if (invocation is not null && IsGuardInvocation(memberAccess))
                    {
                        handlerInfo.ValidationCalls.Add(new HandlerValidationCall(memberAccess.Expression.ToString(), methodName ?? string.Empty, line));
                        recordedUsage = true;
                    }

                    if (IsClientType(baseTypeName) || IsClientType(resolvedBaseType))
                    {
                        var clientType = !string.Equals(resolvedBaseType, baseTypeName, StringComparison.Ordinal)
                            ? resolvedBaseType
                            : baseTypeName;
                        var httpMethod = methodName?.ToUpperInvariant() ?? string.Empty;
                        handlerInfo.HttpClientInvocations.Add(new HandlerClientInvocation(clientType, httpMethod, null, line));
                        recordedUsage = true;
                    }

                    if (TryResolveOptionsType(resolvedType) is { } resolvedOptionsType)
                    {
                        handlerInfo.OptionsUsages.Add(new OptionsUsage(resolvedOptionsType, line));
                        recordedUsage = true;
                    }
                    else if (TryResolveOptionsType(typeName) is { } fieldOptionsType)
                    {
                        handlerInfo.OptionsUsages.Add(new OptionsUsage(fieldOptionsType, line));
                        recordedUsage = true;
                    }

                        if (typeName.Contains("DbContext", StringComparison.Ordinal))
                        {
                            handlerInfo.DbContextAccesses.Add(new HandlerDbAccess(typeName, methodName ?? string.Empty, line));
                            continue;
                        }

                        if (typeName.Contains("Publisher", StringComparison.Ordinal))
                        {
                            string? messageType = null;
                            if (invocation is not null)
                            {
                                var argument = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                                messageType = argument switch
                                {
                                    ObjectCreationExpressionSyntax creation => creation.Type.ToString(),
                                    IdentifierNameSyntax identifierArgument => TryResolveExpressionType(identifierArgument, parameterTypes, localVariables),
                                    _ => null
                                };
                            }

                            handlerInfo.PublisherCalls.Add(new HandlerPublisherCall(typeName, methodName ?? string.Empty, line, messageType));
                            recordedUsage = true;
                        }
                        else if (resolvedType.EndsWith("Repository", StringComparison.Ordinal))
                        {
                            var operation = DetermineRepositoryOperation(methodName ?? string.Empty);
                            handlerInfo.RepositoryCalls.Add(new HandlerRepositoryCall(resolvedType, methodName ?? string.Empty, line, operation));
                            continue;
                        }
                        else if (typeName.Contains("IMapper", StringComparison.Ordinal) && memberAccess.Name is GenericNameSyntax mapperGeneric && mapperGeneric.Identifier.Text == "Map")
                        {
                            var destination = mapperGeneric.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                            var sourceExpression = invocation
                                ?.ArgumentList.Arguments.FirstOrDefault()?.Expression?.ToString();
                            var sourceType = sourceExpression is not null && parameterTypes.TryGetValue(sourceExpression, out var resolved)
                                ? resolved
                                : null;
                            handlerInfo.MapperCalls.Add(new HandlerMapperCall(sourceType, destination, line));
                            recordedUsage = true;
                        }
                        else if (typeName.Contains("IMediator", StringComparison.Ordinal) || typeName.Contains("IPublisher", StringComparison.Ordinal))
                        {
                            if (invocation is not null &&
                                methodName is { Length: > 0 } &&
                                methodName.StartsWith("Publish", StringComparison.Ordinal))
                            {
                                var argument = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                                var notificationType = argument switch
                                {
                                    ObjectCreationExpressionSyntax creation => creation.Type.ToString(),
                                    IdentifierNameSyntax identifierArgument => TryResolveExpressionType(identifierArgument, parameterTypes, localVariables),
                                    _ => null
                                };

                                if (!string.IsNullOrWhiteSpace(notificationType))
                                {
                                    handlerInfo.PublishedNotifications.Add(new HandlerNotificationPublication(notificationType!, line));
                                }
                                recordedUsage = true;
                            }
                        }

                        if (!recordedUsage)
                        {
                            var serviceType = NormalizeServiceType(resolvedType ?? typeName);
                            handlerInfo.ServiceUsages.Add(new ServiceUsage(serviceType, line, methodName));
                        }
                        else if (!resolvedType.EndsWith("Repository", StringComparison.Ordinal))
                        {
                            var serviceType = NormalizeServiceType(resolvedType ?? typeName);
                            handlerInfo.ServiceUsages.Add(new ServiceUsage(serviceType, line, methodName));
                        }
                    }
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
                    handlerInfo.ConfigurationUsages.Add(configurationUsage);
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
                        var line = GetLineNumber(tree, invocation);
                        handlerInfo.MapperCalls.Add(new HandlerMapperCall(sourceType, destination, line));
                    }
                }
                else if (extensionAccess.Name is GenericNameSyntax { Identifier.Text: "ProjectByIdAsync" } projectById)
                {
                    var destination = projectById.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                    var sourceType = TryResolveProjectionSource(extensionAccess.Expression, parameterTypes, localVariables, fieldLookup);
                    if (!string.IsNullOrWhiteSpace(destination))
                    {
                        var line = GetLineNumber(tree, invocation);
                        handlerInfo.MapperCalls.Add(new HandlerMapperCall(sourceType, destination, line));
                    }
                }
            }
        }

        _handlers[fqdn] = handlerInfo;
    }

    private string EnsureGuardNode(string guardType)
    {
        var assembly = GuessAssemblyName(guardType);
        var symbolId = $"T:{guardType}";
        var id = StableId.For("app.guard", guardType, assembly, symbolId);
        if (!_nodes.ContainsKey(id))
        {
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "app.guard",
                Name = guardType.Split('.').Last(),
                Fqdn = guardType,
                Assembly = assembly,
                Project = string.Empty,
                FilePath = string.Empty,
                Span = null,
                SymbolId = symbolId,
                Tags = new[] { "framework" }
            };
        }

        return id;
    }

    private static string? GetMemberName(SimpleNameSyntax nameSyntax)
    {
        return nameSyntax switch
        {
            IdentifierNameSyntax identifier => identifier.Identifier.Text,
            GenericNameSyntax generic => generic.Identifier.Text,
            _ => null
        };
    }

    private void PromoteDerivedRequests()
    {
        foreach (var candidate in _derivedRequestCandidates)
        {
            if (_requests.ContainsKey(candidate.Fqdn))
            {
                continue;
            }

            if (_requests.TryGetValue(candidate.BaseType, out var _) ||
                _requests.Values.Any(r => r.Name.Equals(candidate.BaseType.Split('.').Last(), StringComparison.Ordinal)))
            {
                _requests[candidate.Fqdn] = new RequestInfo(candidate.Fqdn, candidate.Assembly, candidate.Project, candidate.FilePath, candidate.Span, candidate.SymbolId, candidate.Name);
            }
        }
    }

    private void EmitRequests()
    {
        foreach (var request in _requests.Values)
        {
            var id = StableId.For("cqrs.request", request.Fqdn, request.Assembly, request.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "cqrs.request",
                Name = request.Name,
                Fqdn = request.Fqdn,
                Assembly = request.Assembly,
                Project = request.Project,
                FilePath = request.FilePath,
                Span = request.Span,
                SymbolId = request.SymbolId,
                Tags = new[] { "app" }
            };
        }
    }

    private void EmitHandlers()
    {
        foreach (var handler in _handlers.Values)
        {
            var id = StableId.For("cqrs.handler", handler.Fqdn, handler.Assembly, handler.SymbolId);
            Dictionary<string, object>? handlerProps = null;
            if (handler.LogInvocations.Count > 0)
            {
                handlerProps = new Dictionary<string, object>
                {
                    ["log_levels"] = handler.LogInvocations
                        .Select(l => l.Level)
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .OrderBy(level => level, StringComparer.OrdinalIgnoreCase)
                        .ToArray()
                };
            }

            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "cqrs.handler",
                Name = handler.Name,
                Fqdn = handler.Fqdn,
                Assembly = handler.Assembly,
                Project = handler.Project,
                FilePath = handler.FilePath,
                Span = handler.Span,
                SymbolId = handler.SymbolId,
                Tags = new[] { "app" },
                Props = handlerProps
            };

            if (FindRequestByType(handler.RequestType) is { } request)
            {
                var requestId = StableId.For("cqrs.request", request.Fqdn, request.Assembly, request.SymbolId);
                _edges.Add(new GraphEdge
                {
                    From = requestId,
                    To = id,
                    Kind = "handled_by",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "mediatr.handler",
                        MethodSpan = handler.Span
                    },
                    Evidence = CreateEvidence(handler.FilePath, handler.Span)
                });
            }

            foreach (var repositoryCall in handler.RepositoryCalls)
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
                            Type = "mediatr.handler",
                            Location = new GraphLocation { File = handler.FilePath, Line = repositoryCall.Line }
                        },
                        Props = new Dictionary<string, object>
                        {
                            ["method"] = repositoryCall.Method,
                            ["operation"] = repositoryCall.Operation
                        },
                        Evidence = CreateEvidence(handler.FilePath, repositoryCall.Line)
                    });
                }
            }

            foreach (var mapping in handler.MapperCalls)
            {
                if (string.IsNullOrWhiteSpace(mapping.DestinationType))
                {
                    continue;
                }

                if (!TryResolveNodeReference(mapping.DestinationType, out var destination))
                {
                    continue;
                }

                var props = new Dictionary<string, object>
                {
                    ["destination_type"] = mapping.DestinationType
                };

                if (!string.IsNullOrWhiteSpace(mapping.SourceType))
                {
                    props["source_type"] = mapping.SourceType!;
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
                        Location = new GraphLocation { File = handler.FilePath, Line = mapping.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(handler.FilePath, mapping.Line)
                });
            }

            foreach (var clientInvocation in handler.HttpClientInvocations
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
                            Location = new GraphLocation { File = handler.FilePath, Line = clientInvocation.Line }
                        },
                        Props = propsOrNull,
                        Evidence = CreateEvidence(handler.FilePath, clientInvocation.Line)
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
                            Location = new GraphLocation { File = handler.FilePath, Line = clientInvocation.Line }
                        },
                        Props = propsOrNull,
                        Evidence = CreateEvidence(handler.FilePath, clientInvocation.Line)
                    });
                }
            }

            foreach (var usage in handler.ServiceUsages
                .GroupBy(u => u.ServiceType, StringComparer.OrdinalIgnoreCase)
                .Select(group => group.OrderBy(u => u.Line).First()))
            {
                if (!TryEnsureServiceNode(usage.ServiceType, out var serviceId, out var registration))
                {
                    continue;
                }

                if (IsLoggerType(usage.ServiceType) && handler.LogInvocations.Count > 0)
                {
                    foreach (var log in handler.LogInvocations
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
                                Location = new GraphLocation { File = handler.FilePath, Line = log.Line }
                            },
                            Props = new Dictionary<string, object>
                            {
                                ["level"] = log.Level
                            },
                            Evidence = CreateEvidence(handler.FilePath, log.Line)
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
                            Location = new GraphLocation { File = handler.FilePath, Line = usage.Line }
                        },
                        Props = storageProps,
                        Evidence = CreateEvidence(handler.FilePath, usage.Line)
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

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = serviceId!,
                    Kind = "uses_service",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "ioc.resolve",
                        Location = new GraphLocation { File = handler.FilePath, Line = usage.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(handler.FilePath, usage.Line)
                });
            }

            foreach (var cache in handler.CacheInvocations)
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
                        Location = new GraphLocation { File = handler.FilePath, Line = cache.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(handler.FilePath, cache.Line)
                });
            }

            foreach (var validation in handler.ValidationCalls)
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
                        Location = new GraphLocation { File = handler.FilePath, Line = validation.Line }
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["method"] = validation.Method
                    },
                    Evidence = CreateEvidence(handler.FilePath, validation.Line)
                });
            }

            foreach (var optionsUsage in handler.OptionsUsages)
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
                        Location = new GraphLocation { File = handler.FilePath, Line = optionsUsage.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(handler.FilePath, optionsUsage.Line)
                });
            }

            EmitConfigurationEdges(id, handler.ConfigurationUsages);

            foreach (var notification in handler.PublishedNotifications)
            {
                var notificationInfo = FindNotificationByType(notification.NotificationType);
                if (notificationInfo is null)
                {
                    continue;
                }

                var notificationId = StableId.For("cqrs.notification", notificationInfo.Fqdn, notificationInfo.Assembly, notificationInfo.SymbolId);
                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = notificationId,
                    Kind = "publishes_notification",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "mediatr.publish",
                        Location = new GraphLocation { File = handler.FilePath, Line = notification.Line }
                    },
                    Evidence = CreateEvidence(handler.FilePath, notification.Line)
                });
            }
        }
    }
    private bool TryResolveHttpClient(string clientType, [NotNullWhen(true)] out HttpClientInfo? client)
    {
        if (_httpClients.TryGetValue(clientType, out var resolved))
        {
            client = resolved;
            return true;
        }

        var simple = clientType.Split('.').Last();
        if (_httpClients.TryGetValue(simple, out resolved))
        {
            client = resolved;
            return true;
        }

        var match = _httpClients.Values.FirstOrDefault(c =>
            c.Fqdn.Equals(clientType, StringComparison.OrdinalIgnoreCase) ||
            c.Name.Equals(simple, StringComparison.OrdinalIgnoreCase));

        if (match is not null)
        {
            client = match;
            return true;
        }

        client = null;
        return false;
    }
}
