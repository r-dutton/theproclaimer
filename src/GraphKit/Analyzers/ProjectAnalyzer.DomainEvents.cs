using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.FlowAnalysis.Interprocedural;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using FlowAnalysisEngine = GraphKit.FlowAnalysis.Core.FlowAnalysis;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private void RegisterDomainEvent(ProjectInfo project, SyntaxTree tree, TypeDeclarationSyntax declaration, string? namespaceName)
    {
        var typeName = declaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? typeName : $"{namespaceName}.{typeName}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree);
        var span = ToGraphSpan(tree, declaration);

        var info = new DomainEventInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, typeName);
        _domainEvents[fqdn] = info;
    }

    private void AnalyzeDomainEventHandler(
        ProjectInfo project,
        SyntaxTree tree,
        ClassDeclarationSyntax classDeclaration,
        string? namespaceName,
        IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var eventType = classDeclaration.BaseList?.Types
            .Select(t => t.Type)
            .OfType<GenericNameSyntax>()
            .FirstOrDefault(g => string.Equals(g.Identifier.Text, "IHandle", StringComparison.Ordinal) ||
                                 string.Equals(g.Identifier.Text, "IHandleAsync", StringComparison.Ordinal))
            ?.TypeArgumentList.Arguments.FirstOrDefault()
            ?.ToString();

        if (string.IsNullOrWhiteSpace(eventType))
        {
            return;
        }

        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree);
        var span = ToGraphSpan(tree, classDeclaration);

        eventType = QualifyTypeName(eventType!, project.AssemblyName, project.RelativeDirectory);

        var info = new DomainEventHandlerInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className, eventType!);
        var model = project.GetModel(tree);
        var pointsTo = new FlowPointsToFacade();
        var valueContent = new FlowValueContentFacade();

        var fieldLookup = fieldTypes.ToDictionary(pair => pair.Key.TrimStart('_'), pair => pair.Value, StringComparer.OrdinalIgnoreCase);

        foreach (var method in classDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
            var parameterTypes = method.ParameterList.Parameters
                .Where(p => !string.IsNullOrWhiteSpace(p.Identifier.Text))
                .ToDictionary(p => p.Identifier.Text, p => p.Type?.ToString(), StringComparer.OrdinalIgnoreCase);

            var localVariables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var local in Descendants<LocalDeclarationStatementSyntax>(method))
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

            foreach (var memberAccess in Descendants<MemberAccessExpressionSyntax>(method))
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
                var resolvedType = ResolveImplementationType(typeName, info.Assembly, info.Project) ?? typeName;
                var invocation = memberAccess.Parent as InvocationExpressionSyntax;
                if (IsConfigurationType(resolvedType) || IsConfigurationType(typeName))
                {
                    if (invocation is not null && TryCaptureConfigurationUsage(memberAccess, invocation, resolvedType ?? typeName, tree) is { } configurationUsage)
                    {
                        info.ConfigurationUsages.Add(configurationUsage);
                    }

                    continue;
                }
                if (IsCacheService(resolvedType) || IsCacheService(typeName))
                {
                    var cacheType = IsCacheService(resolvedType) ? resolvedType : typeName;
                    if (TryCaptureCacheInvocation(memberAccess, invocation, cacheType, tree) is { } cacheInvocation)
                    {
                        info.CacheInvocations.Add(cacheInvocation);
                    }

                    continue;
                }

                var invocationNode = (SyntaxNode?)invocation ?? memberAccess;
                var line = GetLineNumber(tree, invocationNode);
                var methodName = GetMemberName(memberAccess.Name);
                var recordedUsage = false;

                if (TryResolveOptionsType(resolvedType) is { } resolvedOptionsType)
                {
                    info.OptionsUsages.Add(new OptionsUsage(resolvedOptionsType, line));
                    recordedUsage = true;
                }
                else if (TryResolveOptionsType(typeName) is { } descriptorOptionsType)
                {
                    info.OptionsUsages.Add(new OptionsUsage(descriptorOptionsType, line));
                    recordedUsage = true;
                }

                if (resolvedType.EndsWith("Repository", StringComparison.Ordinal))
                {
                    var operation = DetermineRepositoryOperation(methodName ?? string.Empty);
                    info.RepositoryCalls.Add(new NotificationHandlerRepositoryCall(resolvedType, methodName ?? string.Empty, line, operation));
                    continue;
                }

                if (typeName.Contains("IMapper", StringComparison.Ordinal) && memberAccess.Name is GenericNameSyntax mapperGeneric && mapperGeneric.Identifier.Text == "Map")
                {
                    var destination = mapperGeneric.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                    var sourceExpression = invocation?.ArgumentList.Arguments.FirstOrDefault()?.Expression?.ToString();
                    var sourceType = sourceExpression is not null && parameterTypes.TryGetValue(sourceExpression, out var resolved)
                        ? resolved
                        : null;
                    info.MapperCalls.Add(new HandlerMapperCall(sourceType, destination, line));
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
                                IdentifierNameSyntax identifierArgument => TryResolveExpressionType(identifierArgument, parameterTypes, localVariables, project.AssemblyName, project.RelativeDirectory, fieldLookup),
                                _ => null
                            };

                            if (!string.IsNullOrWhiteSpace(requestType))
                            {
                                info.RequestInvocations.Add(new NotificationHandlerRequestInvocation(requestType!, line));
                            }
                            recordedUsage = true;
                        }
                        else if (methodName.StartsWith("Publish", StringComparison.Ordinal))
                        {
                            var argument = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                            var notification = argument switch
                            {
                                ObjectCreationExpressionSyntax creation => creation.Type.ToString(),
                                IdentifierNameSyntax identifierArgument => TryResolveExpressionType(identifierArgument, parameterTypes, localVariables, project.AssemblyName, project.RelativeDirectory, fieldLookup),
                                _ => null
                            };

                            if (!string.IsNullOrWhiteSpace(notification))
                            {
                                info.PublishedNotifications.Add(new HandlerNotificationPublication(notification!, line));
                            }
                            recordedUsage = true;
                        }
                    }
                }

                if (!recordedUsage)
                {
                    var serviceType = resolvedType ?? typeName;
                    info.ServiceUsages.Add(new ServiceUsage(serviceType, line, methodName));
                }
                else if (!resolvedType.EndsWith("Repository", StringComparison.Ordinal))
                {
                    var serviceType = resolvedType ?? typeName;
                    info.ServiceUsages.Add(new ServiceUsage(serviceType, line, methodName));
                }
            }

            foreach (var elementAccess in Descendants<ElementAccessExpressionSyntax>(method))
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

                if (TryCaptureConfigurationIndexer(elementAccess, descriptor.Type, tree) is { } configurationUsage)
                {
                    info.ConfigurationUsages.Add(configurationUsage);
                }
            }

            IMethodSymbol? methodSymbol = null;
            try
            {
                methodSymbol = model.GetDeclaredSymbol(method) as IMethodSymbol;
            }
            catch (ArgumentException)
            {
                methodSymbol = null;
            }

            if (methodSymbol is not null &&
                methodSymbol.Name.StartsWith("Handle", StringComparison.OrdinalIgnoreCase))
            {
                if (!TryAcquireMethodAnalysis(methodSymbol))
                {
                    continue;
                }

                var visitor = new DomainEventsOperationVisitor(
                    this,
                    model,
                    info,
                    method.Identifier.Text,
                    pointsTo,
                    valueContent,
                    _facts);

                FlowAnalysisEngine.AnalyzeMethod(
                    project.Compilation,
                    model,
                    methodSymbol,
                    new FlowInterproceduralConfig(4, 2),
                    ShouldExpandForCqrsEfHttpMap,
                    visitor);
            }
        }

        _domainEventHandlers[fqdn] = info;
    }

    private void CaptureDomainEventPublications(
        ProjectInfo project,
        SyntaxTree tree,
        TypeDeclarationSyntax typeDeclaration,
        string? namespaceName,
        IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var typeName = typeDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? typeName : $"{namespaceName}.{typeName}";
        var filePath = GetRelativePath(tree);
        var assemblyName = project.AssemblyName;
        var projectPath = project.RelativeDirectory;

        var fieldLookup = new Dictionary<string, FieldDescriptor>(StringComparer.OrdinalIgnoreCase);
        foreach (var pair in fieldTypes)
        {
            var key = pair.Key?.TrimStart('_') ?? string.Empty;
            if (string.IsNullOrWhiteSpace(key))
            {
                continue;
            }

            fieldLookup[key] = pair.Value;
        }

        foreach (var method in typeDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
            CaptureDomainEventPublications(project, tree, fqdn, filePath, assemblyName, projectPath, fieldLookup, method, method.Identifier.Text);
        }

        foreach (var constructor in typeDeclaration.Members.OfType<ConstructorDeclarationSyntax>())
        {
            CaptureDomainEventPublications(project, tree, fqdn, filePath, assemblyName, projectPath, fieldLookup, constructor, ".ctor");
        }
    }

    private void CaptureDomainEventPublications(
        ProjectInfo project,
        SyntaxTree tree,
        string publisherFqdn,
        string filePath,
        string assemblyName,
        string projectPath,
        IReadOnlyDictionary<string, FieldDescriptor> fieldLookup,
        CSharpSyntaxNode methodNode,
        string methodName)
    {
        var parameterTypes = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
        if (methodNode is BaseMethodDeclarationSyntax baseMethod && baseMethod.ParameterList is not null)
        {
            foreach (var parameter in baseMethod.ParameterList.Parameters)
            {
                if (string.IsNullOrWhiteSpace(parameter.Identifier.Text))
                {
                    continue;
                }

                var type = parameter.Type?.ToString();
                if (string.IsNullOrWhiteSpace(type))
                {
                    continue;
                }

                parameterTypes[parameter.Identifier.Text] = QualifyTypeName(type!, assemblyName, projectPath);
            }
        }

        var localVariables = CollectLocalVariables(methodNode);

        foreach (var invocation in Descendants<InvocationExpressionSyntax>(methodNode))
        {
            if (invocation.Expression is not MemberAccessExpressionSyntax access)
            {
                continue;
            }

            if (!string.Equals(access.Name.Identifier.Text, "Raise", StringComparison.Ordinal))
            {
                continue;
            }

            if (!IsDomainEventsAccess(access.Expression))
            {
                continue;
            }

            if (invocation.ArgumentList?.Arguments.Count == 0)
            {
                continue;
            }

            var eventExpression = invocation.ArgumentList.Arguments[0].Expression;
            var eventType = ResolveDomainEventType(eventExpression, parameterTypes, localVariables, fieldLookup, assemblyName, projectPath);
            if (string.IsNullOrWhiteSpace(eventType))
            {
                continue;
            }

            var resolvedEvent = FindDomainEventByType(eventType!);
            eventType = resolvedEvent?.Fqdn ?? QualifyTypeName(eventType!, assemblyName, projectPath);
            var line = GetLineNumber(tree, invocation);
            _domainEventPublications.Add(new DomainEventPublication(publisherFqdn, assemblyName, projectPath, filePath, methodName, line, eventType!));
        }
    }

    private static bool IsDomainEventsAccess(ExpressionSyntax expression)
    {
        switch (expression)
        {
            case IdentifierNameSyntax identifier:
                return string.Equals(identifier.Identifier.Text, "DomainEvents", StringComparison.Ordinal);
            case MemberAccessExpressionSyntax memberAccess:
                return string.Equals(memberAccess.Name.Identifier.Text, "DomainEvents", StringComparison.Ordinal) ||
                       IsDomainEventsAccess(memberAccess.Expression);
            default:
                return false;
        }
    }

    private string? ResolveDomainEventType(
        ExpressionSyntax expression,
        IReadOnlyDictionary<string, string?> parameterTypes,
        Dictionary<string, string> localVariables,
        IReadOnlyDictionary<string, FieldDescriptor> fieldLookup,
        string assemblyName,
        string projectPath)
    {
        switch (expression)
        {
            case ObjectCreationExpressionSyntax creation:
                return creation.Type.ToString();
            case InvocationExpressionSyntax nestedInvocation when nestedInvocation.Expression is MemberAccessExpressionSyntax nestedAccess &&
                                                                  string.Equals(nestedAccess.Name.Identifier.Text, "Create", StringComparison.Ordinal):
                return TryResolveExpressionType(nestedInvocation.Expression, parameterTypes, localVariables, assemblyName, projectPath, fieldLookup);
            default:
                return TryResolveExpressionType(expression, parameterTypes, localVariables, assemblyName, projectPath, fieldLookup);
        }
    }

    private static Dictionary<string, string> CollectLocalVariables(CSharpSyntaxNode node)
    {
        var locals = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var local in Descendants<LocalDeclarationStatementSyntax>(node))
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

                if (string.IsNullOrWhiteSpace(resolvedType))
                {
                    continue;
                }

                locals[variable.Identifier.Text] = resolvedType;
            }
        }

        return locals;
    }

    private void EnsureDomainEventPlaceholders()
    {
        foreach (var publication in _domainEventPublications)
        {
            if (FindDomainEventByType(publication.EventType) is not null)
            {
                continue;
            }

            var simple = GetSimpleIdentifier(publication.EventType) ?? publication.EventType;
            var placeholderSpan = new GraphSpan
            {
                StartLine = publication.Line > 0 ? publication.Line : 0,
                EndLine = publication.Line > 0 ? publication.Line : 0
            };

            var placeholder = new DomainEventInfo(
                publication.EventType,
                publication.PublisherAssembly,
                publication.PublisherProject,
                publication.FilePath,
                placeholderSpan,
                $"T:{publication.EventType}",
                simple);

            _domainEvents.TryAdd(publication.EventType, placeholder);
        }
    }

    private void EmitDomainEvents()
    {
        EnsureDomainEventPlaceholders();

        foreach (var domainEvent in _domainEvents.Values)
        {
            var id = StableId.For("domain.event", domainEvent.Fqdn, domainEvent.Assembly, domainEvent.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "domain.event",
                Name = domainEvent.Name,
                Fqdn = domainEvent.Fqdn,
                Assembly = domainEvent.Assembly,
                Project = domainEvent.Project ?? string.Empty,
                FilePath = domainEvent.FilePath ?? string.Empty,
                Span = domainEvent.Span,
                SymbolId = domainEvent.SymbolId,
                Tags = new[] { "domain", "event" }
            };
        }
    }

    private void EmitDomainEventHandlers()
    {
        foreach (var handler in _domainEventHandlers.Values)
        {
            EnsureDomainEventHandlerFactNode(handler);
            var id = StableId.For("domain.event_handler", handler.Fqdn, handler.Assembly, handler.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "domain.event_handler",
                Name = handler.Name,
                Fqdn = handler.Fqdn,
                Assembly = handler.Assembly,
                Project = handler.Project ?? string.Empty,
                FilePath = handler.FilePath ?? string.Empty,
                Span = handler.Span,
                SymbolId = handler.SymbolId,
                Tags = new[] { "domain", "event" }
            };

            if (FindDomainEventByType(handler.EventType) is { } domainEvent)
            {
                var eventId = StableId.For("domain.event", domainEvent.Fqdn, domainEvent.Assembly, domainEvent.SymbolId);
                _edges.Add(new GraphEdge
                {
                    From = eventId,
                    To = id,
                    Kind = "handled_by",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "domain.event_handler",
                        MethodSpan = handler.Span
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["event_type"] = handler.EventType
                    },
                    Evidence = CreateEvidence(handler.FilePath ?? string.Empty, handler.Span ?? new GraphSpan { StartLine = 0, EndLine = 0 })
                });
            }

            foreach (var repositoryCall in handler.RepositoryCalls)
            {
                if (!TryResolveNodeReference(repositoryCall.RepositoryType, out var repository, handler.Assembly, handler.Project))
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
                        Type = "domain.event_handler",
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

                if (!TryResolveNodeReference(mapping.DestinationType, out var destination, handler.Assembly, handler.Project))
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
                if (!TryEnsureServiceNode(service.ServiceType, out var serviceId, out var registration, service.TargetType, handler.Assembly, handler.Project))
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

                if (!string.IsNullOrWhiteSpace(service.TargetType))
                {
                    props["target_type"] = service.TargetType!;
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

            foreach (var request in handler.RequestInvocations)
            {
                if (!TryResolveNodeReference(request.RequestType, out var requestNode, handler.Assembly, handler.Project))
                {
                    continue;
                }

                var requestProps = new Dictionary<string, object>
                {
                    ["request_type"] = request.RequestType
                };

                if (FindRequestByType(request.RequestType, preferredAssembly: handler.Assembly, preferredProject: handler.Project) is { } requestInfo &&
                    !string.IsNullOrWhiteSpace(requestInfo.ResponseType) &&
                    !IsGenericPlaceholder(requestInfo.ResponseType))
                {
                    requestProps["response_type"] = requestInfo.ResponseType!;
                }

                var pipelineLabels = ResolvePipelineBehaviorsForRequest(request.RequestType);
                if (pipelineLabels.Count > 0)
                {
                    requestProps["pipeline_behaviors"] = string.Join(", ", pipelineLabels);
                }

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = requestNode.Id,
                    Kind = "sends_request",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "domain.event_handler",
                        Location = new GraphLocation { File = handler.FilePath, Line = request.Line }
                    },
                    Props = requestProps,
                    Evidence = CreateEvidence(handler.FilePath, request.Line)
                });
            }

            foreach (var publication in handler.PublishedNotifications)
            {
                if (!TryResolveNodeReference(publication.NotificationType, out var notificationNode, handler.Assembly, handler.Project))
                {
                    continue;
                }

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = notificationNode.Id,
                    Kind = "publishes_notification",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "domain.event_handler",
                        Location = new GraphLocation { File = handler.FilePath, Line = publication.Line }
                    },
                    Evidence = CreateEvidence(handler.FilePath, publication.Line)
                });
            }

            foreach (var cache in handler.CacheInvocations)
            {
                if (!TryResolveNodeReference(cache.CacheType, out var cacheNode, handler.Assembly, handler.Project))
                {
                    continue;
                }

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
                    To = cacheNode.Id,
                    Kind = "uses_cache",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "domain.event_handler",
                        Location = new GraphLocation { File = handler.FilePath, Line = cache.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(handler.FilePath, cache.Line)
                });
            }

            foreach (var option in handler.OptionsUsages)
            {
                if (!TryResolveNodeReference(option.OptionsType, out var optionsNode, handler.Assembly, handler.Project))
                {
                    continue;
                }

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = optionsNode.Id,
                    Kind = "uses_options",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "domain.event_handler",
                        Location = new GraphLocation { File = handler.FilePath, Line = option.Line }
                    },
                    Evidence = CreateEvidence(handler.FilePath, option.Line)
                });
            }

            foreach (var config in handler.ConfigurationUsages)
            {
                if (!TryResolveNodeReference(config.ConfigurationType, out var configNode, handler.Assembly, handler.Project))
                {
                    continue;
                }

                var props = new Dictionary<string, object>
                {
                    ["accessor"] = config.Accessor
                };

                if (!string.IsNullOrWhiteSpace(config.Key))
                {
                    props["key"] = config.Key;
                }

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = configNode.Id,
                    Kind = "uses_configuration",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "domain.event_handler",
                        Location = new GraphLocation { File = handler.FilePath, Line = config.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(handler.FilePath, config.Line)
                });
            }
        }
    }

    private void EmitDomainEventPublications()
    {
        if (_domainEventPublications.Count == 0)
        {
            return;
        }

        var seen = new HashSet<(string PublisherId, string EventId, int Line, string? Method)>();

        foreach (var publication in _domainEventPublications)
        {
            if (FindDomainEventByType(publication.EventType) is not { } domainEvent)
            {
                continue;
            }

            if (!TryResolveDomainEventPublisher(publication.PublisherType, publication.PublisherAssembly, publication.PublisherProject, out var publisherReference))
            {
                continue;
            }

            var eventId = StableId.For("domain.event", domainEvent.Fqdn, domainEvent.Assembly, domainEvent.SymbolId);
            var publisherId = publisherReference.Id;

            if (!seen.Add((publisherId, eventId, publication.Line, publication.MethodName)))
            {
                continue;
            }

            var props = new Dictionary<string, object>
            {
                ["event_type"] = publication.EventType
            };

            if (!string.IsNullOrWhiteSpace(publication.MethodName))
            {
                props["method"] = publication.MethodName!;
            }

            _edges.Add(new GraphEdge
            {
                From = publisherId,
                To = eventId,
                Kind = "publishes_domain_event",
                Source = "static",
                Confidence = 1.0,
                Transform = new GraphTransform
                {
                    Type = "domain.event",
                    Location = new GraphLocation
                    {
                        File = publication.FilePath,
                        Line = publication.Line
                    }
                },
                Props = props,
                Evidence = CreateEvidence(publication.FilePath, publication.Line)
            });
        }
    }

    private bool TryResolveDomainEventPublisher(string publisherType, string? publisherAssembly, string? publisherProject, out NodeReference reference)
    {
        reference = default!;
        if (string.IsNullOrWhiteSpace(publisherType))
        {
            return false;
        }

        if (TryResolveNodeReference(publisherType, out reference, publisherAssembly, publisherProject))
        {
            return true;
        }

        var simple = GetSimpleIdentifier(publisherType);
        if (!string.IsNullOrWhiteSpace(simple) && TryResolveNodeReference(simple!, out reference, publisherAssembly, publisherProject))
        {
            return true;
        }

        return false;
    }
}
