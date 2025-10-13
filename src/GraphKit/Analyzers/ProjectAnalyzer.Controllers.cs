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
                if (invocation.Expression is MemberAccessExpressionSyntax { Name.Identifier.Text: "Send" } memberAccess)
                {
                    var argument = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                    string? requestType = null;
                    if (argument is ObjectCreationExpressionSyntax objectCreation)
                    {
                        requestType = objectCreation.Type.ToString();
                    }
                    else if (argument is IdentifierNameSyntax identifierArgument &&
                        info.LocalVariables.TryGetValue(identifierArgument.Identifier.Text, out var resolvedType) &&
                        !string.Equals(resolvedType, "var", StringComparison.OrdinalIgnoreCase))
                    {
                        requestType = resolvedType;
                    }

                    if (requestType is not null)
                    {
                        var line = GetLineNumber(tree, invocation);
                        info.RequestInvocations.Add(new ControllerRequestInvocation(requestType, line));

                        if (invocation.Parent is AssignmentExpressionSyntax { Left: IdentifierNameSyntax assignTarget })
                        {
                            var handler = FindHandlerForRequest(requestType);
                            if (handler is not null)
                            {
                                info.LocalVariables[assignTarget.Identifier.Text] = handler.ResponseType;
                            }
                        }
                        else if (invocation.Parent is AwaitExpressionSyntax awaitExpression &&
                                 awaitExpression.Parent is AssignmentExpressionSyntax { Left: IdentifierNameSyntax awaitAssign })
                        {
                            var handler = FindHandlerForRequest(requestType);
                            if (handler is not null)
                            {
                                info.LocalVariables[awaitAssign.Identifier.Text] = handler.ResponseType;
                            }
                        }
                        else if (invocation.Parent is EqualsValueClauseSyntax equals && equals.Parent is VariableDeclaratorSyntax declarator)
                        {
                            var handler = FindHandlerForRequest(requestType);
                            if (handler is not null)
                            {
                                info.LocalVariables[declarator.Identifier.Text] = handler.ResponseType;
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
                        var resolvedType = ResolveImplementationType(typeName) ?? typeName;
                        if (typeName.Contains("Client", StringComparison.Ordinal))
                        {
                            var callMethod = access.Name.Identifier.Text.ToUpperInvariant();
                            var routeLiteral = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                            var relativePath = ExtractRouteLiteral(tree, routeLiteral);
                            var line = GetLineNumber(tree, invocation);
                            info.HttpClientInvocations.Add(new ControllerClientInvocation(typeName, callMethod, relativePath, line));
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
                        else if (TryResolveOptionsType(resolvedType) is { } optionsType)
                        {
                            var line = GetLineNumber(tree, access);
                            info.OptionsUsages.Add(new OptionsUsage(optionsType, line));
                        }
                        else if (typeName.Contains("IMediator", StringComparison.Ordinal) || typeName.Contains("IPublisher", StringComparison.Ordinal))
                        {
                            var methodIdentifier = access.Name.Identifier.Text;
                            if (methodIdentifier.StartsWith("Publish", StringComparison.Ordinal))
                            {
                                var line = GetLineNumber(tree, invocation);
                                var argument = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                                var notificationType = argument switch
                                {
                                    ObjectCreationExpressionSyntax creation => creation.Type.ToString(),
                                    IdentifierNameSyntax identifierArgument => TryResolveExpressionType(identifierArgument, parameterTypes, info.LocalVariables),
                                    _ => null
                                };

                                if (!string.IsNullOrWhiteSpace(notificationType))
                                {
                                    info.NotificationInvocations.Add(new ControllerNotificationInvocation(notificationType!, line));
                                }
                            }
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
