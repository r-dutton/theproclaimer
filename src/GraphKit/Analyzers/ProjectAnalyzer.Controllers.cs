using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.FlowAnalysis.Interprocedural;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using FlowAnalysisCore = GraphKit.FlowAnalysis.Core.FlowAnalysis;

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
        var classAuthorization = CollectAuthorizationAttributes(tree, classDeclaration.AttributeLists, "class_attribute");
        var methodLookup = classDeclaration.Members
            .OfType<MethodDeclarationSyntax>()
            .GroupBy(m => m.Identifier.Text, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.ToList(), StringComparer.OrdinalIgnoreCase);
        var helperInfos = new Dictionary<string, ControllerActionInfo>(StringComparer.OrdinalIgnoreCase);
        var actionInfos = new List<ControllerActionInfo>();
        var model = project.GetModel(tree);
        var compilation = project.Compilation;
        var callsitePredicate = ComposeInterproceduralPredicate(ShouldExpandForCqrsEfHttpMap);
        var pointsToFacade = CreatePointsToFacade(callsitePredicate);
        var valueContentFacade = CreateValueContentFacade(callsitePredicate);

            foreach (var method in classDeclaration.Members.OfType<MethodDeclarationSyntax>())
            {
            var isPublic = method.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword));
            var isAsync = method.Modifiers.Any(m => m.IsKind(SyntaxKind.AsyncKeyword));
            var returnsTask = ReturnsTaskLike(method.ReturnType, model);
            if (!isPublic && !isAsync && !returnsTask)
            {
                continue;
            }
            var isAction = isPublic;

            var parameterTypes = method.ParameterList.Parameters
                .Where(p => !string.IsNullOrWhiteSpace(p.Identifier.Text))
                .ToDictionary(
                    p => p.Identifier.Text,
                    p => p.Type is null ? null : QualifyTypeName(p.Type.ToString(), project.AssemblyName, project.RelativeDirectory),
                    StringComparer.OrdinalIgnoreCase);

            var methodName = method.Identifier.Text;
            var actionFqdn = $"{fqdn}.{methodName}";
            var methodSymbolId = BuildControllerMethodSymbolId(fqdn, method, project);
            var actionKey = methodSymbolId;
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

            foreach (var authorization in classAuthorization.Requirements)
            {
                info.Authorizations.Add(authorization);
            }

            info.AllowsAnonymous = classAuthorization.AllowsAnonymous;

            var methodAuthorization = CollectAuthorizationAttributes(tree, method.AttributeLists, "method_attribute");
            if (methodAuthorization.AllowsAnonymous)
            {
                info.Authorizations.Clear();
                info.AllowsAnonymous = true;
            }
            else
            {
                if (methodAuthorization.Requirements.Count > 0)
                {
                    info.AllowsAnonymous = false;
                }

                foreach (var authorization in methodAuthorization.Requirements)
                {
                    info.Authorizations.Add(authorization);
                }
            }

            IMethodSymbol? methodSymbol = null;
            if (isAction)
            {
                try
                {
                    methodSymbol = model.GetDeclaredSymbol(method) as IMethodSymbol;
                }
                catch (ArgumentException)
                {
                    methodSymbol = null;
                }
            }

            var usedOperationVisitor = false;
            if (methodSymbol is not null && TryAcquireMethodAnalysis(methodSymbol))
            {
                var visitor = new ControllerOperationVisitor(
                    this,
                    model,
                    info,
                    pointsToFacade,
                    valueContentFacade,
                    _facts,
                    project,
                    parameterTypes,
                    fieldLookup);
                var analysis = FlowAnalysisCore.GetOrCreateMethodAnalysis(compilation, methodSymbol, InterproceduralConfiguration);
                analysis.Context.Accept(visitor);
                usedOperationVisitor = true;
            }

            // Attribute-declared response status codes (ProducesResponseType)
            foreach (var attr in method.AttributeLists.SelectMany(l => l.Attributes))
            {
                var attrName = attr.Name.ToString();
                if (!attrName.Contains("ProducesResponseType", StringComparison.Ordinal))
                {
                    continue;
                }
                var args = attr.ArgumentList?.Arguments;
                if (args is not { Count: > 0 }) continue;
                foreach (var a in args)
                {
                    var code = TryParseStatusCodeExpression(a.Expression?.ToString());
                    if (code.HasValue)
                    {
                        info.StatusCodes.Add(code.Value);
                    }
                }
            }

            foreach (var local in Descendants<LocalDeclarationStatementSyntax>(method))
            {
                var declaredType = local.Declaration.Type.ToString();
                foreach (var variable in local.Declaration.Variables)
                {
                    var resolvedType = declaredType;
                    if (string.Equals(resolvedType, "var", StringComparison.OrdinalIgnoreCase))
                    {
                        if (variable.Initializer?.Value is ObjectCreationExpressionSyntax creation)
                        {
                            resolvedType = creation.Type.ToString();
                        }
                        else if (variable.Initializer?.Value is InvocationExpressionSyntax initInvocation)
                        {
                            var guessedType = GuessServiceTypeFromInitializer(
                                initInvocation,
                                fieldLookup,
                                parameterTypes,
                                info.LocalVariables,
                                project.AssemblyName,
                                project.RelativeDirectory);
                            var genericReturn = initInvocation.Expression switch
                            {
                                MemberAccessExpressionSyntax memberAccess when memberAccess.Name is GenericNameSyntax genericName && genericName.TypeArgumentList.Arguments.Count > 0
                                    => genericName.TypeArgumentList.Arguments[0].ToString(),
                                GenericNameSyntax directGeneric when directGeneric.TypeArgumentList.Arguments.Count > 0
                                    => directGeneric.TypeArgumentList.Arguments[0].ToString(),
                                _ => null
                            };

                            if (!string.IsNullOrWhiteSpace(genericReturn))
                            {
                                resolvedType = genericReturn!;
                            }
                            else if (!string.IsNullOrWhiteSpace(guessedType))
                            {
                                resolvedType = guessedType!;
                            }
                        }
                    }

                    resolvedType = QualifyTypeName(resolvedType, project.AssemblyName, project.RelativeDirectory);
                    info.LocalVariables[variable.Identifier.Text] = resolvedType;

                    if (ResolveStringValue(variable.Initializer?.Value) is { } stringValue)
                    {
                        info.LocalStringValues[variable.Identifier.Text] = stringValue;
                    }
                }
            }

            // Track subsequent string assignments inside the method so later route inference can see them
            foreach (var assignment in Descendants<AssignmentExpressionSyntax>(method))
            {
                if (assignment.Left is IdentifierNameSyntax left)
                {
                    // Prefer literal/interpolated resolution
                    if (ResolveStringValue(assignment.Right) is { } assignedValue)
                    {
                        info.LocalStringValues[left.Identifier.Text] = assignedValue;
                        continue;
                    }

                    // Heuristic: when assigning AddQueryString(url, ...), keep the base url value
                    if (assignment.Right is InvocationExpressionSyntax inv)
                    {
                        var baseRoute = ResolveRouteFromExpression(inv, info.LocalStringValues);
                        if (!string.IsNullOrWhiteSpace(baseRoute))
                        {
                            info.LocalStringValues[left.Identifier.Text] = baseRoute!;
                        }
                    }
                }
            }

            foreach (var invocation in Descendants<InvocationExpressionSyntax>(method))
            {
                if (usedOperationVisitor)
                {
                    continue;
                }
                // Detect status code via common MVC helper methods inside return statements
                if (!usedOperationVisitor)
                {
                    if (invocation.Expression is MemberAccessExpressionSyntax statusAccess)
                    {
                        var helperName = statusAccess.Name.Identifier.Text;
                        var parentReturn = invocation.Parent as ReturnStatementSyntax ?? (invocation.Parent as AwaitExpressionSyntax)?.Parent as ReturnStatementSyntax;
                        if (parentReturn is not null)
                        {
                            switch (helperName)
                            {
                                case "Ok": info.StatusCodes.Add(200); break;
                                case "Created":
                                case "CreatedAtAction":
                                case "CreatedAtRoute": info.StatusCodes.Add(201); break;
                                case "NoContent": info.StatusCodes.Add(204); break;
                                case "BadRequest": info.StatusCodes.Add(400); break;
                                case "Unauthorized": info.StatusCodes.Add(401); break;
                                case "Forbidden": info.StatusCodes.Add(403); break;
                                case "NotFound": info.StatusCodes.Add(404); break;
                                case "Conflict": info.StatusCodes.Add(409); break;
                                case "Problem": info.StatusCodes.Add(500); break; // generic problem response
                            }
                        }
                    }
                    else if (invocation.Expression is IdentifierNameSyntax statusIdentifier)
                    {
                        var helperName = statusIdentifier.Identifier.Text;
                        var parentReturn = invocation.Parent as ReturnStatementSyntax ?? (invocation.Parent as AwaitExpressionSyntax)?.Parent as ReturnStatementSyntax;
                        if (parentReturn is not null)
                        {
                            switch (helperName)
                            {
                                case "Ok": info.StatusCodes.Add(200); break;
                                case "Created":
                                case "CreatedAtAction":
                                case "CreatedAtRoute": info.StatusCodes.Add(201); break;
                                case "NoContent": info.StatusCodes.Add(204); break;
                                case "BadRequest": info.StatusCodes.Add(400); break;
                                case "Unauthorized": info.StatusCodes.Add(401); break;
                                case "Forbidden": info.StatusCodes.Add(403); break;
                                case "NotFound": info.StatusCodes.Add(404); break;
                                case "Conflict": info.StatusCodes.Add(409); break;
                                case "Problem": info.StatusCodes.Add(500); break; // generic problem response
                            }
                        }
                    }
                }
                if (invocation.Expression is MemberAccessExpressionSyntax memberAccess)
                {
                    var methodIdentifier = memberAccess.Name.Identifier.Text;
                    var isMediatorInvocation = methodIdentifier.Equals("Send", StringComparison.Ordinal) || methodIdentifier.Equals("SendAsync", StringComparison.Ordinal);
                    var isRequestProcessorInvocation = methodIdentifier.Equals("Process", StringComparison.Ordinal) || methodIdentifier.Equals("ProcessAsync", StringComparison.Ordinal);

                    if (isRequestProcessorInvocation)
                    {
                        var targetType = TryResolveExpressionType(memberAccess.Expression, parameterTypes, info.LocalVariables, project.AssemblyName, project.RelativeDirectory, fieldLookup);
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
                            var resolvedTarget = ResolveImplementationType(targetType, info.Assembly, info.Project);
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
                            : TryResolveExpressionType(argumentExpression, parameterTypes, info.LocalVariables, project.AssemblyName, project.RelativeDirectory, fieldLookup);

                        if (requestType is null && argumentExpression is IdentifierNameSyntax identifierArgument &&
                            info.LocalVariables.TryGetValue(identifierArgument.Identifier.Text, out var resolvedType) &&
                            !string.Equals(resolvedType, "var", StringComparison.OrdinalIgnoreCase))
                        {
                            requestType = QualifyTypeName(resolvedType, project.AssemblyName, project.RelativeDirectory);
                        }

                        if (requestType is null && argumentExpression is IdentifierNameSyntax fieldIdentifier &&
                            fieldLookup.TryGetValue(fieldIdentifier.Identifier.Text.TrimStart('_'), out var fieldDescriptor))
                        {
                            requestType = QualifyTypeName(fieldDescriptor.Type, project.AssemblyName, project.RelativeDirectory);
                        }

                        if (!string.IsNullOrWhiteSpace(requestType))
                        {
                            var invocationLine = GetLineNumber(tree, invocation);
                            info.RequestInvocations.Add(new ControllerRequestInvocation(requestType!, invocationLine));

                            var handler = FindHandlerForRequest(requestType!);
                            if (handler is not null)
                            {
                                var responseType = handler.ResponseType;
                                if (invocation.Parent is AssignmentExpressionSyntax { Left: IdentifierNameSyntax assignTarget })
                                {
                                    info.LocalVariables[assignTarget.Identifier.Text] = QualifyTypeName(responseType, project.AssemblyName, project.RelativeDirectory);
                                    if (IsMeaningfulResponseType(responseType))
                                    {
                                        info.ResponseUsages.Add(new ControllerResponseUsage(responseType, assignTarget.Identifier.Text, GetLineNumber(tree, invocation), false));
                                    }
                                }
                                else if (invocation.Parent is AwaitExpressionSyntax awaitedInvocation &&
                                         awaitedInvocation.Parent is AssignmentExpressionSyntax { Left: IdentifierNameSyntax awaitAssign })
                                {
                                    info.LocalVariables[awaitAssign.Identifier.Text] = QualifyTypeName(responseType, project.AssemblyName, project.RelativeDirectory);
                                    if (IsMeaningfulResponseType(responseType))
                                    {
                                        info.ResponseUsages.Add(new ControllerResponseUsage(responseType, awaitAssign.Identifier.Text, GetLineNumber(tree, awaitedInvocation), false));
                                    }
                                }
                                else if (invocation.Parent is EqualsValueClauseSyntax equals && equals.Parent is VariableDeclaratorSyntax declarator)
                                {
                                    info.LocalVariables[declarator.Identifier.Text] = QualifyTypeName(responseType, project.AssemblyName, project.RelativeDirectory);
                                    if (IsMeaningfulResponseType(responseType))
                                    {
                                        info.ResponseUsages.Add(new ControllerResponseUsage(responseType, declarator.Identifier.Text, GetLineNumber(tree, invocation), false));
                                    }
                                }
                                else
                                {
                                    ExpressionSyntax contextNode = invocation;
                                    if (invocation.Parent is AwaitExpressionSyntax awaitedContext)
                                    {
                                        contextNode = awaitedContext;
                                    }

                                    if (IsMeaningfulResponseType(responseType))
                                    {
                                        var responseLine = GetLineNumber(tree, contextNode.Parent is ReturnStatementSyntax returnStatement ? returnStatement : contextNode);
                                        var isReturn = contextNode.Parent is ReturnStatementSyntax;
                                        info.ResponseUsages.Add(new ControllerResponseUsage(responseType, null, responseLine, isReturn));
                                    }
                                }
                            }
                        }
                    }
                }

                if (invocation.Expression is MemberAccessExpressionSyntax access)
                {
                    var handled = false;
                    if (!usedOperationVisitor)
                    {
                        handled = TryHandleServiceInvocationSyntax(info, invocation, access, parameterTypes, fieldLookup, project);
                    }

                    if (handled)
                    {
                        continue;
                    }

                    if (access.Name is GenericNameSyntax { Identifier.Text: "ProjectTo" } projectTo)
                    {
                        var destination = projectTo.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                        var sourceType = TryResolveProjectionSource(access.Expression, parameterTypes, info.LocalVariables, fieldLookup, project.AssemblyName, project.RelativeDirectory);
                        if (!string.IsNullOrWhiteSpace(destination))
                        {
                            var line = GetLineNumber(tree, invocation);
                            info.MappingInvocations.Add(new ControllerMappingInvocation(sourceType, destination, null, line));
                        }
                    }
                    else if (access.Name is GenericNameSyntax { Identifier.Text: "ProjectByIdAsync" } projectById)
                    {
                        var destination = projectById.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                        var sourceType = TryResolveProjectionSource(access.Expression, parameterTypes, info.LocalVariables, fieldLookup, project.AssemblyName, project.RelativeDirectory);
                        if (!string.IsNullOrWhiteSpace(destination))
                        {
                            var line = GetLineNumber(tree, invocation);
                            info.MappingInvocations.Add(new ControllerMappingInvocation(sourceType, destination, null, line));
                        }
                    }
                }
                else if (invocation.Expression is IdentifierNameSyntax helperIdentifier)
                {
                    var helperName = helperIdentifier.Identifier.Text;
                    if (methodLookup.TryGetValue(helperName, out var helperCandidates))
                    {
                        var invocationLine = GetLineNumber(tree, invocation);
                        foreach (var helperMethod in helperCandidates)
                        {
                            var helperIsPublic = helperMethod.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword));
                            if (helperIsPublic)
                            {
                                continue;
                            }

                            var helperFqdn = $"{fqdn}.{helperMethod.Identifier.Text}";
                            var helperKey = BuildControllerMethodSymbolId(fqdn, helperMethod, project);
                            info.HelperInvocations.Add(new ControllerHelperInvocation(helperKey, helperFqdn, invocationLine));
                        }
                    }
                }
                else if (invocation.Expression is MemberAccessExpressionSyntax { Expression: ThisExpressionSyntax } thisAccess)
                {
                    var helperName = thisAccess.Name.Identifier.Text;
                    if (methodLookup.TryGetValue(helperName, out var helperCandidates))
                    {
                        var invocationLine = GetLineNumber(tree, invocation);
                        foreach (var helperMethod in helperCandidates)
                        {
                            var helperIsPublic = helperMethod.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword));
                            if (helperIsPublic)
                            {
                                continue;
                            }

                            var helperFqdn = $"{fqdn}.{helperMethod.Identifier.Text}";
                            var helperKey = BuildControllerMethodSymbolId(fqdn, helperMethod, project);
                            info.HelperInvocations.Add(new ControllerHelperInvocation(helperKey, helperFqdn, invocationLine));
                        }
                    }
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

                var resolvedType = ResolveImplementationType(descriptor.Type, info.Assembly, info.Project) ?? descriptor.Type;
                if (!IsConfigurationType(resolvedType) && !IsConfigurationType(descriptor.Type))
                {
                    continue;
                }

                if (TryCaptureConfigurationIndexer(elementAccess, resolvedType ?? descriptor.Type, tree) is { } configurationUsage)
                {
                    info.ConfigurationUsages.Add(configurationUsage);
                }
            }
            foreach (var binary in Descendants<BinaryExpressionSyntax>(method))
            {
                if (binary.IsKind(SyntaxKind.AsExpression) && binary.Right is TypeSyntax asType)
                {
                    var destinationType = asType.ToString();
                    var sourceType = TryResolveExpressionType(binary.Left, parameterTypes, info.LocalVariables, project.AssemblyName, project.RelativeDirectory, fieldLookup);
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
                    var sourceType = TryResolveExpressionType(binary.Left, parameterTypes, info.LocalVariables, project.AssemblyName, project.RelativeDirectory, fieldLookup);
                    if (string.IsNullOrWhiteSpace(destinationType) || string.IsNullOrWhiteSpace(sourceType))
                    {
                        continue;
                    }

                    var line = GetLineNumber(tree, binary);
                    info.CastInvocations.Add(new ControllerCastInvocation(sourceType!, destinationType, null, line, "is"));
                }
            }

            foreach (var pattern in Descendants<IsPatternExpressionSyntax>(method))
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

                var sourceType = TryResolveExpressionType(pattern.Expression, parameterTypes, info.LocalVariables, project.AssemblyName, project.RelativeDirectory, fieldLookup);
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

            if (isAction)
            {
                // Fallback inference: if no explicit status codes captured, infer a typical default.
                if (info.StatusCodes.Count == 0)
                {
                    if (string.Equals(info.HttpMethod, "POST", StringComparison.Ordinal))
                    {
                        info.StatusCodes.Add(201); // Created endpoints usually return 201
                    }
                    else
                    {
                        info.StatusCodes.Add(200); // Default success
                    }
                }

                _controllerActions[actionKey] = info;
                var routeKey = $"{info.HttpMethod}:{CanonicalizeRoute(info.Route)}";
                var routeBag = _controllerRoutes.GetOrAdd(routeKey, _ => new ConcurrentBag<ControllerActionInfo>());
                routeBag.Add(info);
                actionInfos.Add(info);
            }
            else
            {
                helperInfos[actionKey] = info;
            }
        }

        MergeControllerHelperInvocations(actionInfos, helperInfos);
    }

    private void MergeControllerHelperInvocations(
        IReadOnlyList<ControllerActionInfo> actions,
        IReadOnlyDictionary<string, ControllerActionInfo> helperInfos)
    {
        if (helperInfos.Count == 0 || actions.Count == 0)
        {
            return;
        }

        foreach (var action in actions)
        {
            var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            AppendHelperInfoRecursive(action, action, helperInfos, visited);
        }
    }

    private void AppendHelperInfoRecursive(
        ControllerActionInfo root,
        ControllerActionInfo current,
        IReadOnlyDictionary<string, ControllerActionInfo> helperInfos,
        HashSet<string> visited)
    {
        foreach (var helper in current.HelperInvocations)
        {
            if (!helperInfos.TryGetValue(helper.TargetKey, out var helperInfo))
            {
                continue;
            }

            if (!visited.Add(helper.TargetKey))
            {
                continue;
            }

            AppendControllerInfo(root, helperInfo);
            AppendHelperInfoRecursive(root, helperInfo, helperInfos, visited);
            visited.Remove(helper.TargetKey);
        }
    }

    private void AppendControllerInfo(ControllerActionInfo target, ControllerActionInfo source)
    {
        target.RequestInvocations.AddRange(source.RequestInvocations);
        target.ServiceUsages.AddRange(source.ServiceUsages);
        target.NotificationInvocations.AddRange(source.NotificationInvocations);
        target.CacheInvocations.AddRange(source.CacheInvocations);
        target.OptionsUsages.AddRange(source.OptionsUsages);
        target.RepositoryInvocations.AddRange(source.RepositoryInvocations);
        target.DomainInvocations.AddRange(source.DomainInvocations);
        target.MappingInvocations.AddRange(source.MappingInvocations);
        target.ConfigurationUsages.AddRange(source.ConfigurationUsages);
        target.ValidatorInvocations.AddRange(source.ValidatorInvocations);
        target.CastInvocations.AddRange(source.CastInvocations);
        target.ResponseUsages.AddRange(source.ResponseUsages);
        target.HttpClientInvocations.AddRange(source.HttpClientInvocations);
        target.ValidationCalls.AddRange(source.ValidationCalls);
    }

    private bool TryHandleServiceInvocationSyntax(
        ControllerActionInfo info,
        InvocationExpressionSyntax invocation,
        MemberAccessExpressionSyntax access,
        IReadOnlyDictionary<string, string?> parameterTypes,
        IReadOnlyDictionary<string, FieldDescriptor> fieldLookup,
        ProjectInfo project)
    {
        var tree = invocation.SyntaxTree;
        var resolvedTargetType = TryResolveExpressionType(access.Expression, parameterTypes, info.LocalVariables, project.AssemblyName, project.RelativeDirectory, fieldLookup);
        if (!string.IsNullOrWhiteSpace(resolvedTargetType) && !IsProjectionInvocation(access))
        {
            HandleServiceInvocation(info, access, invocation, resolvedTargetType!, parameterTypes, tree, fieldLookup);
            return true;
        }

        if (access.Expression is IdentifierNameSyntax identifier)
        {
            var identifierName = identifier.Identifier.Text;
            var normalizedName = identifierName.TrimStart('_');

            if (IsProjectionInvocation(access))
            {
                return false;
            }

            if (fieldLookup.TryGetValue(normalizedName, out var descriptor))
            {
                HandleServiceInvocation(info, access, invocation, descriptor.Type, parameterTypes, tree, fieldLookup);
                return true;
            }

            if (info.LocalVariables.TryGetValue(identifierName, out var localType) && !string.IsNullOrWhiteSpace(localType))
            {
                HandleServiceInvocation(info, access, invocation, localType, parameterTypes, tree, fieldLookup);
                return true;
            }

            if (!string.Equals(identifierName, normalizedName, StringComparison.Ordinal) &&
                info.LocalVariables.TryGetValue(normalizedName, out var trimmedLocalType) &&
                !string.IsNullOrWhiteSpace(trimmedLocalType))
            {
                HandleServiceInvocation(info, access, invocation, trimmedLocalType, parameterTypes, tree, fieldLookup);
                return true;
            }

            return false;
        }

        var expressionKey = access.Expression.ToString();
        if (info.LocalVariables.TryGetValue(expressionKey, out var expressionType) &&
            !string.IsNullOrWhiteSpace(expressionType) &&
            !IsProjectionInvocation(access))
        {
            HandleServiceInvocation(info, access, invocation, expressionType, parameterTypes, tree, fieldLookup);
            var logPath = Path.Combine(Path.GetTempPath(), "domain-invocations.log");
            File.AppendAllText(logPath, $"expression-map:{expressionKey} -> {expressionType}" + Environment.NewLine);
            return true;
        }

        return false;
    }

    private void HandleServiceInvocation(
        ControllerActionInfo info,
        MemberAccessExpressionSyntax access,
        InvocationExpressionSyntax? invocation,
        string typeName,
        IReadOnlyDictionary<string, string?> parameterTypes,
        SyntaxTree tree,
        IReadOnlyDictionary<string, FieldDescriptor> fieldLookup)
    {
        if (invocation is null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(typeName) || string.Equals(typeName, "var", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        var qualifiedType = QualifyTypeName(typeName, info.Assembly, info.Project);
        if (string.IsNullOrWhiteSpace(qualifiedType) || string.Equals(qualifiedType, "var", StringComparison.OrdinalIgnoreCase))
        {
            qualifiedType = typeName;
        }
        else if (!qualifiedType.Contains('<') && typeName.Contains('<'))
        {
            qualifiedType = typeName;
        }

        var baseTypeName = GetTypeNameWithoutGenerics(qualifiedType);
        var resolvedType = ResolveImplementationType(qualifiedType, info.Assembly, info.Project) ?? qualifiedType;
        if (IsConfigurationType(resolvedType) || IsConfigurationType(qualifiedType))
        {
            if (TryCaptureConfigurationUsage(access, invocation, resolvedType ?? qualifiedType, tree) is { } configurationUsage)
            {
                info.ConfigurationUsages.Add(configurationUsage);
            }

            return;
        }

        if (string.IsNullOrWhiteSpace(resolvedType))
        {
            return;
        }

        var serviceMethod = GetMemberName(access.Name);
        var expressionKey = access.Expression.ToString();
        var assignedVariable = TryResolveAssignedVariable(invocation);
        var repositoryEntityResolved = false;

        var normalizedExpressionKey = NormalizeExpressionKey(expressionKey);
        string? expressionDomainType = null;

        if (!string.IsNullOrWhiteSpace(expressionKey))
        {
            if (info.DomainLocalTypes.TryGetValue(expressionKey, out var mappedType) && !string.IsNullOrWhiteSpace(mappedType))
            {
                expressionDomainType = mappedType;
            }
            else if (!string.Equals(expressionKey, normalizedExpressionKey, StringComparison.Ordinal) &&
                     info.DomainLocalTypes.TryGetValue(normalizedExpressionKey, out var normalizedMappedType) &&
                     !string.IsNullOrWhiteSpace(normalizedMappedType))
            {
                expressionDomainType = normalizedMappedType;
            }
        }

        if (!string.IsNullOrWhiteSpace(expressionKey) && info.DomainLocalVariables.Contains(expressionKey))
        {
            repositoryEntityResolved = true;
        }
        else if (!string.Equals(expressionKey, normalizedExpressionKey, StringComparison.Ordinal) &&
                 info.DomainLocalVariables.Contains(normalizedExpressionKey))
        {
            repositoryEntityResolved = true;
        }

        if (!string.IsNullOrWhiteSpace(expressionDomainType))
        {
            repositoryEntityResolved = true;
            qualifiedType = expressionDomainType;
            resolvedType = ResolveImplementationType(expressionDomainType, info.Assembly, info.Project) ?? expressionDomainType;

        }

        if (!repositoryEntityResolved)
        {
            var rootIdentifier = TryGetRootIdentifier(access.Expression);
            if (!string.IsNullOrWhiteSpace(rootIdentifier) && info.DomainLocalVariables.Contains(rootIdentifier))
            {
                repositoryEntityResolved = true;
                if (info.DomainLocalTypes.TryGetValue(rootIdentifier, out var rootDomainType) && !string.IsNullOrWhiteSpace(rootDomainType))
                {
                    qualifiedType = rootDomainType;
                    resolvedType = ResolveImplementationType(rootDomainType, info.Assembly, info.Project) ?? rootDomainType;
                }
                else if (info.LocalVariables.TryGetValue(rootIdentifier, out var rootLocalType) && !string.IsNullOrWhiteSpace(rootLocalType))
                {
                    qualifiedType = rootLocalType;
                    resolvedType = ResolveImplementationType(rootLocalType, info.Assembly, info.Project) ?? rootLocalType;
                }
            }
        }
        if (!string.IsNullOrWhiteSpace(serviceMethod) && serviceMethod.Contains("GetById", StringComparison.Ordinal))
        {
            var assignedDebug = TryResolveAssignedVariable(invocation) ?? "<none>";
            var hasExpressionMap = info.LocalVariables.ContainsKey(expressionKey);
        }


        if (!string.IsNullOrWhiteSpace(expressionKey) &&
            info.LocalVariables.TryGetValue(expressionKey, out var expressionMappedType) &&
            !string.IsNullOrWhiteSpace(expressionMappedType))
        {
            qualifiedType = expressionMappedType;
            resolvedType = ResolveImplementationType(expressionMappedType, info.Assembly, info.Project) ?? expressionMappedType;
        }

        if (access.Expression is InvocationExpressionSyntax innerInvocation &&
            innerInvocation.Expression is MemberAccessExpressionSyntax innerAccess &&
            (string.IsNullOrWhiteSpace(expressionKey) || !info.LocalVariables.ContainsKey(expressionKey) || string.IsNullOrWhiteSpace(info.LocalVariables[expressionKey]) || !string.IsNullOrWhiteSpace(assignedVariable)))
        {
            var repositoryCandidate = TryResolveExpressionType(innerAccess.Expression, parameterTypes, info.LocalVariables, info.Assembly, info.Project, fieldLookup);
            if (string.IsNullOrWhiteSpace(repositoryCandidate) && innerAccess.Expression is IdentifierNameSyntax innerIdentifier)
            {
                var normalizedInner = innerIdentifier.Identifier.Text.TrimStart('_');
                if (fieldLookup.TryGetValue(normalizedInner, out var descriptor))
                {
                    repositoryCandidate = descriptor.Type;
                }
            }

            if (!string.IsNullOrWhiteSpace(repositoryCandidate))
            {
                var qualifiedRepository = QualifyTypeName(repositoryCandidate, info.Assembly, info.Project) ?? repositoryCandidate;
                if (IsRepositoryType(qualifiedRepository))
                {
                    var entityCandidate = ExtractRepositoryEntityType(qualifiedRepository)
                        ?? TryDeriveEntityTypeFromRepositoryName(qualifiedRepository);
                    if (!string.IsNullOrWhiteSpace(entityCandidate))
                    {
                        var innermost = ExtractInnermostGenericType(entityCandidate) ?? entityCandidate;
                        var qualifiedEntity = QualifyTypeName(innermost, info.Assembly, info.Project) ?? innermost;
                        var innerKey = innerInvocation.ToString();
                        info.LocalVariables[innerKey] = qualifiedEntity;
                        info.DomainLocalTypes[innerKey] = qualifiedEntity;
                        var normalizedInnerKey = NormalizeExpressionKey(innerKey);
                        if (!string.Equals(innerKey, normalizedInnerKey, StringComparison.Ordinal))
                        {
                            info.LocalVariables[normalizedInnerKey] = qualifiedEntity;
                            info.DomainLocalTypes[normalizedInnerKey] = qualifiedEntity;
                        }

                        var invocationKey = invocation.ToString();
                        info.LocalVariables[invocationKey] = qualifiedEntity;
                        info.DomainLocalTypes[invocationKey] = qualifiedEntity;
                        var normalizedInvocationKey = NormalizeExpressionKey(invocationKey);
                        if (!string.Equals(invocationKey, normalizedInvocationKey, StringComparison.Ordinal))
                        {
                            info.LocalVariables[normalizedInvocationKey] = qualifiedEntity;
                            info.DomainLocalTypes[normalizedInvocationKey] = qualifiedEntity;
                        }

                        if (!string.IsNullOrWhiteSpace(assignedVariable))
                        {
                            info.LocalVariables[assignedVariable!] = qualifiedEntity;
                            info.DomainLocalVariables.Add(assignedVariable!);
                            info.DomainLocalTypes[assignedVariable!] = qualifiedEntity;
                        }

                        qualifiedType = qualifiedEntity;
                        resolvedType = ResolveImplementationType(qualifiedEntity, info.Assembly, info.Project) ?? qualifiedEntity;
                        repositoryEntityResolved = true;

                    }
                }
            }
        }

        var resolvedBaseType = GetTypeNameWithoutGenerics(resolvedType);
        var serviceLine = GetLineNumber(tree, invocation);

        if (repositoryEntityResolved || IsDomainType(resolvedType) || IsDomainType(qualifiedType))
        {
            var instanceExpression = access.Expression.ToString();
            info.DomainInvocations.Add(new ControllerDomainInvocation(
                resolvedType ?? qualifiedType,
                serviceMethod ?? access.Name.ToString(),
                serviceLine,
                instanceExpression,
                assignedVariable));

            if (!string.IsNullOrWhiteSpace(assignedVariable) && !string.IsNullOrWhiteSpace(resolvedType ?? qualifiedType))
            {
                var domainType = resolvedType ?? qualifiedType;
                info.LocalVariables[assignedVariable!] = domainType;
                info.DomainLocalVariables.Add(assignedVariable!);
                info.DomainLocalTypes[assignedVariable!] = domainType;
            }

        }
        else if (access.Expression is IdentifierNameSyntax identifierName)
        {
            var identifierKey = identifierName.Identifier.Text;
            var missMethod = serviceMethod ?? access.Name.ToString();
            if (info.LocalVariables.TryGetValue(identifierKey, out var localVarType))
            {
                if (!string.IsNullOrWhiteSpace(localVarType) && !IsDomainType(localVarType))
                {
                    return;
                }

            }
        }
        if (IsGuardInvocation(access))
        {
            info.ValidationCalls.Add(new ControllerValidationCall(
                access.Expression.ToString(),
                serviceMethod ?? string.Empty,
                serviceLine));
            return;
        }
        var serviceTypeName = resolvedType;
        string? serviceTargetType = null;
        var recordedServiceUsage = false;

        if (IsClientType(baseTypeName) || IsClientType(resolvedBaseType))
        {
            var clientMethod = serviceMethod ?? access.Name switch
            {
                GenericNameSyntax genericName => genericName.Identifier.Text,
                IdentifierNameSyntax identifierName => identifierName.Identifier.Text,
                _ => access.Name.ToString()
            };

            var normalizedVerb = NormalizeHttpVerb(clientMethod);
            var httpVerb = normalizedVerb ?? clientMethod?.ToUpperInvariant();
            ExpressionSyntax? routeLiteral = null;
            if (invocation.ArgumentList is { Arguments.Count: > 0 } httpArguments)
            {
                routeLiteral = httpArguments.Arguments[0].Expression;
            }
            var relativePath = ExtractRouteLiteral(tree, routeLiteral) ?? ResolveRouteFromExpression(routeLiteral, info.LocalStringValues);
            if (!string.IsNullOrWhiteSpace(relativePath))
            {
                var q = relativePath!.IndexOf('?', StringComparison.Ordinal);
                if (q >= 0) relativePath = relativePath![..q];
            }
            var clientType = !string.Equals(resolvedBaseType, baseTypeName, StringComparison.Ordinal)
                ? resolvedBaseType
                : baseTypeName;
            var line = GetLineNumber(tree, invocation);
            var targetService = ResolveClientTargetService(clientType);
            info.HttpClientInvocations.Add(new ControllerClientInvocation(clientType, httpVerb, relativePath, line, clientMethod, targetService));
        }
        else
        {
            // Treat service wrappers (e.g., DataverseService) that expose HTTP-shaped methods as client calls
            var methodName = serviceMethod ?? access.Name switch
            {
                GenericNameSyntax g => g.Identifier.Text,
                IdentifierNameSyntax i => i.Identifier.Text,
                _ => access.Name.ToString()
            };
            var inferredVerb = TryInferHttpMethodFromWrapperName(methodName) ?? NormalizeHttpVerb(methodName);
            ExpressionSyntax? routeExpr = null;
            if (invocation.ArgumentList is { Arguments.Count: > 0 } wrapperArgs)
            {
                routeExpr = wrapperArgs.Arguments[0].Expression;
            }
            var inferredRoute = ExtractRouteLiteral(tree, routeExpr) ?? ResolveRouteFromExpression(routeExpr, info.LocalStringValues);
            if (!string.IsNullOrWhiteSpace(inferredRoute))
            {
                var q2 = inferredRoute!.IndexOf('?', StringComparison.Ordinal);
                if (q2 >= 0) inferredRoute = inferredRoute![..q2];
            }
            if (inferredVerb is not null && !string.IsNullOrWhiteSpace(inferredRoute))
            {
                var clientType = !string.Equals(resolvedBaseType, baseTypeName, StringComparison.Ordinal)
                    ? resolvedBaseType
                    : baseTypeName;
                var line = GetLineNumber(tree, invocation);
                var targetService = ResolveClientTargetService(clientType);
                info.HttpClientInvocations.Add(new ControllerClientInvocation(clientType, inferredVerb, inferredRoute, line, methodName, targetService));
                RecordControllerHttpClientFact(info, clientType, inferredVerb, inferredRoute, methodName, line);
                recordedServiceUsage = true;
            }
        }
        if (!recordedServiceUsage && qualifiedType.Contains("IMapper", StringComparison.Ordinal) && access.Name is GenericNameSyntax mapperGeneric && mapperGeneric.Identifier.Text == "Map")
        {
            var destination = mapperGeneric.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
            string? sourceExpression = null;
            if (invocation.ArgumentList is { Arguments.Count: > 0 } mapperArguments)
            {
                sourceExpression = mapperArguments.Arguments[0].Expression?.ToString();
            }
            var sourceType = sourceExpression is not null && parameterTypes.TryGetValue(sourceExpression, out var resolved)
                ? resolved
                : null;
            var line = GetLineNumber(tree, invocation);
            if (!string.IsNullOrWhiteSpace(assignedVariable) && !string.IsNullOrWhiteSpace(destination))
            {
                info.LocalVariables[assignedVariable!] = destination!;
            }

            info.MappingInvocations.Add(new ControllerMappingInvocation(sourceType, destination, assignedVariable, line));
            recordedServiceUsage = true;
        }
        else if (!recordedServiceUsage && qualifiedType.Contains("IMapper", StringComparison.Ordinal) && access.Name is IdentifierNameSyntax { Identifier.Text: "Map" })
        {
            string? destination = null;
            string? sourceExpression = null;
            if (invocation.ArgumentList is { Arguments.Count: > 0 } mapperArguments)
            {
                var arguments = mapperArguments.Arguments;
                sourceExpression = arguments[0].Expression?.ToString();
                if (arguments.Count > 1)
                {
                    destination = arguments[1].Expression?.ToString();
                }
            }
            var sourceType = sourceExpression is not null && parameterTypes.TryGetValue(sourceExpression, out var resolved)
                ? resolved
                : null;
            var line = GetLineNumber(tree, invocation);
            if (!string.IsNullOrWhiteSpace(assignedVariable) && !string.IsNullOrWhiteSpace(destination))
            {
                info.LocalVariables[assignedVariable!] = destination!;
            }

            info.MappingInvocations.Add(new ControllerMappingInvocation(sourceType, destination, assignedVariable, line));
            recordedServiceUsage = true;
        }
        else if (qualifiedType.StartsWith("IValidator", StringComparison.OrdinalIgnoreCase))
        {
            var line = GetLineNumber(tree, invocation);
            var validatorType = ExtractGenericArgument(qualifiedType) ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(validatorType))
            {
                info.ValidatorInvocations.Add(new ControllerValidatorInvocation(validatorType, line));
            }
        }
        else if (IsCacheService(resolvedType) || IsCacheService(qualifiedType))
        {
            var cacheType = IsCacheService(resolvedType) ? resolvedType : qualifiedType;
            if (TryCaptureCacheInvocation(access, invocation, cacheType, tree) is { } cacheInvocation)
            {
                info.CacheInvocations.Add(cacheInvocation);
            }
        }
        else if (IsRepositoryType(resolvedType) || IsRepositoryType(qualifiedType))
        {
            var repositoryType = IsRepositoryType(resolvedType) ? resolvedType : qualifiedType;
            string? repositoryEntityType = null;
            if (string.IsNullOrWhiteSpace(serviceTargetType))
            {
                var genericArgument = SplitGenericArguments(qualifiedType).FirstOrDefault()
                    ?? SplitGenericArguments(typeName).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(genericArgument))
                {
                    serviceTargetType = QualifyTypeName(genericArgument, info.Assembly, info.Project) ?? genericArgument;
                }
            }

            if (TryCaptureRepositoryInvocation(
                    access,
                    invocation,
                    repositoryType,
                    qualifiedType,
                    tree,
                    info.Assembly,
                    info.Project) is { } repositoryInvocation)
            {
                info.RepositoryInvocations.Add(repositoryInvocation);

                if (!string.IsNullOrWhiteSpace(repositoryInvocation.EntityType))
                {
                    repositoryEntityType = repositoryInvocation.EntityType;
                    var innermost = ExtractInnermostGenericType(repositoryEntityType);
                    if (!string.IsNullOrWhiteSpace(innermost))
                    {
                        repositoryEntityType = innermost;
                    }

                    repositoryEntityType = QualifyTypeName(repositoryEntityType!, info.Assembly, info.Project) ?? repositoryEntityType;
                    serviceTargetType = repositoryEntityType;
                }
            }

            if (string.IsNullOrWhiteSpace(repositoryEntityType) && !string.IsNullOrWhiteSpace(serviceTargetType))
            {
                repositoryEntityType = serviceTargetType;
            }

            if (!string.IsNullOrWhiteSpace(repositoryEntityType))
            {
                var invocationKey = invocation.ToString();
                info.LocalVariables[invocationKey] = repositoryEntityType!;
                info.DomainLocalTypes[invocationKey] = repositoryEntityType!;
                var normalizedInvocationKey = NormalizeExpressionKey(invocationKey);
                if (!string.Equals(invocationKey, normalizedInvocationKey, StringComparison.Ordinal))
                {
                    info.LocalVariables[normalizedInvocationKey] = repositoryEntityType!;
                    info.DomainLocalTypes[normalizedInvocationKey] = repositoryEntityType!;
                }

            }

            if (!string.IsNullOrWhiteSpace(assignedVariable) && !string.IsNullOrWhiteSpace(repositoryEntityType))
            {
                info.LocalVariables[assignedVariable!] = repositoryEntityType!;
                info.DomainLocalVariables.Add(assignedVariable!);
                info.DomainLocalTypes[assignedVariable!] = repositoryEntityType!;
                repositoryEntityResolved = true;
            }

            if (!string.IsNullOrWhiteSpace(resolvedType) && resolvedType.Contains("Repository", StringComparison.Ordinal))
            {
                serviceTypeName = resolvedType;
            }
            else if (!string.IsNullOrWhiteSpace(serviceTargetType))
            {
                var interfaceBase = GetTypeNameWithoutGenerics(typeName);
                if (string.IsNullOrWhiteSpace(interfaceBase))
                {
                    interfaceBase = GetTypeNameWithoutGenerics(qualifiedType);
                }

                if (!string.IsNullOrWhiteSpace(interfaceBase))
                {
                    serviceTypeName = $"{interfaceBase}<{serviceTargetType}>";
                }
            }

            return;
        }
        else if (TryResolveOptionsType(resolvedType) is { } optionsType)
        {
            info.OptionsUsages.Add(new OptionsUsage(optionsType, serviceLine));
            recordedServiceUsage = true;
        }
        else if (IsServiceType(resolvedType) || IsServiceType(qualifiedType))
        {
            recordedServiceUsage = true;
        }
        else if (qualifiedType.Contains("IMediator", StringComparison.Ordinal) || qualifiedType.Contains("IPublisher", StringComparison.Ordinal))
        {
            var methodIdentifier = access.Name.Identifier.Text;
            if (methodIdentifier.StartsWith("Publish", StringComparison.Ordinal))
            {
                ExpressionSyntax? argument = null;
                if (invocation.ArgumentList is { Arguments.Count: > 0 } publishArguments)
                {
                    argument = publishArguments.Arguments[0].Expression;
                }
                var notificationType = argument switch
                {
                    ObjectCreationExpressionSyntax creation => creation.Type.ToString(),
                    IdentifierNameSyntax identifierArgument => TryResolveExpressionType(identifierArgument, parameterTypes, info.LocalVariables, info.Assembly, info.Project, fieldLookup),
                    _ => null
                };

                if (!string.IsNullOrWhiteSpace(notificationType))
                {
                    info.NotificationInvocations.Add(new ControllerNotificationInvocation(notificationType!, serviceLine));
                }

                recordedServiceUsage = true;
            }
            else if (methodIdentifier.StartsWith("Send", StringComparison.Ordinal))
            {
                ExpressionSyntax? argument = null;
                string? argumentIdentifier = null;
                if (invocation.ArgumentList is { Arguments.Count: > 0 } sendArguments)
                {
                    argument = sendArguments.Arguments[0].Expression;
                    if (argument is IdentifierNameSyntax idSyntax)
                    {
                        argumentIdentifier = idSyntax.Identifier.Text;
                    }
                }

                string? requestTypeCandidate = argument switch
                {
                    ObjectCreationExpressionSyntax creation => creation.Type.ToString(),
                    IdentifierNameSyntax identifierArgument => TryResolveExpressionType(identifierArgument, parameterTypes, info.LocalVariables, info.Assembly, info.Project, fieldLookup),
                    MemberAccessExpressionSyntax memberAccessExpr => TryResolveExpressionType(memberAccessExpr.Expression, parameterTypes, info.LocalVariables, info.Assembly, info.Project, fieldLookup),
                    _ => null
                };

                if ((string.IsNullOrWhiteSpace(requestTypeCandidate) || string.Equals(requestTypeCandidate, methodIdentifier, StringComparison.OrdinalIgnoreCase)) && !string.IsNullOrWhiteSpace(argumentIdentifier))
                {
                    var mapped = info.MappingInvocations.LastOrDefault(m => string.Equals(m.AssignedVariable, argumentIdentifier, StringComparison.OrdinalIgnoreCase));
                    if (mapped is not null && !string.IsNullOrWhiteSpace(mapped.DestinationType))
                    {
                        requestTypeCandidate = mapped.DestinationType;
                    }
                }

                if (!string.IsNullOrWhiteSpace(requestTypeCandidate))
                {
                    var qualifiedRequest = QualifyTypeName(requestTypeCandidate!, info.Assembly, info.Project);
                    if (!string.IsNullOrWhiteSpace(qualifiedRequest))
                    {
                        var existing = info.RequestInvocations.FirstOrDefault(r => r.Line == serviceLine);
                        var shouldRecordFact = true;
                        if (existing is not null)
                        {
                            if (existing.RequestType.Equals(qualifiedRequest, StringComparison.OrdinalIgnoreCase))
                            {
                                shouldRecordFact = false;
                            }
                            else
                            {
                                info.RequestInvocations.Remove(existing);
                                info.RequestInvocations.Add(new ControllerRequestInvocation(qualifiedRequest!, serviceLine));
                            }
                        }
                        else
                        {
                            info.RequestInvocations.Add(new ControllerRequestInvocation(qualifiedRequest!, serviceLine));
                        }

                        if (shouldRecordFact)
                        {
                            RecordControllerRequestFact(info, qualifiedRequest!, methodIdentifier, serviceLine);
                        }
                    }
                }

                recordedServiceUsage = true;
            }
        }
        else if ((qualifiedType.Contains("IRequestProcessor", StringComparison.Ordinal) || qualifiedType.Contains("RequestProcessor", StringComparison.Ordinal)) &&
                 (string.Equals(serviceMethod, "Process", StringComparison.OrdinalIgnoreCase) || string.Equals(serviceMethod, "ProcessAsync", StringComparison.OrdinalIgnoreCase)))
        {
            recordedServiceUsage = true;
        }

        if (recordedServiceUsage)
        {
            string? requestType = null;
            string? responseType = null;
            string? dispatchKind = null;

            // IRequestProcessor dynamic dispatch capture
            if ((qualifiedType.Contains("IRequestProcessor", StringComparison.Ordinal) || resolvedType.Contains("RequestProcessor", StringComparison.Ordinal)) &&
                (string.Equals(serviceMethod, "Process", StringComparison.OrdinalIgnoreCase) || string.Equals(serviceMethod, "ProcessAsync", StringComparison.OrdinalIgnoreCase)))
            {
                // Generic TResult (ProcessAsync<TResult>/Process<TResult>)
                if (access.Name is GenericNameSyntax g && g.TypeArgumentList.Arguments.Count > 0)
                {
                    responseType = QualifyTypeName(g.TypeArgumentList.Arguments[0].ToString(), info.Assembly, info.Project);
                }

                // First argument is the request instance
                ExpressionSyntax? argExpr = null;
                if (invocation.ArgumentList is { Arguments.Count: > 0 } processorArguments)
                {
                    argExpr = processorArguments.Arguments[0].Expression;
                }
                if (argExpr is ObjectCreationExpressionSyntax creation)
                {
                    requestType = QualifyTypeName(creation.Type.ToString(), info.Assembly, info.Project);
                }
                else if (argExpr is IdentifierNameSyntax idArg)
                {
                    // Try resolve via parameters or locals
                    requestType = TryResolveExpressionType(idArg, parameterTypes, info.LocalVariables, info.Assembly, info.Project, fieldLookup);
                }
                else if (argExpr is MemberAccessExpressionSyntax memberAccessExpr)
                {
                    // Heuristic: attempt to resolve base expression type
                    if (memberAccessExpr.Expression is IdentifierNameSyntax memberRoot)
                    {
                        requestType = TryResolveExpressionType(memberRoot, parameterTypes, info.LocalVariables, info.Assembly, info.Project, fieldLookup);
                    }
                }

                if (!string.IsNullOrWhiteSpace(requestType))
                {
                    requestType = QualifyTypeName(requestType!, info.Assembly, info.Project);
                }
                if (!string.IsNullOrWhiteSpace(responseType))
                {
                    responseType = QualifyTypeName(responseType!, info.Assembly, info.Project);
                }

                if (!string.IsNullOrWhiteSpace(requestType))
                {
                    dispatchKind = "requestprocessor.dispatch";
                    // Also record a controller-level request fact so downstream renderers have invocation context
                    // Use the current method identifier (e.g., Process/ProcessAsync) for richer narrative
                    var methodIdentifier = access.Name is GenericNameSyntax gg ? gg.Identifier.Text : access.Name.Identifier.Text;
                    RecordControllerRequestFact(info, requestType!, methodIdentifier, serviceLine);
                }
            }

            info.ServiceUsages.Add(new ServiceUsage(serviceTypeName, serviceLine, serviceMethod, serviceMethod, requestType, responseType, dispatchKind, serviceTargetType));
        }

        if (!string.IsNullOrWhiteSpace(serviceMethod) && !IsDomainType(qualifiedType))
        {
            if (!string.IsNullOrWhiteSpace(assignedVariable) &&
                info.LocalVariables.TryGetValue(assignedVariable, out var assignedValue) &&
                !string.IsNullOrWhiteSpace(assignedValue) &&
                string.Equals(assignedValue, qualifiedType, StringComparison.Ordinal))
            {
                return;
            }

            var guessedProduct = GuessServiceTypeFromFactory(qualifiedType, serviceMethod);
            if (!string.IsNullOrWhiteSpace(guessedProduct))
            {
                var qualifiedProduct = QualifyTypeName(guessedProduct, info.Assembly, info.Project) ?? guessedProduct;

                void AssignGuessToLocal(string variableName)
                {
                    if (string.IsNullOrWhiteSpace(variableName))
                    {
                        return;
                    }

                    if (info.DomainLocalVariables.Contains(variableName))
                    {
                        return;
                    }

                    if (info.LocalVariables.TryGetValue(variableName, out var existingValue) &&
                        !string.IsNullOrWhiteSpace(existingValue) &&
                        !string.Equals(existingValue, "var", StringComparison.OrdinalIgnoreCase))
                    {
                        // Preserve existing precise type inference.
                        return;
                    }

                    info.LocalVariables[variableName] = qualifiedProduct;
                }

                if (invocation.Parent is EqualsValueClauseSyntax equalsClause && equalsClause.Parent is VariableDeclaratorSyntax declarator)
                {
                    AssignGuessToLocal(declarator.Identifier.Text);
                }
                else if (invocation.Parent is AssignmentExpressionSyntax assignExpression && assignExpression.Left is IdentifierNameSyntax assignIdentifier)
                {
                    AssignGuessToLocal(assignIdentifier.Identifier.Text);
                }
                else if (invocation.Parent is AwaitExpressionSyntax awaited && awaited.Parent is EqualsValueClauseSyntax awaitedEquals && awaitedEquals.Parent is VariableDeclaratorSyntax awaitedDeclarator)
                {
                    AssignGuessToLocal(awaitedDeclarator.Identifier.Text);
                }
                else if (invocation.Parent is AwaitExpressionSyntax awaitedAssign && awaitedAssign.Parent is AssignmentExpressionSyntax awaitedAssignExpression && awaitedAssignExpression.Left is IdentifierNameSyntax awaitedIdentifier)
                {
                    AssignGuessToLocal(awaitedIdentifier.Identifier.Text);
                }
            }
        }
    }

    private string BuildControllerMethodSymbolId(string controllerFqdn, MethodDeclarationSyntax method, ProjectInfo project)
    {
        var methodName = method.Identifier.Text;
        if (string.IsNullOrWhiteSpace(controllerFqdn) || string.IsNullOrWhiteSpace(methodName))
        {
            return $"M:{controllerFqdn}.{methodName}";
        }

        var parameters = method.ParameterList?.Parameters ?? default;
        if (parameters.Count == 0)
        {
            return $"M:{controllerFqdn}.{methodName}";
        }

        var parts = new List<string>(parameters.Count);
        foreach (var parameter in parameters)
        {
            if (parameter is null)
            {
                continue;
            }

            var modifier = parameter.Modifiers.Count == 0
                ? string.Empty
                : string.Join(" ", parameter.Modifiers.Select(m => m.Text)).Trim();

            var rawType = parameter.Type?.ToString();
            var qualifiedType = string.IsNullOrWhiteSpace(rawType)
                ? null
                : QualifyTypeName(rawType!, project.AssemblyName, project.RelativeDirectory);
            var parameterType = string.IsNullOrWhiteSpace(qualifiedType) ? rawType : qualifiedType;
            if (string.IsNullOrWhiteSpace(parameterType))
            {
                parameterType = "object";
            }

            var segment = string.IsNullOrWhiteSpace(modifier)
                ? parameterType
                : $"{modifier} {parameterType}";

            parts.Add(segment.Trim());
        }

        if (parts.Count == 0)
        {
            return $"M:{controllerFqdn}.{methodName}";
        }

        var signature = string.Join(",", parts);
        return $"M:{controllerFqdn}.{methodName}({signature})";
    }

    private bool ReturnsTaskLike(TypeSyntax? returnType, SemanticModel model)
    {
        if (returnType is null)
        {
            return false;
        }

        TypeInfo typeInfo;
        try
        {
            typeInfo = model.GetTypeInfo(returnType);
        }
        catch (ArgumentException)
        {
            try
            {
                var root = model.SyntaxTree.GetRoot();
                if (returnType.SyntaxTree != model.SyntaxTree)
                {
                    var mapped = root.FindNode(returnType.Span, getInnermostNodeForTie: true) as TypeSyntax;
                    if (mapped is null)
                    {
                        return false;
                    }

                    typeInfo = model.GetTypeInfo(mapped);
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        var type = typeInfo.Type as INamedTypeSymbol;
        if (type is null)
        {
            return false;
        }

        var index = GraphKit.Resolution.RoslynTypeIndex.Get(model.Compilation);
        return index.IsTaskLike(type);
    }

    private string? GuessServiceTypeFromInitializer(
        InvocationExpressionSyntax invocation,
        IReadOnlyDictionary<string, FieldDescriptor> fieldLookup,
        IReadOnlyDictionary<string, string?> parameterTypes,
        Dictionary<string, string> localVariables,
        string? preferredAssembly,
        string? preferredProject)
    {
        if (invocation.Expression is not MemberAccessExpressionSyntax access)
        {
            return null;
        }

    var factoryType = TryResolveExpressionType(access.Expression, parameterTypes, localVariables, preferredAssembly, preferredProject, fieldLookup);
        if (factoryType is null)
        {
            if (access.Expression is IdentifierNameSyntax identifier &&
                fieldLookup.TryGetValue(identifier.Identifier.Text.TrimStart('_'), out var descriptor))
            {
                factoryType = descriptor.Type;
            }
            else if (access.Expression is MemberAccessExpressionSyntax nestedAccess &&
                     nestedAccess.Expression is IdentifierNameSyntax nestedIdentifier &&
                     fieldLookup.TryGetValue(nestedIdentifier.Identifier.Text.TrimStart('_'), out var nestedDescriptor))
            {
                factoryType = nestedDescriptor.Type;
            }
        }

        var methodName = GetMemberName(access.Name);
        if (string.IsNullOrWhiteSpace(methodName))
        {
            return null;
        }

        return GuessServiceTypeFromFactory(factoryType, methodName);
    }

    private string? GuessServiceTypeFromFactory(string? factoryType, string? methodName)
    {
        if (string.IsNullOrWhiteSpace(methodName))
        {
            return null;
        }

        var lookupMethods = new List<string> { methodName! };
        if (methodName.EndsWith("Async", StringComparison.Ordinal))
        {
            var alternate = methodName[..^5];
            if (!string.IsNullOrWhiteSpace(alternate))
            {
                lookupMethods.Add(alternate);
            }
        }

        if (!string.IsNullOrWhiteSpace(factoryType))
        {
            foreach (var key in GetFactoryLookupKeys(factoryType!))
            {
                if (_interfaceMethodReturnTypes.TryGetValue(key, out var interfaceMethods))
                {
                    foreach (var candidateMethod in lookupMethods)
                    {
                        if (interfaceMethods.TryGetValue(candidateMethod, out var returnType) && !string.IsNullOrWhiteSpace(returnType))
                        {
                            return QualifyTypeName(returnType);
                        }
                    }
                }
            }
        }

        var productName = StripFactoryMethodPrefixes(methodName!);
        if (string.IsNullOrWhiteSpace(productName))
        {
            return null;
        }

        var candidates = new List<string>();
        var candidateSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        void AddCandidate(string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && candidateSet.Add(value))
            {
                candidates.Add(value);
            }
        }

        AddCandidate(productName);
        if (!productName.StartsWith("I", StringComparison.Ordinal))
        {
            AddCandidate($"I{productName}");
        }

        if (!productName.EndsWith("Service", StringComparison.OrdinalIgnoreCase))
        {
            var serviceName = $"{productName}Service";
            AddCandidate(serviceName);
            if (!serviceName.StartsWith("I", StringComparison.Ordinal))
            {
                AddCandidate($"I{serviceName}");
            }
        }

        foreach (var candidate in candidates)
        {
            if (FindServiceRegistration(candidate) is { } registration)
            {
                return registration.ServiceType;
            }

            var match = _services.Values.FirstOrDefault(s =>
                s.Name.Equals(candidate, StringComparison.OrdinalIgnoreCase) ||
                s.Fqdn.Equals(candidate, StringComparison.OrdinalIgnoreCase));
            if (match is not null)
            {
                return match.Fqdn;
            }
        }

        var factoryNamespace = ExtractNamespace(factoryType);
        if (!string.IsNullOrWhiteSpace(factoryNamespace))
        {
            foreach (var candidate in candidates)
            {
                var qualified = $"{factoryNamespace}.{candidate}";
                if (FindServiceRegistration(qualified) is { } registration)
                {
                    return registration.ServiceType;
                }

                if (_services.TryGetValue(qualified, out var serviceInfo))
                {
                    return serviceInfo.Fqdn;
                }
            }
        }

        return candidates.FirstOrDefault();
    }

    private IEnumerable<string> GetFactoryLookupKeys(string factoryType)
    {
        var keys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var topLevelSimples = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        void Add(string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                keys.Add(value);
            }
        }

    var qualifiedFactory = QualifyTypeName(factoryType);
        Add(factoryType);
        Add(qualifiedFactory);

        foreach (var typeName in new[] { factoryType, qualifiedFactory })
        {
            if (string.IsNullOrWhiteSpace(typeName))
            {
                continue;
            }

            var topLevel = GetTopLevelSimpleIdentifier(typeName);
            Add(topLevel);
            if (!string.IsNullOrWhiteSpace(topLevel))
            {
                topLevelSimples.Add(topLevel);
                if (topLevel.Length > 1 && topLevel[0] == 'I' && char.IsUpper(topLevel[1]))
                {
                    var interfaceCandidate = topLevel[1..];
                    Add(interfaceCandidate);
                    topLevelSimples.Add(interfaceCandidate);
                }
            }

            var innermost = GetSimpleIdentifier(typeName);
            Add(innermost);
        }

        var factoryNamespace = ExtractNamespace(factoryType);
        if (!string.IsNullOrWhiteSpace(factoryNamespace))
        {
            foreach (var simple in topLevelSimples)
            {
                Add($"{factoryNamespace}.{simple}");
                if (simple.Length > 1 && simple[0] == 'I' && char.IsUpper(simple[1]))
                {
                    Add($"{factoryNamespace}.{simple[1..]}");
                }
            }
        }

        return keys;
    }

    private static string StripFactoryMethodPrefixes(string methodName)
    {
        var value = methodName;
        if (value.EndsWith("Async", StringComparison.Ordinal))
        {
            value = value[..^5];
        }

        var prefixes = new[]
        {
            "GetOrCreate",
            "GetOrSet",
            "Get",
            "Create",
            "Build",
            "Resolve",
            "Provide",
            "Ensure",
            "Fetch",
            "Retrieve",
            "Load",
            "Make"
        };

        foreach (var prefix in prefixes)
        {
            if (value.StartsWith(prefix, StringComparison.Ordinal) && value.Length > prefix.Length)
            {
                return value[prefix.Length..];
            }
        }

        return value;
    }

    private static string? ExtractNamespace(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return null;
        }

        var namespaceValue = GetTypeNamespace(typeName);
        return string.IsNullOrWhiteSpace(namespaceValue) ? null : namespaceValue;
    }

    private static int? TryParseStatusCodeExpression(string? expr)
    {
        if (string.IsNullOrWhiteSpace(expr)) return null;
        expr = expr.Trim();
        if (int.TryParse(expr, out var numeric)) return numeric;
        // Handle nameof(StatusCodes.Status201Created) style (rare) by ignoring nameof(
        if (expr.StartsWith("nameof(", StringComparison.Ordinal) && expr.EndsWith(')'))
        {
            expr = expr.Substring(7, expr.Length - 8);
        }
        var segments = expr.Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var candidate = segments.LastOrDefault();
        if (candidate is not null)
        {
            // e.g. Status200OK, Status404NotFound
            var digits = new string(candidate.Where(char.IsDigit).ToArray());
            if (int.TryParse(digits, out var parsed)) return parsed;
        }
        return null;
    }

    private void EmitControllers()
    {
        foreach (var action in _controllerActions.Values)
        {
            EnsureControllerFactNode(action);
            var id = StableId.For("endpoint.controller", action.Fqdn, action.Assembly, action.SymbolId);
            var nodeProps = new Dictionary<string, object>
            {
                ["route"] = action.Route,
                ["http_method"] = action.HttpMethod,
                ["verb"] = action.HttpMethod,
                ["controller_display"] = action.Fqdn
            };

            if (action.StatusCodes.Count > 0)
            {
                nodeProps["status_codes"] = action.StatusCodes.OrderBy(c => c).ToArray();
            }

            if (action.Authorizations.Count > 0)
            {
                nodeProps["authorization"] = action.Authorizations
                    .Select(CreateAuthorizationProps)
                    .ToList();
            }

            if (action.AllowsAnonymous)
            {
                nodeProps["allow_anonymous"] = true;
            }

            var authLabel = BuildAuthLabel(action.AllowsAnonymous, action.Authorizations);
            if (!string.IsNullOrWhiteSpace(authLabel))
            {
                nodeProps["auth"] = authLabel!;
            }

            var domainSummaries = action.DomainInvocations
                .GroupBy(d => new { d.TargetType, d.Method, d.Instance, d.AssignedVariable })
                .Select(g => g.OrderBy(x => x.Line).First())
                .ToList();

            if (domainSummaries.Count > 0)
            {
                var domainProps = new List<Dictionary<string, object>>();
                foreach (var domainCall in domainSummaries)
                {
                    var domainEntry = new Dictionary<string, object>
                    {
                        ["type"] = domainCall.TargetType,
                        ["method"] = domainCall.Method,
                        ["line"] = domainCall.Line
                    };

                    if (!string.IsNullOrWhiteSpace(domainCall.Instance))
                    {
                        domainEntry["instance"] = domainCall.Instance!;
                    }

                    if (!string.IsNullOrWhiteSpace(domainCall.AssignedVariable))
                    {
                        domainEntry["variable"] = domainCall.AssignedVariable!;
                    }

                    domainProps.Add(domainEntry);
                }

                nodeProps["domain_calls"] = domainProps;
            }

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
                Props = nodeProps
            };
            _nodes[id] = node;

            if (domainSummaries.Count > 0)
            {
                foreach (var domainCall in domainSummaries)
                {
                    if (string.IsNullOrWhiteSpace(domainCall.TargetType))
                    {
                        continue;
                    }

                    NodeReference? domainReference = null;
                    var candidateTypes = new List<string?>
                    {
                        domainCall.TargetType,
                        GetTypeNameWithoutGenerics(domainCall.TargetType),
                        GetTopLevelSimpleIdentifier(domainCall.TargetType)
                    };

                    foreach (var candidate in candidateTypes)
                    {
                        if (string.IsNullOrWhiteSpace(candidate))
                        {
                            continue;
                        }

                        if (TryResolveNodeReference(candidate!, out var reference, action.Assembly, action.Project))
                        {
                            domainReference = reference;
                            break;
                        }
                    }

                    if (domainReference is null)
                    {
                        continue;
                    }

                    var edgeProps = new Dictionary<string, object>
                    {
                        ["method"] = domainCall.Method,
                        ["target_type"] = domainCall.TargetType
                    };

                    if (!string.IsNullOrWhiteSpace(domainCall.Instance))
                    {
                        edgeProps["instance"] = domainCall.Instance!;
                    }

                    if (!string.IsNullOrWhiteSpace(domainCall.AssignedVariable))
                    {
                        edgeProps["variable"] = domainCall.AssignedVariable!;
                    }

                    _edges.Add(new GraphEdge
                    {
                        From = id,
                        To = domainReference.Id,
                        Kind = "invokes_domain",
                        Source = "static",
                        Confidence = 0.9,
                        Transform = new GraphTransform
                        {
                            Type = "domain.invocation",
                            Location = new GraphLocation { File = action.FilePath, Line = domainCall.Line }
                        },
                        Props = edgeProps,
                        Evidence = CreateEvidence(action.FilePath, domainCall.Line)
                    });
                }
            }

            foreach (var request in action.RequestInvocations)
            {
                var requestInfo = FindRequestByType(request.RequestType, preferredAssembly: action.Assembly, preferredProject: action.Project);
                var downstreamHandler = FindHandlerForRequest(request.RequestType);

                string? responseType = null;
                if (!string.IsNullOrWhiteSpace(requestInfo?.ResponseType))
                {
                    responseType = requestInfo!.ResponseType;
                }
                else if (downstreamHandler is not null)
                {
                    var matchingSignature = downstreamHandler.RequestSignatures.FirstOrDefault(sig => sig.RequestType.Equals(request.RequestType, StringComparison.OrdinalIgnoreCase));
                    responseType = matchingSignature?.ResponseType ?? downstreamHandler.ResponseType;
                }

                if (!string.IsNullOrWhiteSpace(responseType) && IsGenericPlaceholder(responseType))
                {
                    responseType = null;
                }

                if (requestInfo is not null)
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
                            ["request_type"] = request.RequestType,
                            ["response_type"] = responseType ?? string.Empty
                        },
                        Evidence = CreateEvidence(action.FilePath, request.Line)
                    });
                }

                if (downstreamHandler is not null)
                {
                    var handlerId = StableId.For("cqrs.handler", downstreamHandler.Fqdn, downstreamHandler.Assembly, downstreamHandler.SymbolId);
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
                        Props = new Dictionary<string, object>
                        {
                            ["request_type"] = request.RequestType,
                            ["handler"] = downstreamHandler.Fqdn,
                            ["response_type"] = responseType ?? string.Empty
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
                    TryResolveNodeReference(repository.RepositoryType, out var directReference, action.Assembly, action.Project))
                {
                    repositoryReference = directReference;
                }
                else if (!string.IsNullOrWhiteSpace(repository.EntityType))
                {
                    var derivedRepositoryType = repository.EntityType.EndsWith("Repository", StringComparison.Ordinal)
                        ? repository.EntityType
                        : $"{repository.EntityType}Repository";
                    if (TryResolveNodeReference(derivedRepositoryType, out var derivedReference, action.Assembly, action.Project))
                    {
                        repositoryReference = derivedReference;
                    }
                }

                if (repositoryReference is not null)
                {
                    var props = new Dictionary<string, object>
                    {
                        ["method"] = repository.Method,
                        ["operation"] = repository.Operation
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
                    TryResolveNodeReference(repository.EntityType, out var entityReference, action.Assembly, action.Project))
                {
                    var kind = repository.Operation switch
                    {
                        "insert" => "inserts_into",
                        "update" => "updates",
                        "delete" => "deletes_from",
                        "upsert" => "upserts",
                        "write" => "writes_to",
                        _ => "queries"
                    };
                    var transformType = kind switch
                    {
                        "inserts_into" => "repository.insert",
                        "updates" => "repository.update",
                        "deletes_from" => "repository.delete",
                        "upserts" => "repository.upsert",
                        "writes_to" => "repository.write",
                        _ => "repository.query"
                    };
                    _edges.Add(new GraphEdge
                    {
                        From = id,
                        To = entityReference.Id,
                        Kind = kind,
                        Source = "static",
                        Confidence = 1.0,
                        Transform = new GraphTransform
                        {
                            Type = transformType,
                            Location = new GraphLocation { File = action.FilePath, Line = repository.Line }
                        },
                        Props = new Dictionary<string, object>
                        {
                            ["operation"] = repository.Operation
                        },
                        Evidence = CreateEvidence(action.FilePath, repository.Line)
                    });
                }
            }

            // Heuristic: infer entity writes from domain mutations performed on instances originating from repositories
            if (domainSummaries.Count > 0 && action.DomainLocalTypes.Count > 0)
            {
                foreach (var domainCall in domainSummaries)
                {
                    if (string.IsNullOrWhiteSpace(domainCall.Instance) || string.IsNullOrWhiteSpace(domainCall.Method))
                    {
                        continue;
                    }

                    if (!action.DomainLocalTypes.TryGetValue(domainCall.Instance!, out var boundEntityType) || string.IsNullOrWhiteSpace(boundEntityType))
                    {
                        var normalizedInstance = NormalizeExpressionKey(domainCall.Instance!);
                        if (!string.IsNullOrWhiteSpace(normalizedInstance))
                        {
                            action.DomainLocalTypes.TryGetValue(normalizedInstance!, out boundEntityType);
                        }
                    }

                    if (string.IsNullOrWhiteSpace(boundEntityType))
                    {
                        continue;
                    }

                    if (!IsLikelyMutationMethod(domainCall.Method))
                    {
                        continue;
                    }

                    if (TryResolveNodeReference(boundEntityType!, out var entityRef, action.Assembly, action.Project))
                    {
                        _edges.Add(new GraphEdge
                        {
                            From = id,
                            To = entityRef.Id,
                            Kind = "writes_to",
                            Source = "static",
                            Confidence = 1.0,
                            Transform = new GraphTransform
                            {
                                Type = "repository.write",
                                Location = new GraphLocation { File = action.FilePath, Line = domainCall.Line }
                            },
                            Props = new Dictionary<string, object> { ["operation"] = "write" },
                            Evidence = CreateEvidence(action.FilePath, domainCall.Line)
                        });
                    }
                }
            }

            foreach (var serviceGroup in action.ServiceUsages
                .GroupBy(s => s.ServiceType, StringComparer.OrdinalIgnoreCase))
            {
                var primary = serviceGroup
                    .OrderBy(s => s.Line)
                    .First();

                if (!IsServiceUsageInScope(primary, action.Assembly, action.Project))
                {
                    continue;
                }

                if (!TryEnsureServiceNode(primary.ServiceType, out var serviceId, out var registration, primary.TargetType, action.Assembly, action.Project))
                {
                    continue;
                }

                if (IsStorageService(primary.ServiceType))
                {
                    var storageProps = new Dictionary<string, object>
                    {
                        ["service_type"] = primary.ServiceType
                    };

                    if (!string.IsNullOrWhiteSpace(primary.Method))
                    {
                        storageProps["method"] = primary.Method!;
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
                            Location = new GraphLocation { File = action.FilePath, Line = primary.Line }
                        },
                        Props = storageProps,
                        Evidence = CreateEvidence(action.FilePath, primary.Line)
                    });
                }

                var props = new Dictionary<string, object>
                {
                    ["service_type"] = primary.ServiceType
                };

                if (registration is not null)
                {
                    props["lifetime"] = registration.Lifetime;
                }

                if (!string.IsNullOrWhiteSpace(primary.Method))
                {
                    props["method"] = primary.Method!;
                }

                if (!string.IsNullOrWhiteSpace(primary.InvocationMethod) &&
                    !string.Equals(primary.InvocationMethod, primary.Method, StringComparison.Ordinal))
                {
                    props["invoked_method"] = primary.InvocationMethod!;
                }

                if (!string.IsNullOrWhiteSpace(primary.TargetType))
                {
                    props["target_type"] = primary.TargetType!;
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
                        Location = new GraphLocation { File = action.FilePath, Line = primary.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(action.FilePath, primary.Line)
                });

                var dispatchSeen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var usage in serviceGroup)
                {
                    if (string.IsNullOrWhiteSpace(usage.DispatchKind) ||
                        string.IsNullOrWhiteSpace(usage.RequestType))
                    {
                        continue;
                    }

                    var dispatchKey = $"{usage.DispatchKind}|{usage.RequestType}|{usage.ResponseType}";
                    if (!dispatchSeen.Add(dispatchKey))
                    {
                        continue;
                    }

                    var requestType = usage.RequestType!;
                    var requestInfo = FindRequestByType(requestType, preferredAssembly: action.Assembly, preferredProject: action.Project, serviceType: usage.ServiceType) ?? ResolveRequestForDispatch(requestType, action);
                    if (requestInfo is null)
                    {
                        continue;
                    }

                    var downstreamHandler = FindHandlerForRequest(requestType);
                    var responseType = usage.ResponseType;
                    if (string.IsNullOrWhiteSpace(responseType))
                    {
                        if (!string.IsNullOrWhiteSpace(requestInfo.ResponseType))
                        {
                            responseType = requestInfo.ResponseType;
                        }
                        else if (downstreamHandler is not null)
                        {
                            var matchingSignature = downstreamHandler.RequestSignatures.FirstOrDefault(sig => sig.RequestType.Equals(requestType, StringComparison.OrdinalIgnoreCase));
                            responseType = matchingSignature?.ResponseType ?? downstreamHandler.ResponseType;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(responseType) && IsGenericPlaceholder(responseType))
                    {
                        responseType = null;
                    }

                    var requestNodeId = StableId.For("cqrs.request", requestInfo.Fqdn, requestInfo.Assembly, requestInfo.SymbolId);
                    var requestProps = new Dictionary<string, object>
                    {
                        ["service"] = usage.ServiceType,
                        ["invocation"] = usage.InvocationMethod ?? usage.Method ?? string.Empty,
                        ["request_type"] = requestType,
                        ["response_type"] = responseType ?? string.Empty
                    };

                    var pipelineLabels = ResolvePipelineBehaviorsForRequest(requestType);
                    if (pipelineLabels.Count > 0)
                    {
                        requestProps["pipeline_behaviors"] = string.Join(", ", pipelineLabels);
                    }

                    _edges.Add(new GraphEdge
                    {
                        From = id,
                        To = requestNodeId,
                        Kind = "sends_request",
                        Source = "synthetic",
                        Confidence = 0.9,
                        Transform = new GraphTransform
                        {
                            Type = usage.DispatchKind!,
                            Location = new GraphLocation { File = action.FilePath, Line = usage.Line }
                        },
                        Props = requestProps,
                        Evidence = CreateEvidence(action.FilePath, usage.Line)
                    });

                    if (downstreamHandler is not null)
                    {
                        var handlerId = StableId.For("cqrs.handler", downstreamHandler.Fqdn, downstreamHandler.Assembly, downstreamHandler.SymbolId);
                        _edges.Add(new GraphEdge
                        {
                            From = requestNodeId,
                            To = handlerId,
                            Kind = "handled_by",
                            Source = "synthetic",
                            Confidence = 0.85,
                            Transform = new GraphTransform
                            {
                                Type = usage.DispatchKind!,
                                Location = new GraphLocation { File = action.FilePath, Line = usage.Line }
                            },
                            Props = new Dictionary<string, object>
                            {
                                ["request_type"] = requestType,
                                ["handler"] = downstreamHandler.Fqdn,
                                ["response_type"] = responseType ?? string.Empty
                            },
                            Evidence = CreateEvidence(action.FilePath, usage.Line)
                        });
                    }
                }
            }

            EmitConfigurationEdges(id, action.ConfigurationUsages);

            foreach (var clientInvocation in action.HttpClientInvocations)
            {
                var props = CreateClientInvocationProps(clientInvocation);

                var clientSimpleName = GetTopLevelSimpleIdentifier(clientInvocation.ClientType);
                if (_httpClients.Values.FirstOrDefault(client => string.Equals(client.Name, clientSimpleName, StringComparison.Ordinal)) is { } client)
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
                        Props = props,
                        Evidence = CreateEvidence(action.FilePath, clientInvocation.Line)
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

                if (!TryResolveNodeReference(mapping.DestinationType, out var destination, action.Assembly, action.Project))
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

            foreach (var response in action.ResponseUsages
                .Where(r => IsMeaningfulResponseType(r.ResponseType))
                .GroupBy(r => new { r.ResponseType, r.Variable, r.IsReturn })
                .Select(group => group.OrderBy(r => r.Line).First()))
            {
                if (!TryResolveNodeReference(response.ResponseType, out var responseNode, action.Assembly, action.Project))
                {
                    continue;
                }

                var props = new Dictionary<string, object>
                {
                    ["response_type"] = response.ResponseType
                };

                if (!string.IsNullOrWhiteSpace(response.Variable))
                {
                    props["variable"] = response.Variable!;
                }

                if (response.IsReturn)
                {
                    props["kind"] = "return";
                }

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = responseNode.Id,
                    Kind = "returns",
                    Source = "static",
                    Confidence = 1.0,
                    Transform = new GraphTransform
                    {
                        Type = "controller.response",
                        Location = new GraphLocation { File = action.FilePath, Line = response.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(action.FilePath, response.Line)
                });
            }

            foreach (var cast in action.CastInvocations)
            {
                if (string.IsNullOrWhiteSpace(cast.DestinationType))
                {
                    continue;
                }

                if (!TryResolveNodeReference(cast.DestinationType, out var destination, action.Assembly, action.Project))
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
                var validatorInvocationSimple = GetSimpleIdentifier(validator.ValidatorType);
                var validatorInfo = _validators.FirstOrDefault(v =>
                    string.Equals(GetSimpleIdentifier(v.TargetType), validatorInvocationSimple, StringComparison.OrdinalIgnoreCase));
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

            foreach (var validation in action.ValidationCalls)
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
                        Location = new GraphLocation { File = action.FilePath, Line = validation.Line }
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["method"] = validation.Method
                    },
                    Evidence = CreateEvidence(action.FilePath, validation.Line)
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

    private static bool IsLikelyMutationMethod(string methodName)
    {
        if (string.IsNullOrWhiteSpace(methodName)) return false;
        // Common mutation verbs seen across aggregates
        if (methodName.StartsWith("Set", StringComparison.OrdinalIgnoreCase)) return true;
        if (methodName.StartsWith("Add", StringComparison.OrdinalIgnoreCase)) return true;
        if (methodName.StartsWith("Update", StringComparison.OrdinalIgnoreCase)) return true;
        if (methodName.StartsWith("Remove", StringComparison.OrdinalIgnoreCase)) return true;
        if (methodName.StartsWith("Delete", StringComparison.OrdinalIgnoreCase)) return true;
        if (methodName.StartsWith("Create", StringComparison.OrdinalIgnoreCase)) return true;
        if (methodName.StartsWith("Bulk", StringComparison.OrdinalIgnoreCase)) return true;
        // Explicit known cases
        if (string.Equals(methodName, "Activate", StringComparison.OrdinalIgnoreCase)) return true;
        if (string.Equals(methodName, "Restore", StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }

    private string? TryGetRootIdentifier(ExpressionSyntax expression)
    {
        switch (expression)
        {
            case IdentifierNameSyntax identifier:
                return identifier.Identifier.Text;
            case MemberAccessExpressionSyntax memberAccess:
                return TryGetRootIdentifier(memberAccess.Expression);
            case InvocationExpressionSyntax invocation:
                if (invocation.Expression is MemberAccessExpressionSyntax invocationAccess)
                {
                    return TryGetRootIdentifier(invocationAccess.Expression);
                }

                if (invocation.Expression is IdentifierNameSyntax invocationIdentifier)
                {
                    return invocationIdentifier.Identifier.Text;
                }

                return null;
            case ElementAccessExpressionSyntax elementAccess:
                return TryGetRootIdentifier(elementAccess.Expression);
            case ConditionalAccessExpressionSyntax conditionalAccess:
                return TryGetRootIdentifier(conditionalAccess.Expression);
            default:
                return null;
        }
    }

    private static string NormalizeExpressionKey(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        Span<char> buffer = value.Length <= 256 ? stackalloc char[value.Length] : new char[value.Length];
        var index = 0;
        foreach (var ch in value)
        {
            if (!char.IsWhiteSpace(ch))
            {
                buffer[index++] = ch;
            }
        }

        return index == value.Length ? value : new string(buffer[..index]);
    }

        private ControllerRepositoryInvocation? TryCaptureRepositoryInvocation(
            MemberAccessExpressionSyntax access,
            InvocationExpressionSyntax invocation,
            string repositoryType,
            string originalRepositoryType,
            SyntaxTree tree,
            string? preferredAssembly,
            string? preferredProject)
        {
            var methodName = access.Name.Identifier.Text;
            if (string.IsNullOrWhiteSpace(methodName))
            {
                return null;
            }

            var invocationEntityType = ExtractRepositoryEntityTypeFromInvocation(access.Name, invocation);
            var operation = DetermineRepositoryOperation(methodName);
            var entityType = invocationEntityType
                ?? ExtractRepositoryEntityType(repositoryType)
                ?? ExtractRepositoryEntityType(originalRepositoryType)
                ?? TryDeriveEntityTypeFromRepositoryName(repositoryType)
                ?? TryDeriveEntityTypeFromRepositoryName(originalRepositoryType);

            if (!string.IsNullOrWhiteSpace(entityType))
            {
                entityType = QualifyTypeName(entityType!, preferredAssembly, preferredProject);
            }

            var line = GetLineNumber(tree, invocation);
            return new ControllerRepositoryInvocation(repositoryType, entityType, methodName, operation, line);
        }

        private static string? ExtractRepositoryEntityTypeFromInvocation(
            SimpleNameSyntax methodNameSyntax,
            InvocationExpressionSyntax invocation)
        {
            if (methodNameSyntax is GenericNameSyntax genericName && genericName.TypeArgumentList.Arguments.Count > 0)
            {
                var candidate = genericName.TypeArgumentList.Arguments[0].ToString();
                if (!string.IsNullOrWhiteSpace(candidate))
                {
                    return candidate;
                }
            }

            if (invocation.ArgumentList is not { Arguments.Count: > 0 })
            {
                return null;
            }

            var firstArgument = invocation.ArgumentList.Arguments[0].Expression;
            return firstArgument switch
            {
                TypeOfExpressionSyntax typeOfExpression => typeOfExpression.Type.ToString(),
                ObjectCreationExpressionSyntax creation => creation.Type.ToString(),
                _ => null
            };
        }

    private bool IsDomainType(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return false;
        }

        var normalized = GetTypeNameWithoutGenerics(typeName) ?? typeName;
        var simple = GetTopLevelSimpleIdentifier(normalized);

        if (IsServiceType(normalized) || IsClientType(normalized) || IsRepositoryType(normalized))
        {
            return false;
        }

        if (_entities.TryGetValue(normalized, out _) ||
            _entities.Values.Any(e => e.Fqdn.Equals(normalized, StringComparison.OrdinalIgnoreCase) ||
                                      e.Name.Equals(simple, StringComparison.OrdinalIgnoreCase)))
        {
            return true;
        }

        if (normalized.Contains(".Domain", StringComparison.OrdinalIgnoreCase) ||
            normalized.Contains(".Aggregates", StringComparison.OrdinalIgnoreCase) ||
            normalized.Contains(".DomainModel", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (simple.EndsWith("Aggregate", StringComparison.OrdinalIgnoreCase) ||
            simple.EndsWith("AggregateRoot", StringComparison.OrdinalIgnoreCase) ||
            simple.EndsWith("Entity", StringComparison.OrdinalIgnoreCase) ||
            simple.EndsWith("Manager", StringComparison.OrdinalIgnoreCase) ||
            simple.EndsWith("Coordinator", StringComparison.OrdinalIgnoreCase) ||
            simple.Contains("Domain", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }

    private static string DetermineRepositoryOperation(string methodName)
    {
        if (string.IsNullOrWhiteSpace(methodName))
        {
            return "query";
        }

        if (methodName.StartsWith("Add", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Insert", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Create", StringComparison.OrdinalIgnoreCase))
        {
            return "insert";
        }

        if (methodName.StartsWith("Update", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Set", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Save", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Commit", StringComparison.OrdinalIgnoreCase))
        {
            return "update";
        }

        if (methodName.StartsWith("Remove", StringComparison.OrdinalIgnoreCase) ||
            methodName.StartsWith("Delete", StringComparison.OrdinalIgnoreCase))
        {
            return "delete";
        }

        if (methodName.StartsWith("Upsert", StringComparison.OrdinalIgnoreCase))
        {
            return "upsert";
        }

        if (methodName.StartsWith("Write", StringComparison.OrdinalIgnoreCase))
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

        var simple = GetTopLevelSimpleIdentifier(typeName);
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
               typeName.Contains("IReadOnlyRepository", StringComparison.Ordinal) ||
               typeName.Contains("UnitOfWork", StringComparison.Ordinal);
    }

    private static bool IsProjectionInvocation(MemberAccessExpressionSyntax access)
    {
        var identifier = access.Name switch
        {
            GenericNameSyntax generic => generic.Identifier.Text,
            IdentifierNameSyntax name => name.Identifier.Text,
            _ => access.Name.ToString()
        };

        if (string.IsNullOrWhiteSpace(identifier))
        {
            return false;
        }

        return string.Equals(identifier, "ProjectTo", StringComparison.Ordinal) ||
               string.Equals(identifier, "ProjectById", StringComparison.Ordinal) ||
               string.Equals(identifier, "ProjectByIdAsync", StringComparison.Ordinal);
    }

    private bool IsServiceType(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return false;
        }

        var baseType = GetTypeNameWithoutGenerics(typeName);
        if (string.IsNullOrWhiteSpace(baseType))
        {
            return false;
        }

        if (baseType.EndsWith("Service", StringComparison.Ordinal) ||
            baseType.EndsWith("Provider", StringComparison.Ordinal) ||
            baseType.EndsWith("ContextProvider", StringComparison.Ordinal) ||
            baseType.Contains(".Services", StringComparison.Ordinal) ||
            baseType.Contains(".Providers", StringComparison.Ordinal) ||
            baseType.Contains("Service.", StringComparison.Ordinal) ||
            IsClientType(baseType))
        {
            return true;
        }

        if (baseType.EndsWith("Query", StringComparison.Ordinal) ||
            baseType.EndsWith("QueryHandler", StringComparison.Ordinal) ||
            baseType.Contains(".Queries", StringComparison.Ordinal) ||
            baseType.Contains("Queries.", StringComparison.Ordinal))
        {
            return true;
        }

        if (baseType.EndsWith("Command", StringComparison.Ordinal) ||
            baseType.EndsWith("CommandHandler", StringComparison.Ordinal) ||
            baseType.Contains(".Commands", StringComparison.Ordinal) ||
            baseType.Contains("Commands.", StringComparison.Ordinal))
        {
            return true;
        }

        return false;
    }

    private const string ClientSuffix = "Client";
    private const string ClientBaseSuffix = "ClientBase";

    private bool IsClientType(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return false;
        }

        var baseType = GetTypeNameWithoutGenerics(typeName);
        var simple = GetTopLevelSimpleIdentifier(baseType);

        if (simple.Length > 1 && simple[0] == 'I' && char.IsUpper(simple[1]))
        {
            simple = simple[1..];
        }

        if (string.IsNullOrWhiteSpace(simple))
        {
            return false;
        }

        var hasClientStem = TryGetClientStem(simple, out var simpleStem);

        if (_httpClients.ContainsKey(baseType) ||
            _httpClients.ContainsKey(simple) ||
            _httpClients.ContainsKey(typeName))
        {
            return true;
        }

        if (TryResolveHttpClient(baseType, out _) ||
            TryResolveHttpClient(simple, out _) ||
            TryResolveHttpClient(typeName, out _))
        {
            return true;
        }

        if (_clientTargetServices.ContainsKey(baseType) ||
            _clientTargetServices.ContainsKey(simple) ||
            _clientTargetServices.ContainsKey(typeName))
        {
            return true;
        }

        if (_clientTargetServices.Count > 0 && !string.IsNullOrWhiteSpace(simpleStem))
        {
            foreach (var pair in _clientTargetServices)
            {
                var pairSimple = GetTopLevelSimpleIdentifier(pair.Key);
                if (TryGetClientStem(pairSimple, out var pairStem) &&
                    !string.IsNullOrWhiteSpace(pairStem) &&
                    pairStem.Equals(simpleStem, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }

        if (hasClientStem)
        {
            return true;
        }

        return false;
    }

    private static bool TryGetClientStem(string? candidate, out string stem)
    {
        stem = string.Empty;
        if (string.IsNullOrWhiteSpace(candidate))
        {
            return false;
        }

        var simple = candidate;
        var genericIndex = simple.IndexOf('<');
        if (genericIndex >= 0)
        {
            simple = simple[..genericIndex];
        }
        if (simple.Length > 1 && simple[0] == 'I' && char.IsUpper(simple[1]))
        {
            simple = simple[1..];
        }

        if (simple.EndsWith(ClientBaseSuffix, StringComparison.OrdinalIgnoreCase))
        {
            var prefix = simple[..^ClientBaseSuffix.Length];
            if (string.IsNullOrWhiteSpace(prefix) || prefix.Equals("I", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            stem = prefix;
            return true;
        }

        if (simple.EndsWith(ClientSuffix, StringComparison.OrdinalIgnoreCase))
        {
            var prefix = simple[..^ClientSuffix.Length];
            if (string.IsNullOrWhiteSpace(prefix) || prefix.Equals("I", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            stem = prefix;
            return true;
        }

        return false;
    }

    private string? ResolveClientTargetService(string clientType)
    {
        if (string.IsNullOrWhiteSpace(clientType))
        {
            return null;
        }

        var keys = new List<string>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        void AddKey(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            if (seen.Add(value))
            {
                keys.Add(value);
            }
        }

        AddKey(clientType);
        foreach (var key in DeriveClientBindingKeys(clientType))
        {
            AddKey(key);
        }

        foreach (var key in keys)
        {
            if (_clientTargetServices.TryGetValue(key, out var mapped))
            {
                return mapped;
            }
        }

        if (TryResolveHttpClient(clientType, out var clientInfo))
        {
            AddKey(clientInfo.Fqdn);
            AddKey(clientInfo.Name);

            foreach (var key in keys)
            {
                if (_clientTargetServices.TryGetValue(key, out var mapped))
                {
                    return mapped;
                }
            }
        }

        foreach (var key in keys)
        {
            var address = TryGetHttpClientBaseAddress(key);
            if (address is not { })
            {
                continue;
            }

            var normalized = NormalizeBaseUrlKey(address.BaseUrl);
            if (!string.IsNullOrWhiteSpace(normalized) && _baseUrlServiceAliases.TryGetValue(normalized!, out var alias))
            {
                return alias;
            }
        }

        if (_clientTargetServices.Count > 0)
        {
            foreach (var key in keys)
            {
                var keySimple = GetTopLevelSimpleIdentifier(key);
                if (!TryGetClientStem(keySimple, out var keyStem) || string.IsNullOrWhiteSpace(keyStem))
                {
                    continue;
                }

                foreach (var pair in _clientTargetServices)
                {
                    var pairSimple = GetTopLevelSimpleIdentifier(pair.Key);
                    if (TryGetClientStem(pairSimple, out var pairStem) &&
                        !string.IsNullOrWhiteSpace(pairStem) &&
                        pairStem.Equals(keyStem, StringComparison.OrdinalIgnoreCase))
                    {
                        return pair.Value;
                    }
                }
            }
        }

        return null;
    }

    private static bool ShouldExpandForCqrsEfHttpMap(IInvocationOperation invocation)
    {
        if (invocation is null)
        {
            return false;
        }

        return AnalysisPredicates.IsMediatorSend(invocation) ||
               AnalysisPredicates.IsMediatorPublish(invocation) ||
               AnalysisPredicates.IsDbContextOrRepoCall(invocation) ||
               AnalysisPredicates.IsHttpClientCall(invocation) ||
               AnalysisPredicates.IsMapperMap(invocation) ||
               AnalysisPredicates.IsPipelineBehavior(invocation) ||
               AnalysisPredicates.IsValidatorCall(invocation) ||
               AnalysisPredicates.IsDomainEventPublish(invocation);
    }

    private static Dictionary<string, object> CreateClientInvocationProps(ControllerClientInvocation invocation)
    {
        var props = new Dictionary<string, object>
        {
            ["route"] = invocation.RelativePath ?? string.Empty
        };

        if (!string.IsNullOrWhiteSpace(invocation.HttpMethod))
        {
            props["verb"] = invocation.HttpMethod!;
        }

        if (!string.IsNullOrWhiteSpace(invocation.ClientMethod))
        {
            props["method"] = invocation.ClientMethod!;
        }

        if (!string.IsNullOrWhiteSpace(invocation.TargetService))
        {
            props["target_service"] = invocation.TargetService!;
        }

        return props;
    }

    private static readonly string[] KnownHttpVerbs = { "GET", "POST", "PUT", "DELETE", "PATCH", "HEAD", "OPTIONS" };

    private static string? NormalizeHttpVerb(string? methodName)
    {
        if (string.IsNullOrWhiteSpace(methodName))
        {
            return null;
        }

        var trimmed = methodName.Trim();
        if (trimmed.EndsWith("Async", StringComparison.OrdinalIgnoreCase))
        {
            trimmed = trimmed[..^5];
        }

        foreach (var verb in KnownHttpVerbs)
        {
            if (trimmed.StartsWith(verb, StringComparison.OrdinalIgnoreCase))
            {
                return verb;
            }
        }

        return null;
    }

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
                Name = GetTopLevelSimpleIdentifier(clientType),
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

    private static bool IsMeaningfulResponseType(string? responseType)
    {
        if (string.IsNullOrWhiteSpace(responseType))
        {
            return false;
        }

        return !responseType.Equals("Unit", StringComparison.OrdinalIgnoreCase) &&
               !responseType.Equals("MediatR.Unit", StringComparison.OrdinalIgnoreCase) &&
               !responseType.Equals("void", StringComparison.OrdinalIgnoreCase) &&
               !responseType.Equals("System.Void", StringComparison.OrdinalIgnoreCase);
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
        foreach (var invocation in Descendants<InvocationExpressionSyntax>(classDeclaration))
        {
            if (invocation.Expression is MemberAccessExpressionSyntax { Name.Identifier.Text: var methodName } memberAccess &&
                methodName.StartsWith("Map", StringComparison.Ordinal))
            {
                var routeLiteral = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                var route = ExtractRouteLiteral(tree, routeLiteral) ?? ResolveRouteFromExpression(routeLiteral);
                if (route is null)
                {
                    continue;
                }

                var verb = methodName.Replace("Map", string.Empty, StringComparison.Ordinal).ToUpperInvariant();
                var line = GetLineNumber(tree, invocation);
                var symbolId = $"M:{namespaceName}.{classDeclaration.Identifier.Text}.{methodName}";
                var info = new MinimalEndpointInfo(route, verb, project.AssemblyName, project.RelativeDirectory, GetRelativePath(tree.FilePath), new GraphSpan { StartLine = line, EndLine = line }, symbolId, methodName);
                ApplyMinimalEndpointAuthorization(tree, invocation, info);
                _minimalEndpoints[$"{verb}:{CanonicalizeRoute(route)}"] = info;
            }
        }
    }

    private void AnalyzeMinimalApiFromProgramFile(ProjectInfo project, SyntaxTree tree, CompilationUnitSyntax root)
    {
        foreach (var invocation in Descendants<InvocationExpressionSyntax>(root))
        {
            if (invocation.Expression is MemberAccessExpressionSyntax { Name.Identifier.Text: var methodName } memberAccess &&
                methodName.StartsWith("Map", StringComparison.Ordinal))
            {
                var routeLiteral = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                var route = ExtractRouteLiteral(tree, routeLiteral) ?? ResolveRouteFromExpression(routeLiteral);
                if (route is null)
                {
                    continue;
                }

                var verb = methodName.Replace("Map", string.Empty, StringComparison.Ordinal).ToUpperInvariant();
                var line = GetLineNumber(tree, invocation);
                var symbolId = $"M:Program.{methodName}";
                var info = new MinimalEndpointInfo(route, verb, project.AssemblyName, project.RelativeDirectory, GetRelativePath(tree.FilePath), new GraphSpan { StartLine = line, EndLine = line }, symbolId, methodName);
                ApplyMinimalEndpointAuthorization(tree, invocation, info);
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
            EnsureMinimalEndpointFactNode(endpoint);
            var id = StableId.For("endpoint.minimal_api", endpoint.Fqdn, endpoint.Assembly, endpoint.SymbolId);
            var props = new Dictionary<string, object>
            {
                ["route"] = endpoint.Route,
                ["http_method"] = endpoint.HttpMethod,
                ["verb"] = endpoint.HttpMethod,
                ["controller_display"] = endpoint.Name
            };

            // Default inference for minimal endpoints (parity with controllers) so status codes appear in flows
            // Only emit if not already provided (future explicit capture could populate)
            if (!props.ContainsKey("status_codes"))
            {
                if (string.Equals(endpoint.HttpMethod, "POST", StringComparison.Ordinal))
                {
                    props["status_codes"] = new[] { 201 };
                }
                else
                {
                    props["status_codes"] = new[] { 200 };
                }
            }

            if (endpoint.Authorizations.Count > 0)
            {
                props["authorization"] = endpoint.Authorizations
                    .Select(CreateAuthorizationProps)
                    .ToList();
            }

            if (endpoint.AllowsAnonymous)
            {
                props["allow_anonymous"] = true;
            }

            var authLabel = BuildAuthLabel(endpoint.AllowsAnonymous, endpoint.Authorizations);
            if (!string.IsNullOrWhiteSpace(authLabel))
            {
                props["auth"] = authLabel!;
            }

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
                Props = props
            };
        }
    }

    private RequestInfo? ResolveRequestForDispatch(string requestType, ControllerActionInfo action)
    {
        if (string.IsNullOrWhiteSpace(requestType))
        {
            return null;
        }

        var simple = GetSimpleIdentifier(requestType);
        var candidates = _requests.Values
            .Where(r => r.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (candidates.Count == 0)
        {
            return null;
        }

        if (candidates.Count == 1)
        {
            return candidates[0];
        }

        var callerRoot = GetAssemblyRoot(action.Assembly);
        if (!string.IsNullOrWhiteSpace(callerRoot))
        {
            var sameRoot = candidates
                .Where(r => string.Equals(GetAssemblyRoot(r.Assembly), callerRoot, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (sameRoot.Count == 1)
            {
                return sameRoot[0];
            }

            if (sameRoot.Count > 1)
            {
                candidates = sameRoot;
            }
        }

        var namespaceHint = GetTypeNamespace(requestType);
        if (!string.IsNullOrWhiteSpace(namespaceHint))
        {
            var scored = candidates
                .Select(r => new { Item = r, Score = LongestCommonPrefixLength(namespaceHint, r.Fqdn) })
                .OrderByDescending(x => x.Score)
                .ThenBy(x => x.Item.Fqdn, StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (scored.Count > 0 && scored[0].Score > 0)
            {
                var secondScore = scored.ElementAtOrDefault(1)?.Score ?? int.MinValue;
                if (scored.Count == 1 || scored[0].Score > secondScore)
                {
                    return scored[0].Item;
                }
            }
        }

        var nonTest = candidates
            .Where(r => !r.Assembly.Contains(".Tests", StringComparison.OrdinalIgnoreCase))
            .ToList();
        if (nonTest.Count == 1)
        {
            return nonTest[0];
        }

        return candidates
            .OrderBy(r => r.Fqdn, StringComparer.OrdinalIgnoreCase)
            .FirstOrDefault();
    }
}
