using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private void RegisterNotification(ProjectInfo project, SyntaxTree tree, TypeDeclarationSyntax declaration, string namespaceName)
    {
        var typeName = declaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? typeName : $"{namespaceName}.{typeName}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, declaration);
        var info = new NotificationInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, typeName, fqdn);
        _notifications[fqdn] = info;
    }

    private void AnalyzeNotificationHandler(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var notificationType = classDeclaration.BaseList?.Types
            .Select(t => t.Type)
            .OfType<GenericNameSyntax>()
            .FirstOrDefault(g => g.Identifier.Text == "INotificationHandler")?
            .TypeArgumentList.Arguments.FirstOrDefault()?.ToString() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(notificationType))
        {
            return;
        }

        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var span = ToGraphSpan(tree, classDeclaration);

        var handler = new NotificationHandlerInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className, notificationType);
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
                        handler.ConfigurationUsages.Add(configurationUsage);
                    }

                    continue;
                }
                if (IsCacheService(resolvedType) || IsCacheService(typeName))
                {
                    var cacheType = IsCacheService(resolvedType) ? resolvedType : typeName;
                    if (TryCaptureCacheInvocation(memberAccess, invocation, cacheType, tree) is { } cacheInvocation)
                    {
                        handler.CacheInvocations.Add(cacheInvocation);
                    }

                    continue;
                }

                var invocationNode = (SyntaxNode?)invocation ?? memberAccess;
                var line = GetLineNumber(tree, invocationNode);
                var methodName = GetMemberName(memberAccess.Name);
                var recordedUsage = false;

                if (TryResolveOptionsType(resolvedType) is { } resolvedOptionsType)
                {
                    handler.OptionsUsages.Add(new OptionsUsage(resolvedOptionsType, line));
                    recordedUsage = true;
                }
                else if (TryResolveOptionsType(typeName) is { } descriptorOptionsType)
                {
                    handler.OptionsUsages.Add(new OptionsUsage(descriptorOptionsType, line));
                    recordedUsage = true;
                }

                if (resolvedType.EndsWith("Repository", StringComparison.Ordinal))
                {
                    var operation = DetermineRepositoryOperation(methodName ?? string.Empty);
                    handler.RepositoryCalls.Add(new NotificationHandlerRepositoryCall(resolvedType, methodName ?? string.Empty, line, operation));
                    continue;
                }

                if (typeName.Contains("IMapper", StringComparison.Ordinal) && memberAccess.Name is GenericNameSyntax mapperGeneric && mapperGeneric.Identifier.Text == "Map")
                {
                    var destination = mapperGeneric.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                    var sourceExpression = invocation
                        ?.ArgumentList.Arguments.FirstOrDefault()?.Expression?.ToString();
                    var sourceType = sourceExpression is not null && parameterTypes.TryGetValue(sourceExpression, out var resolved)
                        ? resolved
                        : null;
                    handler.MapperCalls.Add(new HandlerMapperCall(sourceType, destination, line));
                    recordedUsage = true;
                }
                else if (typeName.Contains("IMediator", StringComparison.Ordinal) || typeName.Contains("IPublisher", StringComparison.Ordinal))
                {
                    if (invocation is not null && methodName is { Length: > 0 })
                    {
                        if (methodName.StartsWith("Send", StringComparison.Ordinal))
                        {
                            var argument = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                            var requestType = argument switch
                            {
                                ObjectCreationExpressionSyntax creation => creation.Type.ToString(),
                                IdentifierNameSyntax identifierArgument => TryResolveExpressionType(identifierArgument, parameterTypes, localVariables),
                                _ => null
                            };

                            if (!string.IsNullOrWhiteSpace(requestType))
                            {
                                handler.RequestInvocations.Add(new NotificationHandlerRequestInvocation(requestType!, line));
                            }
                            recordedUsage = true;
                        }
                        else if (methodName.StartsWith("Publish", StringComparison.Ordinal))
                        {
                            var argument = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                            var notification = argument switch
                            {
                                ObjectCreationExpressionSyntax creation => creation.Type.ToString(),
                                IdentifierNameSyntax identifierArgument => TryResolveExpressionType(identifierArgument, parameterTypes, localVariables),
                                _ => null
                            };

                            if (!string.IsNullOrWhiteSpace(notification))
                            {
                                handler.PublishedNotifications.Add(new HandlerNotificationPublication(notification!, line));
                            }
                            recordedUsage = true;
                        }
                    }
                }

                if (!recordedUsage)
                {
                    var serviceType = resolvedType ?? typeName;
                    handler.ServiceUsages.Add(new ServiceUsage(serviceType, line, methodName));
                }
                else if (!resolvedType.EndsWith("Repository", StringComparison.Ordinal))
                {
                    var serviceType = resolvedType ?? typeName;
                    handler.ServiceUsages.Add(new ServiceUsage(serviceType, line, methodName));
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
                    handler.ConfigurationUsages.Add(configurationUsage);
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
                        handler.MapperCalls.Add(new HandlerMapperCall(sourceType, destination, line));
                    }
                }
                else if (extensionAccess.Name is GenericNameSyntax { Identifier.Text: "ProjectByIdAsync" } projectById)
                {
                    var destination = projectById.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                    var sourceType = TryResolveProjectionSource(extensionAccess.Expression, parameterTypes, localVariables, fieldLookup);
                    if (!string.IsNullOrWhiteSpace(destination))
                    {
                        var line = GetLineNumber(tree, invocation);
                        handler.MapperCalls.Add(new HandlerMapperCall(sourceType, destination, line));
                    }
                }
            }
        }

        _notificationHandlers[fqdn] = handler;
    }

    private void EmitNotifications()
    {
        foreach (var notification in _notifications.Values)
        {
            var id = StableId.For("cqrs.notification", notification.Fqdn, notification.Assembly, notification.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "cqrs.notification",
                Name = notification.Name,
                Fqdn = notification.Fqdn,
                Assembly = notification.Assembly,
                Project = notification.Project,
                FilePath = notification.FilePath,
                Span = notification.Span,
                SymbolId = notification.SymbolId,
                Tags = new[] { "app", "event" },
                Props = new Dictionary<string, object>
                {
                    ["contract_type"] = notification.ContractType ?? notification.Fqdn
                }
            };
        }
    }

    private void EmitNotificationHandlers()
    {
        foreach (var handler in _notificationHandlers.Values)
        {
            var id = StableId.For("cqrs.notification_handler", handler.Fqdn, handler.Assembly, handler.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "cqrs.notification_handler",
                Name = handler.Name,
                Fqdn = handler.Fqdn,
                Assembly = handler.Assembly,
                Project = handler.Project,
                FilePath = handler.FilePath,
                Span = handler.Span,
                SymbolId = handler.SymbolId,
                Tags = new[] { "app", "event" }
            };

            if (FindNotificationByType(handler.NotificationType) is { } notificationInfo)
            {
                var notificationId = StableId.For("cqrs.notification", notificationInfo.Fqdn, notificationInfo.Assembly, notificationInfo.SymbolId);
                _edges.Add(new GraphEdge
                {
                    From = notificationId,
                    To = id,
                    Kind = "handled_by",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "mediatr.notification_handler",
                        MethodSpan = handler.Span
                    },
                    Evidence = CreateEvidence(handler.FilePath, handler.Span)
                });
            }

            foreach (var repositoryCall in handler.RepositoryCalls)
            {
                if (!TryResolveNodeReference(repositoryCall.RepositoryType, out var repository))
                {
                    continue;
                }

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = repository.Id,
                    Kind = "calls",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "notification.handler",
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

            foreach (var service in handler.ServiceUsages
                .GroupBy(u => u.ServiceType, StringComparer.OrdinalIgnoreCase)
                .Select(group => group.OrderBy(u => u.Line).First()))
            {
                if (!TryEnsureServiceNode(service.ServiceType, out var serviceId, out var registration))
                {
                    continue;
                }

                var props = new Dictionary<string, object>
                {
                    ["service_type"] = service.ServiceType
                };

                if (registration is not null)
                {
                    props["lifetime"] = registration.Lifetime;
                }

                if (!string.IsNullOrWhiteSpace(service.Method))
                {
                    props["method"] = service.Method!;
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
                        Location = new GraphLocation { File = handler.FilePath, Line = service.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(handler.FilePath, service.Line)
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

            EmitConfigurationEdges(id, handler.ConfigurationUsages);

            foreach (var request in handler.RequestInvocations)
            {
                var requestInfo = FindRequestByType(request.RequestType);
                if (requestInfo is null)
                {
                    continue;
                }

                var requestId = StableId.For("cqrs.request", requestInfo.Fqdn, requestInfo.Assembly, requestInfo.SymbolId);
                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = requestId,
                    Kind = "sends_request",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "mediatr.send",
                        Location = new GraphLocation { File = handler.FilePath, Line = request.Line }
                    },
                    Evidence = CreateEvidence(handler.FilePath, request.Line)
                });
            }

            foreach (var publication in handler.PublishedNotifications)
            {
                var publishedNotification = FindNotificationByType(publication.NotificationType);
                if (publishedNotification is null)
                {
                    continue;
                }

                var notificationId = StableId.For("cqrs.notification", publishedNotification.Fqdn, publishedNotification.Assembly, publishedNotification.SymbolId);
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
                        Location = new GraphLocation { File = handler.FilePath, Line = publication.Line }
                    },
                    Evidence = CreateEvidence(handler.FilePath, publication.Line)
                });
            }
        }
    }
}
