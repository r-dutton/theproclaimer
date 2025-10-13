using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private void AnalyzeController(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree.FilePath);
        var controllerRoute = ResolveRoute(classDeclaration.AttributeLists, className);

        var fieldLookup = fieldTypes.ToDictionary(pair => pair.Key.TrimStart('_'), pair => pair.Value, StringComparer.OrdinalIgnoreCase);

        foreach (var method in classDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
            if (!method.Modifiers.Any(m => m.Text is "public" or "async"))
            {
                continue;
            }

            var parameterTypes = method.ParameterList.Parameters
                .Where(p => !string.IsNullOrWhiteSpace(p.Identifier.Text))
                .ToDictionary(p => p.Identifier.Text, p => p.Type?.ToString(), StringComparer.OrdinalIgnoreCase);

            var methodName = method.Identifier.Text;
            var actionFqdn = $"{fqdn}.{methodName}";
            var methodSymbolId = $"M:{fqdn}.{methodName}";
            var methodSpan = ToGraphSpan(tree, method);
            var httpAttr = method.AttributeLists.SelectMany(list => list.Attributes)
                .FirstOrDefault(attr => attr.Name.ToString().StartsWith("Http", StringComparison.Ordinal));
            var httpMethod = httpAttr?.Name.ToString().Replace("Http", string.Empty, StringComparison.OrdinalIgnoreCase).ToUpperInvariant() ?? "GET";
            var methodRoute = ResolveRoute(method.AttributeLists, className);
            var route = methodRoute ?? controllerRoute ?? "/";
            if (methodRoute is not null && controllerRoute is not null &&
                (!methodRoute.StartsWith("/", StringComparison.Ordinal) || !methodRoute.StartsWith(controllerRoute, StringComparison.OrdinalIgnoreCase)))
            {
                route = NormalizeRoute(controllerRoute.TrimEnd('/') + "/" + methodRoute.TrimStart('/'));
            }

            var info = new ControllerActionInfo(actionFqdn, project.AssemblyName, project.RelativeDirectory, filePath, methodSpan, methodSymbolId, methodName, route, httpMethod, symbolId);

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

                    info.LocalVariables[variable.Identifier.Text] = resolvedType;
                }
            }


            foreach (var invocation in method.DescendantNodes().OfType<InvocationExpressionSyntax>())
            {
                if (invocation.Expression is MemberAccessExpressionSyntax memberAccess)
                {
                    var methodIdentifier = memberAccess.Name.Identifier.Text;
                    var isMediatorInvocation = methodIdentifier.Equals("Send", StringComparison.Ordinal) || methodIdentifier.Equals("SendAsync", StringComparison.Ordinal);
                    var isRequestProcessorInvocation = methodIdentifier.Equals("Process", StringComparison.Ordinal) || methodIdentifier.Equals("ProcessAsync", StringComparison.Ordinal);

                    if (isRequestProcessorInvocation)
                    {
                        var targetType = TryResolveExpressionType(memberAccess.Expression, parameterTypes, info.LocalVariables);
                        if (targetType is null && memberAccess.Expression is IdentifierNameSyntax targetIdentifier &&
                            fieldLookup.TryGetValue(targetIdentifier.Identifier.Text.TrimStart('_'), out var descriptor))
                        {
                            targetType = descriptor.Type;
                        }
                        else if (targetType is null && memberAccess.Expression is MemberAccessExpressionSyntax nestedAccess &&
                                 nestedAccess.Name is IdentifierNameSyntax nestedIdentifier &&
                                 fieldLookup.TryGetValue(nestedIdentifier.Identifier.Text.TrimStart('_'), out var nestedDescriptor))
                        {
                            targetType = nestedDescriptor.Type;
                        }

                        if (targetType is not null)
                        {
                            var resolvedTarget = ResolveImplementationType(targetType);
                            if (!IsRequestProcessorType(resolvedTarget) && !IsRequestProcessorType(targetType))
                            {
                                isRequestProcessorInvocation = false;
                            }
                        }
                        else
                        {
                            isRequestProcessorInvocation = false;
                        }
                    }

                    if (isMediatorInvocation || isRequestProcessorInvocation)
                    {
                        var argumentExpression = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                        var requestType = argumentExpression is null
                            ? null
                            : TryResolveExpressionType(argumentExpression, parameterTypes, info.LocalVariables);

                        if (requestType is null && argumentExpression is IdentifierNameSyntax identifierArgument &&
                            info.LocalVariables.TryGetValue(identifierArgument.Identifier.Text, out var resolvedType) &&
                            !string.Equals(resolvedType, "var", StringComparison.OrdinalIgnoreCase))
                        {
                            requestType = resolvedType;
                        }

                        if (requestType is null && argumentExpression is IdentifierNameSyntax fieldIdentifier &&
                            fieldLookup.TryGetValue(fieldIdentifier.Identifier.Text.TrimStart('_'), out var fieldDescriptor))
                        {
                            requestType = fieldDescriptor.Type;
                        }

                        if (!string.IsNullOrWhiteSpace(requestType))
                        {
                            var line = GetLineNumber(tree, invocation);
                            info.RequestInvocations.Add(new ControllerRequestInvocation(requestType!, line));

                            var handler = FindHandlerForRequest(requestType!);
                            if (handler is not null)
                            {
                                if (invocation.Parent is AssignmentExpressionSyntax { Left: IdentifierNameSyntax assignTarget })
                                {
                                    info.LocalVariables[assignTarget.Identifier.Text] = handler.ResponseType;
                                }
                                else if (invocation.Parent is AwaitExpressionSyntax awaitExpression &&
                                         awaitExpression.Parent is AssignmentExpressionSyntax { Left: IdentifierNameSyntax awaitAssign })
                                {
                                    info.LocalVariables[awaitAssign.Identifier.Text] = handler.ResponseType;
                                }
                                else if (invocation.Parent is EqualsValueClauseSyntax equals && equals.Parent is VariableDeclaratorSyntax declarator)
                                {
                                    info.LocalVariables[declarator.Identifier.Text] = handler.ResponseType;
                                }
                            }
                        }
                    }
                }

                if (invocation.Expression is MemberAccessExpressionSyntax { Expression: IdentifierNameSyntax identifier } access)
                {
                    var fieldName = identifier.Identifier.Text.TrimStart('_');
                    if (fieldLookup.TryGetValue(fieldName, out var descriptor))
                    {
                        var typeName = descriptor.Type;
                        var baseTypeName = GetTypeNameWithoutGenerics(typeName);
                        var resolvedType = ResolveImplementationType(typeName) ?? typeName;
                        if (string.IsNullOrWhiteSpace(resolvedType))
                        {
                            continue;
                        }
                        var resolvedBaseType = GetTypeNameWithoutGenerics(resolvedType);
                        SyntaxNode invocationNode = invocation;
                        var serviceLine = GetLineNumber(tree, invocationNode);
                        var serviceMethod = GetMemberName(access.Name);
                        var serviceTypeName = resolvedType;
                        var recordedServiceUsage = false;
                        if (IsClientType(baseTypeName) || IsClientType(resolvedBaseType))
                        {
                            var callMethod = access.Name.Identifier.Text.ToUpperInvariant();
                            var routeLiteral = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                            var relativePath = ExtractRouteLiteral(tree, routeLiteral);
                            var clientType = !string.Equals(resolvedBaseType, baseTypeName, StringComparison.Ordinal)
                                ? resolvedBaseType
                                : baseTypeName;
                            var line = GetLineNumber(tree, invocation);
                            info.HttpClientInvocations.Add(new ControllerClientInvocation(clientType, callMethod, relativePath, line));
                        }
                        else if (typeName.Contains("IMapper", StringComparison.Ordinal) && access.Name is GenericNameSyntax mapperGeneric && mapperGeneric.Identifier.Text == "Map")
                        {
                            var destination = mapperGeneric.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                            var sourceExpression = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression?.ToString();
                            var sourceType = sourceExpression is not null && parameterTypes.TryGetValue(sourceExpression, out var resolved)
                                ? resolved
                                : null;
                            var line = GetLineNumber(tree, invocation);
                            var assignedVariable = TryResolveAssignedVariable(invocation);
                            if (!string.IsNullOrWhiteSpace(assignedVariable) && !string.IsNullOrWhiteSpace(destination))
                            {
                                info.LocalVariables[assignedVariable!] = destination!;
                            }

                            info.MappingInvocations.Add(new ControllerMappingInvocation(sourceType, destination, assignedVariable, line));
                            recordedServiceUsage = true;
                        }
                        else if (typeName.Contains("IMapper", StringComparison.Ordinal) && access.Name is IdentifierNameSyntax { Identifier.Text: "Map" })
                        {
                            var destination = invocation.ArgumentList.Arguments.Skip(1).FirstOrDefault()?.Expression?.ToString();
                            var sourceExpression = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression?.ToString();
                            var sourceType = sourceExpression is not null && parameterTypes.TryGetValue(sourceExpression, out var resolved)
                                ? resolved
                                : null;
                            var line = GetLineNumber(tree, invocation);
                            var assignedVariable = TryResolveAssignedVariable(invocation);
                            if (!string.IsNullOrWhiteSpace(assignedVariable) && !string.IsNullOrWhiteSpace(destination))
                            {
                                info.LocalVariables[assignedVariable!] = destination!;
                            }

                            info.MappingInvocations.Add(new ControllerMappingInvocation(sourceType, destination, assignedVariable, line));
                            recordedServiceUsage = true;
                        }
                        else if (typeName.StartsWith("IValidator", StringComparison.OrdinalIgnoreCase))
                        {
                            var line = GetLineNumber(tree, invocation);
                            var validatorType = ExtractGenericArgument(typeName) ?? string.Empty;
                            if (!string.IsNullOrWhiteSpace(validatorType))
                            {
                                info.ValidatorInvocations.Add(new ControllerValidatorInvocation(validatorType, line));
                            }
                        }
                        else if (IsCacheService(resolvedType) || IsCacheService(typeName))
                        {
                            var cacheType = IsCacheService(resolvedType) ? resolvedType : typeName;
                            if (TryCaptureCacheInvocation(access, invocation, cacheType, tree) is { } cacheInvocation)
                            {
                                info.CacheInvocations.Add(cacheInvocation);
                            }
                        }
                        else if (IsRepositoryType(resolvedType) || IsRepositoryType(typeName))
                        {
                            var repositoryType = IsRepositoryType(resolvedType) ? resolvedType : typeName;
                            if (TryCaptureRepositoryInvocation(access, invocation, repositoryType, typeName, tree) is { } repositoryInvocation)
                            {
                                info.RepositoryInvocations.Add(repositoryInvocation);
                            }
                            recordedServiceUsage = true;
                        }
                        else if (TryResolveOptionsType(resolvedType) is { } optionsType)
                        {
                            info.OptionsUsages.Add(new OptionsUsage(optionsType, serviceLine));
                            recordedServiceUsage = true;
                        }
                        else if (IsServiceType(resolvedType) || IsServiceType(typeName))
                        {
                            recordedServiceUsage = true;
                        }
                        else if (typeName.Contains("IMediator", StringComparison.Ordinal) || typeName.Contains("IPublisher", StringComparison.Ordinal))
                        {
                            var methodIdentifier = access.Name.Identifier.Text;
                            if (methodIdentifier.StartsWith("Publish", StringComparison.Ordinal))
                            {
                                var argument = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                                var notificationType = argument switch
                                {
                                    ObjectCreationExpressionSyntax creation => creation.Type.ToString(),
                                    IdentifierNameSyntax identifierArgument => TryResolveExpressionType(identifierArgument, parameterTypes, info.LocalVariables),
                                    _ => null
                                };

                                if (!string.IsNullOrWhiteSpace(notificationType))
                                {
                                    info.NotificationInvocations.Add(new ControllerNotificationInvocation(notificationType!, serviceLine));
                                }

                                recordedServiceUsage = true;
                            }
                        }

                        if (recordedServiceUsage)
                        {
                            info.ServiceUsages.Add(new ServiceUsage(serviceTypeName, serviceLine, serviceMethod));
                        }
                    }
                }
                else if (invocation.Expression is MemberAccessExpressionSyntax extensionAccess)
                {
                    if (extensionAccess.Name is GenericNameSyntax { Identifier.Text: "ProjectTo" } projectTo)
                    {
                        var destination = projectTo.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                        if (!string.IsNullOrWhiteSpace(destination))
                        {
                            var line = GetLineNumber(tree, invocation);
                            info.MappingInvocations.Add(new ControllerMappingInvocation(null, destination, null, line));
                        }
                    }
                    else if (extensionAccess.Name is GenericNameSyntax { Identifier.Text: "ProjectByIdAsync" } projectById)
                    {
                        var destination = projectById.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                        if (!string.IsNullOrWhiteSpace(destination))
                        {
                            var line = GetLineNumber(tree, invocation);
                            info.MappingInvocations.Add(new ControllerMappingInvocation(null, destination, null, line));
                        }
                    }
                }
            }
            foreach (var binary in method.DescendantNodes().OfType<BinaryExpressionSyntax>())
            {
                if (binary.IsKind(SyntaxKind.AsExpression) && binary.Right is TypeSyntax asType)
                {
                    var destinationType = asType.ToString();
                    var sourceType = TryResolveExpressionType(binary.Left, parameterTypes, info.LocalVariables);
                    if (string.IsNullOrWhiteSpace(destinationType) || string.IsNullOrWhiteSpace(sourceType))
                    {
                        continue;
                    }

                    var variable = TryResolveAssignedVariable(binary);
                    if (!string.IsNullOrWhiteSpace(variable))
                    {
                        info.LocalVariables[variable!] = destinationType;
                    }

                    var line = GetLineNumber(tree, binary);
                    info.CastInvocations.Add(new ControllerCastInvocation(sourceType!, destinationType, variable, line, "as"));
                }
                else if (binary.IsKind(SyntaxKind.IsExpression) && binary.Right is TypeSyntax isType)
                {
                    var destinationType = isType.ToString();
                    var sourceType = TryResolveExpressionType(binary.Left, parameterTypes, info.LocalVariables);
                    if (string.IsNullOrWhiteSpace(destinationType) || string.IsNullOrWhiteSpace(sourceType))
                    {
                        continue;
                    }

                    var line = GetLineNumber(tree, binary);
                    info.CastInvocations.Add(new ControllerCastInvocation(sourceType!, destinationType, null, line, "is"));
                }
            }

            foreach (var pattern in method.DescendantNodes().OfType<IsPatternExpressionSyntax>())
            {
                var corePattern = pattern.Pattern is UnaryPatternSyntax unary ? unary.Pattern : pattern.Pattern;

                var destinationType = corePattern switch
                {
                    DeclarationPatternSyntax declaration => declaration.Type.ToString(),
                    RecursivePatternSyntax recursive => recursive.Type?.ToString(),
                    _ => null
                };

                if (string.IsNullOrWhiteSpace(destinationType))
                {
                    continue;
                }

                var sourceType = TryResolveExpressionType(pattern.Expression, parameterTypes, info.LocalVariables);
                if (string.IsNullOrWhiteSpace(sourceType))
                {
                    continue;
                }

                string? variable = corePattern switch
                {
                    DeclarationPatternSyntax { Designation: SingleVariableDesignationSyntax designation } => designation.Identifier.Text,
                    VarPatternSyntax { Designation: SingleVariableDesignationSyntax varDesignation } => varDesignation.Identifier.Text,
                    RecursivePatternSyntax { Designation: SingleVariableDesignationSyntax recursiveDesignation } => recursiveDesignation.Identifier.Text,
                    _ => null
                };

                if (!string.IsNullOrWhiteSpace(variable))
                {
                    info.LocalVariables[variable!] = destinationType!;
                }

                var line = GetLineNumber(tree, pattern);
                info.CastInvocations.Add(new ControllerCastInvocation(sourceType!, destinationType!, variable, line, "is"));
            }

            _controllerActions[actionFqdn] = info;
            var routeKey = $"{info.HttpMethod}:{CanonicalizeRoute(info.Route)}";
            var routeBag = _controllerRoutes.GetOrAdd(routeKey, _ => new ConcurrentBag<ControllerActionInfo>());
            routeBag.Add(info);
        }
    }

    private void EmitControllers()
    {
        foreach (var action in _controllerActions.Values)
        {
            var id = StableId.For("endpoint.controller", action.Fqdn, action.Assembly, action.SymbolId);
            var node = new GraphNode
            {
                Id = id,
                Type = "endpoint.controller",
                Name = action.Name,
                Fqdn = action.Fqdn,
                Assembly = action.Assembly,
                Project = action.Project,
                FilePath = action.FilePath,
                Span = action.Span,
                SymbolId = action.SymbolId,
                Tags = new[] { "web" },
                Props = new Dictionary<string, object>
                {
                    ["route"] = action.Route,
                    ["http_method"] = action.HttpMethod
                }
            };
            _nodes[id] = node;


            foreach (var request in action.RequestInvocations)
            {
                if (FindRequestByType(request.RequestType) is { } requestInfo)
                {
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
                            Type = "controller.action",
                            Location = new GraphLocation { File = action.FilePath, Line = request.Line }
                        },
                        Props = new Dictionary<string, object>
                        {
                            ["request_type"] = request.RequestType
                        },
                        Evidence = CreateEvidence(action.FilePath, request.Line)
                    });
                }

                if (FindHandlerForRequest(request.RequestType) is { } handler)
                {
                    var handlerId = StableId.For("cqrs.handler", handler.Fqdn, handler.Assembly, handler.SymbolId);
                    _edges.Add(new GraphEdge
                    {
                        From = id,
                        To = handlerId,
                        Kind = "handled_by",
                        Source = "static",
                        Confidence = 1.0,
                        Transform = new GraphTransform
                        {
                            Type = "controller.action",
                            Location = new GraphLocation { File = action.FilePath, Line = request.Line }
                        },
                        Evidence = CreateEvidence(action.FilePath, request.Line)
                    });
                }
            }

            foreach (var repository in action.RepositoryInvocations
                .GroupBy(r => new { r.RepositoryType, r.Method, r.Operation, r.EntityType })
                .Select(group => group.OrderBy(r => r.Line).First()))
            {
                NodeReference? repositoryReference = null;
                if (!string.IsNullOrWhiteSpace(repository.RepositoryType) &&
                    TryResolveNodeReference(repository.RepositoryType, out var directReference))
                {
                    repositoryReference = directReference;
                }
                else if (!string.IsNullOrWhiteSpace(repository.EntityType))
                {
                    var derivedRepositoryType = repository.EntityType.EndsWith("Repository", StringComparison.Ordinal)
                        ? repository.EntityType
                        : $"{repository.EntityType}Repository";
                    if (TryResolveNodeReference(derivedRepositoryType, out var derivedReference))
                    {
                        repositoryReference = derivedReference;
                    }
                }

                if (repositoryReference is not null)
                {
                    var props = new Dictionary<string, object>
                    {
                        ["method"] = repository.Method
                    };

                    _edges.Add(new GraphEdge
                    {
                        From = id,
                        To = repositoryReference.Id,
                        Kind = "calls",
                        Source = "static",
                        Confidence = 1.0,
                        Transform = new GraphTransform
                        {
                            Type = "controller.repository",
                            Location = new GraphLocation { File = action.FilePath, Line = repository.Line }
                        },
                        Props = props,
                        Evidence = CreateEvidence(action.FilePath, repository.Line)
                    });
                }

                if (!string.IsNullOrWhiteSpace(repository.EntityType) &&
                    TryResolveNodeReference(repository.EntityType, out var entityReference))
                {
                    var kind = repository.Operation.Equals("write", StringComparison.OrdinalIgnoreCase) ? "writes_to" : "queries";
                    _edges.Add(new GraphEdge
                    {
                        From = id,
                        To = entityReference.Id,
                        Kind = kind,
                        Source = "static",
                        Confidence = 1.0,
                        Transform = new GraphTransform
                        {
                            Type = kind == "writes_to" ? "repository.write" : "repository.query",
                            Location = new GraphLocation { File = action.FilePath, Line = repository.Line }
                        },
                        Evidence = CreateEvidence(action.FilePath, repository.Line)
                    });
                }
            }

            foreach (var serviceUsage in action.ServiceUsages
                .GroupBy(s => s.ServiceType, StringComparer.OrdinalIgnoreCase)
                .Select(group => group.OrderBy(s => s.Line).First()))
            {
                if (!TryEnsureServiceNode(serviceUsage.ServiceType, out var serviceId, out var registration))
                {
                    continue;
                }

                var props = new Dictionary<string, object>
                {
                    ["service_type"] = serviceUsage.ServiceType
                };

                if (registration is not null)
                {
                    props["lifetime"] = registration.Lifetime;
                }

                if (!string.IsNullOrWhiteSpace(serviceUsage.Method))
                {
                    props["method"] = serviceUsage.Method!;
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
                        Location = new GraphLocation { File = action.FilePath, Line = serviceUsage.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(action.FilePath, serviceUsage.Line)
                });
            }

            foreach (var clientInvocation in action.HttpClientInvocations)
            {
                if (_httpClients.Values.FirstOrDefault(client => client.Name == clientInvocation.ClientType.Split('.').Last()) is { } client)
                {
                    var clientId = StableId.For("http.client", client.Fqdn, client.Assembly, client.SymbolId);
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
                            Location = new GraphLocation { File = action.FilePath, Line = clientInvocation.Line }
                        },
                        Props = new Dictionary<string, object>
                        {
                            ["verb"] = clientInvocation.HttpMethod,
                            ["route"] = clientInvocation.RelativePath ?? string.Empty
                        },
                        Evidence = CreateEvidence(action.FilePath, clientInvocation.Line)
                    });
                }
                else
                {
                    var clientId = EnsureHttpClientNode(clientInvocation.ClientType);
                    var props = new Dictionary<string, object>
                    {
                        ["verb"] = clientInvocation.HttpMethod,
                        ["route"] = clientInvocation.RelativePath ?? string.Empty
                    };

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
                            Location = new GraphLocation { File = action.FilePath, Line = clientInvocation.Line }
                        },
                        Props = props,
                        Evidence = CreateEvidence(action.FilePath, clientInvocation.Line)
                    });
                }
            }

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
                            Location = new GraphLocation { File = action.FilePath, Line = clientInvocation.Line }
                        },
                        Props = props,
                        Evidence = CreateEvidence(action.FilePath, clientInvocation.Line)
                    });
                }
            }
            foreach (var mapping in action.MappingInvocations)
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

                if (!string.IsNullOrWhiteSpace(mapping.AssignedVariable))
                {
                    props["variable"] = mapping.AssignedVariable!;
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
                        Location = new GraphLocation { File = action.FilePath, Line = mapping.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(action.FilePath, mapping.Line)
                });
            }

            foreach (var cast in action.CastInvocations)
            {
                if (string.IsNullOrWhiteSpace(cast.DestinationType))
                {
                    continue;
                }

                if (!TryResolveNodeReference(cast.DestinationType, out var destination))
                {
                    continue;
                }

                var props = new Dictionary<string, object>
                {
                    ["from_type"] = cast.SourceType,
                    ["cast_kind"] = cast.Kind
                };

                if (!string.IsNullOrWhiteSpace(cast.AssignedVariable))
                {
                    props["variable"] = cast.AssignedVariable!;
                }

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = destination.Id,
                    Kind = "casts_to",
                    Source = "static",
                    Confidence = 0.95,
                    Transform = new GraphTransform
                    {
                        Type = "controller.cast",
                        Location = new GraphLocation { File = action.FilePath, Line = cast.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(action.FilePath, cast.Line)
                });
            }

            foreach (var validator in action.ValidatorInvocations)
            {
                var validatorInfo = _validators.FirstOrDefault(v => v.TargetType.Split('.').Last().Equals(validator.ValidatorType.Split('.').Last(), StringComparison.OrdinalIgnoreCase));
                if (validatorInfo is null)
                {
                    continue;
                }

                var validatorId = StableId.For("validator", validatorInfo.Fqdn, validatorInfo.Assembly, validatorInfo.SymbolId);
                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = validatorId,
                    Kind = "uses_validator",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "validation",
                        Location = new GraphLocation { File = action.FilePath, Line = validator.Line }
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["target_type"] = validator.ValidatorType
                    },
                    Evidence = CreateEvidence(action.FilePath, validator.Line)
                });
            }

            foreach (var notification in action.NotificationInvocations)
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
                        Location = new GraphLocation { File = action.FilePath, Line = notification.Line }
                    },
                    Evidence = CreateEvidence(action.FilePath, notification.Line)
                });
            }

            foreach (var cache in action.CacheInvocations)
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
                        Location = new GraphLocation { File = action.FilePath, Line = cache.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(action.FilePath, cache.Line)
                });
            }

            foreach (var optionsUsage in action.OptionsUsages)
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
                        Location = new GraphLocation { File = action.FilePath, Line = optionsUsage.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(action.FilePath, optionsUsage.Line)
                });
            }
        }
    }

    private ControllerRepositoryInvocation? TryCaptureRepositoryInvocation(
        MemberAccessExpressionSyntax access,
        InvocationExpressionSyntax invocation,
        string repositoryType,
        string originalRepositoryType,
        SyntaxTree tree)
    {
        var methodName = access.Name.Identifier.Text;
        if (string.IsNullOrWhiteSpace(methodName))
        {
            return null;
        }

        var operation = DetermineRepositoryOperation(methodName);
        var entityType = ExtractRepositoryEntityType(repositoryType)
            ?? ExtractRepositoryEntityType(originalRepositoryType)
            ?? TryDeriveEntityTypeFromRepositoryName(repositoryType)
            ?? TryDeriveEntityTypeFromRepositoryName(originalRepositoryType);

        var line = GetLineNumber(tree, invocation);
        return new ControllerRepositoryInvocation(repositoryType, entityType, methodName, operation, line);
    }

    private static string DetermineRepositoryOperation(string methodName)
    {
        if (string.IsNullOrWhiteSpace(methodName))
        {
            return "query";
        }

        if (methodName.StartsWith("Write", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Add", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Update", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Remove", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Delete", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Create", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Insert", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Save", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Commit", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Upsert", StringComparison.OrdinalIgnoreCase))
        {
            return "write";
        }

        return "query";
    }

    private static string? ExtractRepositoryEntityType(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return null;
        }

        var start = typeName.IndexOf('<');
        var end = typeName.LastIndexOf('>');
        if (start >= 0 && end > start)
        {
            var content = typeName.Substring(start + 1, end - start - 1).Trim();
            if (content.Length == 0)
            {
                return null;
            }

            var comma = content.IndexOf(',');
            if (comma > 0)
            {
                content = content[..comma].Trim();
            }

            return content;
        }

        return null;
    }

    private static string? TryDeriveEntityTypeFromRepositoryName(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return null;
        }

        var simple = typeName.Split('.').Last();
        const string suffix = "Repository";
        if (simple.EndsWith(suffix, StringComparison.Ordinal) && simple.Length > suffix.Length)
        {
            return simple[..^suffix.Length];
        }

        return null;
    }

    private static bool IsRepositoryType(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return false;
        }

        return typeName.Contains("Repository", StringComparison.Ordinal) ||
               typeName.Contains("IControlledRepository", StringComparison.Ordinal) ||
               typeName.Contains("IReadOnlyRepository", StringComparison.Ordinal);
    }

    private static bool IsServiceType(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return false;
        }

        var baseType = GetTypeNameWithoutGenerics(typeName);
        return baseType.EndsWith("Service", StringComparison.Ordinal) ||
               baseType.Contains(".Services", StringComparison.Ordinal) ||
               baseType.Contains("Service.", StringComparison.Ordinal) ||
               IsClientType(baseType);
    }

    private static bool IsClientType(string typeName)
        => typeName.EndsWith("Client", StringComparison.Ordinal) ||
           typeName.Contains(".Client", StringComparison.Ordinal);

    private static string GetTypeNameWithoutGenerics(string typeName)
    {
        var genericIndex = typeName.IndexOf('<');
        return genericIndex >= 0 ? typeName[..genericIndex] : typeName;
    }

    private string EnsureHttpClientNode(string clientType)
    {
        var assembly = GuessAssemblyName(clientType);
        var symbolId = $"T:{clientType}";
        var id = StableId.For("http.client", clientType, assembly, symbolId);
        if (!_nodes.ContainsKey(id))
        {
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "http.client",
                Name = clientType.Split('.').Last(),
                Fqdn = clientType,
                Assembly = assembly,
                Project = string.Empty,
                FilePath = string.Empty,
                Span = null,
                SymbolId = symbolId,
                Tags = new[] { "integration" }
            };
        }

        return id;
    }

    private static bool IsRequestProcessorType(string? typeName)
        => !string.IsNullOrWhiteSpace(typeName) && typeName.Contains("IRequestProcessor", StringComparison.Ordinal);

    private static string? ExtractGenericArgument(string typeName)
    {
        var start = typeName.IndexOf('<');
        var end = typeName.LastIndexOf('>');
        if (start < 0 || end <= start)
        {
            return null;
        }

        return typeName.Substring(start + 1, end - start - 1);
    }

    private static string? TryResolveAssignedVariable(InvocationExpressionSyntax invocation)
    {
        var resolved = TryResolveAssignedVariable((ExpressionSyntax)invocation);
        if (!string.IsNullOrWhiteSpace(resolved))
        {
            return resolved;
        }

        if (invocation.Parent is AwaitExpressionSyntax awaitExpression)
        {
            return TryResolveAssignedVariable(awaitExpression);
        }

        return null;
    }

    private static string? TryResolveAssignedVariable(AwaitExpressionSyntax awaitExpression)
    {
        return TryResolveAssignedVariable((ExpressionSyntax)awaitExpression);
    }

    private static string? TryResolveAssignedVariable(ExpressionSyntax expression)
    {
        if (expression.Parent is AssignmentExpressionSyntax { Left: IdentifierNameSyntax identifier })
        {
            return identifier.Identifier.Text;
        }

        if (expression.Parent is EqualsValueClauseSyntax equals && equals.Parent is VariableDeclaratorSyntax declarator)
        {
            return declarator.Identifier.Text;
        }

        return null;
    }

    private void AnalyzeMinimalApiFromClass(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName)
    {
        foreach (var invocation in classDeclaration.DescendantNodes().OfType<InvocationExpressionSyntax>())
        {
            if (invocation.Expression is MemberAccessExpressionSyntax { Name.Identifier.Text: var methodName } memberAccess &&
                methodName.StartsWith("Map", StringComparison.Ordinal))
            {
                var routeLiteral = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                var route = ExtractRouteLiteral(tree, routeLiteral);
                if (route is null)
                {
                    continue;
                }

                var verb = methodName.Replace("Map", string.Empty, StringComparison.Ordinal).ToUpperInvariant();
                var line = GetLineNumber(tree, invocation);
                var symbolId = $"M:{namespaceName}.{classDeclaration.Identifier.Text}.{methodName}";
                var info = new MinimalEndpointInfo(route, verb, project.AssemblyName, project.RelativeDirectory, GetRelativePath(tree.FilePath), new GraphSpan { StartLine = line, EndLine = line }, symbolId, methodName);
                _minimalEndpoints[$"{verb}:{CanonicalizeRoute(route)}"] = info;
            }
        }
    }

    private void AnalyzeMinimalApiFromProgramFile(ProjectInfo project, SyntaxTree tree, CompilationUnitSyntax root)
    {
        foreach (var invocation in root.DescendantNodes().OfType<InvocationExpressionSyntax>())
        {
            if (invocation.Expression is MemberAccessExpressionSyntax { Name.Identifier.Text: var methodName } memberAccess &&
                methodName.StartsWith("Map", StringComparison.Ordinal))
            {
                var routeLiteral = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                var route = ExtractRouteLiteral(tree, routeLiteral);
                if (route is null)
                {
                    continue;
                }

                var verb = methodName.Replace("Map", string.Empty, StringComparison.Ordinal).ToUpperInvariant();
                var line = GetLineNumber(tree, invocation);
                var symbolId = $"M:Program.{methodName}";
                var info = new MinimalEndpointInfo(route, verb, project.AssemblyName, project.RelativeDirectory, GetRelativePath(tree.FilePath), new GraphSpan { StartLine = line, EndLine = line }, symbolId, methodName);
                _minimalEndpoints[$"{verb}:{CanonicalizeRoute(route)}"] = info;
            }
        }
    }

    private void AnalyzeMinimalEndpoints(ProjectInfo project, SyntaxTree tree)
    {
        var root = tree.GetCompilationUnitRoot();
        AnalyzeMinimalApiFromProgramFile(project, tree, root);
    }

    private void EmitMinimalEndpoints()
    {
        foreach (var endpoint in _minimalEndpoints.Values)
        {
            var id = StableId.For("endpoint.minimal_api", endpoint.Fqdn, endpoint.Assembly, endpoint.SymbolId);
            _nodes[id] = new GraphNode
            {
                Id = id,
                Type = "endpoint.minimal_api",
                Name = endpoint.Name,
                Fqdn = endpoint.Fqdn,
                Assembly = endpoint.Assembly,
                Project = endpoint.Project,
                FilePath = endpoint.FilePath,
                Span = endpoint.Span,
                SymbolId = endpoint.SymbolId,
                Tags = new[] { "web" },
                Props = new Dictionary<string, object>
                {
                    ["route"] = endpoint.Route,
                    ["http_method"] = endpoint.HttpMethod
                }
            };
        }
    }

    private static string? ResolveRoute(SyntaxList<AttributeListSyntax> attributes, string className)
    {
        foreach (var attribute in attributes.SelectMany(list => list.Attributes))
        {
            var name = attribute.Name.ToString();
            if (name.Contains("Route", StringComparison.Ordinal) || name.StartsWith("Http", StringComparison.Ordinal))
            {
                var argument = attribute.ArgumentList?.Arguments.FirstOrDefault()?.ToString();
                if (!string.IsNullOrWhiteSpace(argument))
                {
                    var template = argument.Trim('"');
                    return NormalizeRoute(template.Replace("[controller]", className.Replace("Controller", string.Empty, StringComparison.Ordinal), StringComparison.OrdinalIgnoreCase));
                }
            }
        }

        return null;
    }

    private static string NormalizeRoute(string route)
    {
        if (string.IsNullOrWhiteSpace(route))
        {
            return "/";
        }

        return route.StartsWith("/", StringComparison.Ordinal) ? route : "/" + route;
    }

    private static string CanonicalizeRoute(string route)
    {
        var normalized = NormalizeRoute(route);
        return Regex.Replace(normalized, "\\{[^}]+\\}", "{*}");
    }

    private static string? ExtractRouteLiteral(SyntaxTree tree, ExpressionSyntax? expression)
    {
        switch (expression)
        {
            case LiteralExpressionSyntax literal when literal.IsKind(SyntaxKind.StringLiteralExpression):
                return NormalizeRoute(literal.Token.ValueText);
            case InterpolatedStringExpressionSyntax interpolated:
                var text = string.Concat(interpolated.Contents.Select(content => content switch
                {
                    InterpolatedStringTextSyntax t => t.TextToken.ValueText,
                    _ => "{*}"
                }));
                return NormalizeRoute(text);
            default:
                return null;
        }
    }
}
