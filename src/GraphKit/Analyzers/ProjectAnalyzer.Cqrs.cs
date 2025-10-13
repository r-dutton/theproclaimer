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
        var responseType = handlerInterface.TypeArgumentList.Arguments.Skip(1).FirstOrDefault()?.ToString() ?? "void";

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
                .ToDictionary(p => p.Identifier.Text, p => p.Type?.ToString(), StringComparer.OrdinalIgnoreCase);

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
                        if (IsCacheService(resolvedType) || IsCacheService(typeName))
                        {
                            var cacheType = IsCacheService(resolvedType) ? resolvedType : typeName;
                            if (TryCaptureCacheInvocation(memberAccess, memberAccess.Parent as InvocationExpressionSyntax, cacheType, tree) is { } cacheInvocation)
                            {
                                handlerInfo.CacheInvocations.Add(cacheInvocation);
                            }
                            continue;
                        }

                        var invocation = memberAccess.Parent as InvocationExpressionSyntax;
                        var invocationLineNode = (SyntaxNode?)invocation ?? memberAccess;
                        var line = GetLineNumber(tree, invocationLineNode);
                        var methodName = GetMemberName(memberAccess.Name);
                        var recordedUsage = false;

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
                            handlerInfo.RepositoryCalls.Add(new HandlerRepositoryCall(resolvedType, methodName ?? string.Empty, line));
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
                            var serviceType = resolvedType ?? typeName;
                            handlerInfo.ServiceUsages.Add(new ServiceUsage(serviceType, line, methodName));
                        }
                        else if (!resolvedType.EndsWith("Repository", StringComparison.Ordinal))
                        {
                            var serviceType = resolvedType ?? typeName;
                            handlerInfo.ServiceUsages.Add(new ServiceUsage(serviceType, line, methodName));
                        }
                    }
                }
            }
        }

        _handlers[fqdn] = handlerInfo;
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
                Tags = new[] { "app" }
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
                            ["method"] = repositoryCall.Method
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

            foreach (var usage in handler.ServiceUsages
                .GroupBy(u => u.ServiceType, StringComparer.OrdinalIgnoreCase)
                .Select(group => group.OrderBy(u => u.Line).First()))
            {
                if (!TryEnsureServiceNode(usage.ServiceType, out var serviceId, out var registration))
                {
                    continue;
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
}
