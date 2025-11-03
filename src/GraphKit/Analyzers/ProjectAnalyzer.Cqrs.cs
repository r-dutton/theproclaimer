using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    private void AnalyzeHandler(ProjectInfo project, SyntaxTree tree, ClassDeclarationSyntax classDeclaration, string namespaceName, IReadOnlyDictionary<string, FieldDescriptor> fieldTypes)
    {
        var handlerInterfaces = classDeclaration.BaseList?.Types
            .Select(t => t.Type)
            .OfType<GenericNameSyntax>()
            .Where(g => g.Identifier.Text is "IRequestHandler" or "IAsyncRequestHandler")
            .ToList();

        if (handlerInterfaces is null || handlerInterfaces.Count == 0)
        {
            return;
        }

        (string RequestType, string ResponseType) ExtractSignature(GenericNameSyntax handlerInterfaceSyntax)
        {
            var rawRequest = handlerInterfaceSyntax.TypeArgumentList.Arguments.FirstOrDefault()?.ToString() ?? string.Empty;
            var qualifiedRequest = string.IsNullOrWhiteSpace(rawRequest)
                ? rawRequest
                : QualifyTypeName(rawRequest, project.AssemblyName, project.RelativeDirectory);

            var rawResponse = handlerInterfaceSyntax.TypeArgumentList.Arguments.Skip(1).FirstOrDefault()?.ToString() ?? "void";
            var qualifiedResponse = string.IsNullOrWhiteSpace(rawResponse)
                ? rawResponse
                : QualifyTypeName(rawResponse, project.AssemblyName, project.RelativeDirectory);

            return (qualifiedRequest, qualifiedResponse);
        }

        var (requestType, responseType) = ExtractSignature(handlerInterfaces[0]);

        var className = classDeclaration.Identifier.Text;
        var fqdn = string.IsNullOrWhiteSpace(namespaceName) ? className : $"{namespaceName}.{className}";
        var symbolId = $"T:{fqdn}";
        var filePath = GetRelativePath(tree);
        var span = ToGraphSpan(tree, classDeclaration);

        var handlerInfo = new HandlerInfo(fqdn, project.AssemblyName, project.RelativeDirectory, filePath, span, symbolId, className, requestType, responseType);
        handlerInfo.RegisterRequestSignature(requestType, responseType);

        foreach (var additionalInterface in handlerInterfaces.Skip(1))
        {
            var (additionalRequest, additionalResponse) = ExtractSignature(additionalInterface);
            handlerInfo.RegisterRequestSignature(additionalRequest, additionalResponse);
        }

        var fieldLookup = fieldTypes.ToDictionary(pair => pair.Key.TrimStart('_'), pair => pair.Value, StringComparer.OrdinalIgnoreCase);
        var methodLookup = classDeclaration.Members
            .OfType<MethodDeclarationSyntax>()
            .Where(m => !string.IsNullOrWhiteSpace(m.Identifier.Text))
            .GroupBy(m => m.Identifier.Text, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(group => group.Key, group => group.ToList(), StringComparer.OrdinalIgnoreCase);

        foreach (var method in classDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
            if (string.IsNullOrWhiteSpace(method.Identifier.Text))
            {
                continue;
            }

            var parameterTypes = method.ParameterList.Parameters
                .Where(p => !string.IsNullOrWhiteSpace(p.Identifier.Text))
                .ToDictionary(
                    p => p.Identifier.Text,
                    p => p.Type is null ? null : QualifyTypeName(p.Type.ToString(), project.AssemblyName, project.RelativeDirectory),
                    StringComparer.OrdinalIgnoreCase);

            var localVariables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var localStringValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var local in Descendants<LocalDeclarationStatementSyntax>(method))
            {
                var declaredType = local.Declaration.Type.ToString();
                foreach (var variable in local.Declaration.Variables)
                {
                    var resolvedType = declaredType;
                    if (string.Equals(resolvedType, "var", StringComparison.OrdinalIgnoreCase) &&
                        variable.Initializer is { Value: { } initializer })
                    {
                        switch (initializer)
                        {
                            case ObjectCreationExpressionSyntax creation:
                                resolvedType = creation.Type.ToString();
                                break;
                            case InvocationExpressionSyntax invocation:
                                if (TryResolveInvocationReturnType(
                                        invocation,
                                        parameterTypes,
                                        localVariables,
                                        project.AssemblyName,
                                        project.RelativeDirectory,
                                        fieldLookup) is { } inferredType)
                                {
                                    resolvedType = inferredType;
                                }

                                break;
                        }
                    }

                    resolvedType = QualifyTypeName(resolvedType, project.AssemblyName, project.RelativeDirectory);
                    localVariables[variable.Identifier.Text] = resolvedType;

                    if (ResolveStringValue(variable.Initializer?.Value) is { } stringValue)
                    {
                        localStringValues[variable.Identifier.Text] = stringValue;
                    }
                }
            }

            foreach (var assignment in Descendants<AssignmentExpressionSyntax>(method))
            {
                if (assignment.Left is IdentifierNameSyntax left &&
                    ResolveStringValue(assignment.Right) is { } assignedValue)
                {
                    localStringValues[left.Identifier.Text] = assignedValue;
                }
            }

            foreach (var memberAccess in Descendants<MemberAccessExpressionSyntax>(method))
            {
                if (memberAccess.Expression is IdentifierNameSyntax identifier)
                {
                var fieldName = identifier.Identifier.Text.TrimStart('_');
                if (fieldLookup.TryGetValue(fieldName, out var descriptor))
                {
                    var typeName = descriptor.Type;
                    var resolvedType = ResolveImplementationType(typeName, handlerInfo.Assembly, handlerInfo.Project) ?? typeName;
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
                        var clientMethod = methodName ?? memberAccess.Name switch
                        {
                            GenericNameSyntax genericName => genericName.Identifier.Text,
                            IdentifierNameSyntax identifierName => identifierName.Identifier.Text,
                            _ => memberAccess.Name.ToString()
                        };

                        var normalizedVerb = NormalizeHttpVerb(clientMethod);
                        var httpMethod = normalizedVerb ?? clientMethod?.ToUpperInvariant() ?? string.Empty;

                        string? relativePath = null;
                        if (invocation is not null)
                        {
                            var arguments = invocation.ArgumentList.Arguments;
                            if (arguments.Count > 0)
                            {
                                relativePath = ExtractRouteLiteral(tree, arguments[0].Expression) ??
                                    ResolveRouteFromExpression(arguments[0].Expression, localStringValues);

                                if (string.IsNullOrWhiteSpace(relativePath) && arguments.Count > 1)
                                {
                                    relativePath = ExtractRouteLiteral(tree, arguments[1].Expression) ??
                                        ResolveRouteFromExpression(arguments[1].Expression, localStringValues);
                                }
                            }
                        }

                        var targetService = ResolveClientTargetService(clientType);
                        handlerInfo.HttpClientInvocations.Add(new HandlerClientInvocation(clientType, httpMethod, relativePath, line, clientMethod, targetService, null));
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
                                    IdentifierNameSyntax identifierArgument => TryResolveExpressionType(identifierArgument, parameterTypes, localVariables, project.AssemblyName, project.RelativeDirectory, fieldLookup),
                                    _ => null
                                };
                            }

                            handlerInfo.PublisherCalls.Add(new HandlerPublisherCall(typeName, methodName ?? string.Empty, line, messageType));
                            recordedUsage = true;
                        }
                        else if (IsRepositoryType(resolvedType) || IsRepositoryType(typeName))
                        {
                            var repositoryType = IsRepositoryType(resolvedType) ? resolvedType : typeName;
                            var operation = DetermineRepositoryOperation(methodName ?? string.Empty);
                            handlerInfo.RepositoryCalls.Add(new HandlerRepositoryCall(repositoryType ?? string.Empty, methodName ?? string.Empty, line, operation));
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
                                    IdentifierNameSyntax identifierArgument => TryResolveExpressionType(identifierArgument, parameterTypes, localVariables, project.AssemblyName, project.RelativeDirectory, fieldLookup),
                                    _ => null
                                };

                                if (!string.IsNullOrWhiteSpace(notificationType))
                                {
                                    handlerInfo.PublishedNotifications.Add(new HandlerNotificationPublication(notificationType!, line));
                                }
                                recordedUsage = true;
                            }
                        }

                        var normalizedServiceType = NormalizeServiceType((resolvedType ?? typeName) ?? string.Empty);
                        string? invocationMethod = methodName;
                        string? dispatchRequestType = null;
                        string? dispatchResponseType = null;
                        string? dispatchKind = null;

                        var isRequestProcessor =
                            (typeName?.Contains("RequestProcessor", StringComparison.OrdinalIgnoreCase) ?? false) ||
                            (resolvedType?.Contains("RequestProcessor", StringComparison.OrdinalIgnoreCase) ?? false);

                        if (invocation is not null &&
                            isRequestProcessor &&
                            !string.IsNullOrWhiteSpace(methodName) &&
                            (string.Equals(methodName, "Process", StringComparison.OrdinalIgnoreCase) || string.Equals(methodName, "ProcessAsync", StringComparison.OrdinalIgnoreCase)))
                        {
                            if (memberAccess.Name is GenericNameSyntax generic && generic.TypeArgumentList.Arguments.Count > 0)
                            {
                                var responseCandidate = generic.TypeArgumentList.Arguments[0].ToString();
                                if (!string.IsNullOrWhiteSpace(responseCandidate))
                                {
                                    var qualifiedResponse = QualifyTypeName(responseCandidate, project.AssemblyName, project.RelativeDirectory);
                                    dispatchResponseType = string.IsNullOrWhiteSpace(qualifiedResponse) ? responseCandidate : qualifiedResponse;
                                }
                            }

                            var argExpr = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                            if (argExpr is ObjectCreationExpressionSyntax creationExpression)
                            {
                                dispatchRequestType = QualifyTypeName(creationExpression.Type.ToString(), project.AssemblyName, project.RelativeDirectory);
                            }
                            else if (argExpr is IdentifierNameSyntax identifierArgument)
                            {
                                dispatchRequestType = TryResolveExpressionType(identifierArgument, parameterTypes, localVariables, project.AssemblyName, project.RelativeDirectory, fieldLookup);
                            }
                            else if (argExpr is MemberAccessExpressionSyntax memberAccessExpr &&
                                     memberAccessExpr.Expression is IdentifierNameSyntax memberRoot)
                            {
                                dispatchRequestType = TryResolveExpressionType(memberRoot, parameterTypes, localVariables, project.AssemblyName, project.RelativeDirectory, fieldLookup);
                            }

                            if (!string.IsNullOrWhiteSpace(dispatchRequestType))
                            {
                                var qualifiedRequest = QualifyTypeName(dispatchRequestType, project.AssemblyName, project.RelativeDirectory);
                                if (!string.IsNullOrWhiteSpace(qualifiedRequest))
                                {
                                    dispatchRequestType = qualifiedRequest;
                                }

                                dispatchKind = "requestprocessor.dispatch";

                                if (string.IsNullOrWhiteSpace(dispatchResponseType))
                                {
                                    var requestInfo = FindRequestByType(dispatchRequestType, preferredAssembly: project.AssemblyName, preferredProject: project.RelativeDirectory);
                                    if (!string.IsNullOrWhiteSpace(requestInfo?.ResponseType))
                                    {
                                        dispatchResponseType = requestInfo!.ResponseType;
                                    }

                                    if (string.IsNullOrWhiteSpace(dispatchResponseType))
                                    {
                                        var downstreamHandler = FindHandlerForRequest(dispatchRequestType);
                                        if (downstreamHandler is not null)
                                        {
                                            string? handlerResponse = null;
                                            if (downstreamHandler.RequestSignatures.FirstOrDefault(sig => sig.RequestType.Equals(dispatchRequestType, StringComparison.OrdinalIgnoreCase)) is { } matchingSignature && !string.IsNullOrWhiteSpace(matchingSignature.ResponseType))
                                            {
                                                handlerResponse = matchingSignature.ResponseType;
                                            }
                                            else if (!string.IsNullOrWhiteSpace(downstreamHandler.ResponseType))
                                            {
                                                handlerResponse = downstreamHandler.ResponseType;
                                            }

                                            if (!string.IsNullOrWhiteSpace(handlerResponse) && !IsGenericPlaceholder(handlerResponse))
                                            {
                                                dispatchResponseType = handlerResponse;
                                            }
                                        }
                                    }
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(dispatchResponseType))
                            {
                                var qualifiedResponse = QualifyTypeName(dispatchResponseType, project.AssemblyName, project.RelativeDirectory);
                                if (!string.IsNullOrWhiteSpace(qualifiedResponse))
                                {
                                    dispatchResponseType = qualifiedResponse;
                                }

                                if (string.IsNullOrWhiteSpace(dispatchResponseType) || IsGenericPlaceholder(dispatchResponseType))
                                {
                                    dispatchResponseType = null;
                                }
                            }
                        }

                        var serviceUsage = new ServiceUsage(normalizedServiceType, line, methodName, invocationMethod, dispatchRequestType, dispatchResponseType, dispatchKind);

                        if (!recordedUsage)
                        {
                            handlerInfo.ServiceUsages.Add(serviceUsage);
                        }
                        else if (!(resolvedType?.EndsWith("Repository", StringComparison.Ordinal) ?? false))
                        {
                            handlerInfo.ServiceUsages.Add(serviceUsage);
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

                var resolvedType = ResolveImplementationType(descriptor.Type, handlerInfo.Assembly, handlerInfo.Project) ?? descriptor.Type;
                if (!IsConfigurationType(resolvedType) && !IsConfigurationType(descriptor.Type))
                {
                    continue;
                }

                if (TryCaptureConfigurationIndexer(elementAccess, resolvedType ?? descriptor.Type, tree) is { } configurationUsage)
                {
                    handlerInfo.ConfigurationUsages.Add(configurationUsage);
                }
            }

            foreach (var invocation in Descendants<InvocationExpressionSyntax>(method))
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

            foreach (var invocation in Descendants<InvocationExpressionSyntax>(method))
            {
                if (!TryGetHelperMethodName(invocation.Expression, out var helperName))
                {
                    continue;
                }

                if (!methodLookup.ContainsKey(helperName))
                {
                    continue;
                }

                var helperVisited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                MergeHandlerHelperDispatches(handlerInfo, helperName, fqdn, project, tree, fieldLookup, methodLookup, helperVisited);
            }
        }

        foreach (var signature in handlerInfo.RequestSignatures)
        {
            if (string.IsNullOrWhiteSpace(signature.RequestType))
            {
                continue;
            }

            _handlersByRequestType[signature.RequestType] = handlerInfo;
        }

        AnalyzeHandlerOperations(handlerInfo);
        _handlers[fqdn] = handlerInfo;
    }

    private void AnalyzeHandlerOperations(HandlerInfo handler)
    {
        if (handler is null)
        {
            return;
        }

        if (!_projectsByAssembly.TryGetValue(handler.Assembly, out var project))
        {
            return;
        }

        if (!_analyzedHandlers.TryAdd(handler.Fqdn, 0))
        {
            return;
        }

        var typeSymbol = project.Compilation.GetTypeByMetadataName(handler.Fqdn);
        if (typeSymbol is null)
        {
            return;
        }

        var pointsTo = new FlowPointsToFacade();
        var valueContent = new FlowValueContentFacade();

        foreach (var method in typeSymbol.GetMembers().OfType<IMethodSymbol>())
        {
            if (!string.Equals(method.Name, "Handle", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(method.Name, "HandleAsync", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (method.DeclaringSyntaxReferences.Length == 0)
            {
                continue;
            }

            var syntax = method.DeclaringSyntaxReferences[0].GetSyntax();
            if (syntax is not MethodDeclarationSyntax methodSyntax)
            {
                continue;
            }

            var tree = methodSyntax.SyntaxTree;
            var model = project.GetModel(tree);
            var visitor = new CqrsOperationVisitor(this, model, handler, method.Name, pointsTo, valueContent, _facts);
            FlowAnalysisEngine.AnalyzeMethod(
                project.Compilation,
                model,
                method,
                new FlowInterproceduralConfig(4, 2),
                ShouldExpandForCqrsEfHttpMap,
                visitor);
        }
    }

    internal void EnsureHandlerAnalysis(string? requestType)
    {
        if (string.IsNullOrWhiteSpace(requestType))
        {
            return;
        }

        var handler = FindHandlerForRequest(requestType);
        if (handler is null)
        {
            return;
        }

        AnalyzeHandlerOperations(handler);
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
                Name = GetTopLevelSimpleIdentifier(guardType),
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
                _requests.Values.Any(r => r.Name.Equals(GetTopLevelSimpleIdentifier(candidate.BaseType), StringComparison.Ordinal)))
            {
                _requests[candidate.Fqdn] = new RequestInfo(
                    candidate.Fqdn,
                    candidate.Assembly,
                    candidate.Project,
                    candidate.FilePath,
                    candidate.Span,
                    candidate.SymbolId,
                    candidate.Name,
                    System.Array.Empty<string>(),
                    null);
            }
        }
    }

    private void EmitRequests()
    {
        foreach (var request in _requests.Values)
        {
            EnsureRequestFactNode(request.Fqdn, request, request.Assembly, request.Project);
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
            EnsureHandlerFactNode(handler);
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

            var signatures = handler.RequestSignatures.Count > 0
                ? handler.RequestSignatures
                : new List<RequestSignature> { new(handler.RequestType, handler.ResponseType) };

            foreach (var signature in signatures)
            {
                if (string.IsNullOrWhiteSpace(signature.RequestType))
                {
                    continue;
                }

                if (FindRequestByType(signature.RequestType, preferredAssembly: handler.Assembly, preferredProject: handler.Project) is not { } request)
                {
                    continue;
                }

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
                var targetType = ResolveImplementationType(repositoryCall.RepositoryType, handler.Assembly, handler.Project) ?? repositoryCall.RepositoryType;
                var repositoryName = GetTopLevelSimpleIdentifier(targetType);

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

            foreach (var clientGroup in handler.HttpClientInvocations
                .GroupBy(c => c.ClientType, StringComparer.OrdinalIgnoreCase))
            {
                var preferredInvocation = clientGroup
                    .OrderByDescending(c => !string.IsNullOrWhiteSpace(c.RelativePath))
                    .ThenByDescending(c => !string.IsNullOrWhiteSpace(c.HttpMethod))
                    .ThenBy(c => c.Line)
                    .First();

                var props = new Dictionary<string, object>();

                var distinctVerbs = clientGroup
                    .Select(c => c.HttpMethod)
                    .Where(v => !string.IsNullOrWhiteSpace(v))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                if (!string.IsNullOrWhiteSpace(preferredInvocation.HttpMethod))
                {
                    props["verb"] = preferredInvocation.HttpMethod!;
                }
                else if (distinctVerbs.Count == 1)
                {
                    props["verb"] = distinctVerbs[0]!;
                }

                var distinctClientMethods = clientGroup
                    .Select(c => c.ClientMethod)
                    .Where(m => !string.IsNullOrWhiteSpace(m))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                if (!string.IsNullOrWhiteSpace(preferredInvocation.ClientMethod))
                {
                    props["method"] = preferredInvocation.ClientMethod!;
                }
                else if (distinctClientMethods.Count == 1)
                {
                    props["method"] = distinctClientMethods[0]!;
                }
                else if (distinctClientMethods.Count > 1)
                {
                    props["methods"] = distinctClientMethods.Select(m => (object)m!).ToArray();
                }

                var distinctRoutes = clientGroup
                    .Select(c => c.RelativePath)
                    .Where(r => !string.IsNullOrWhiteSpace(r))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                if (!string.IsNullOrWhiteSpace(preferredInvocation.RelativePath))
                {
                    props["route"] = preferredInvocation.RelativePath!;
                }
                else if (distinctRoutes.Count > 0)
                {
                    props["route"] = distinctRoutes[0]!;
                }

                var targetService = preferredInvocation.TargetService
                    ?? clientGroup.Select(c => c.TargetService).FirstOrDefault(ts => !string.IsNullOrWhiteSpace(ts))
                    ?? ResolveClientTargetService(preferredInvocation.ClientType);

                if (!string.IsNullOrWhiteSpace(targetService))
                {
                    props["target_service"] = targetService!;
                }

                EnrichClientPropsFromHttpClient(preferredInvocation, props, handler.Assembly, handler.Project);

                var propsOrNull = props.Count > 0 ? props : null;
                var invocationLine = preferredInvocation.Line;

                if (TryResolveHttpClient(preferredInvocation.ClientType, out var clientInfo, handler.Assembly, handler.Project))
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
                            Location = new GraphLocation { File = handler.FilePath, Line = invocationLine }
                        },
                        Props = propsOrNull,
                        Evidence = CreateEvidence(handler.FilePath, invocationLine)
                    });
                }
                else
                {
                    var clientId = EnsureHttpClientNode(preferredInvocation.ClientType);
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
                            Location = new GraphLocation { File = handler.FilePath, Line = invocationLine }
                        },
                        Props = propsOrNull,
                        Evidence = CreateEvidence(handler.FilePath, invocationLine)
                    });
                }
            }

            foreach (var serviceGroup in handler.ServiceUsages
                .GroupBy(u => u.ServiceType, StringComparer.OrdinalIgnoreCase))
            {
                var primary = serviceGroup
                    .OrderBy(u => u.Line)
                    .First();

                if (!TryEnsureServiceNode(primary.ServiceType, out var serviceId, out var registration, primary.TargetType, handler.Assembly, handler.Project))
                {
                    continue;
                }

                if (IsLoggerType(primary.ServiceType) && handler.LogInvocations.Count > 0)
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
                            Location = new GraphLocation { File = handler.FilePath, Line = primary.Line }
                        },
                        Props = storageProps,
                        Evidence = CreateEvidence(handler.FilePath, primary.Line)
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
                        Location = new GraphLocation { File = handler.FilePath, Line = primary.Line }
                    },
                    Props = props,
                    Evidence = CreateEvidence(handler.FilePath, primary.Line)
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
                    var requestInfo = FindRequestByType(requestType, preferredAssembly: handler.Assembly, preferredProject: handler.Project, serviceType: usage.ServiceType);
                    if (requestInfo is null)
                    {
                        continue;
                    }

                    var requestNodeId = StableId.For("cqrs.request", requestInfo.Fqdn, requestInfo.Assembly, requestInfo.SymbolId);

                    var downstreamHandler = FindHandlerForRequest(requestType);
                    var responseType = usage.ResponseType;
                    if (string.IsNullOrWhiteSpace(responseType) && downstreamHandler is not null)
                    {
                        var matchingSignature = downstreamHandler.RequestSignatures.FirstOrDefault(sig => sig.RequestType.Equals(requestType, StringComparison.OrdinalIgnoreCase));
                        responseType = matchingSignature?.ResponseType ?? downstreamHandler.ResponseType;
                        if (!string.IsNullOrWhiteSpace(responseType) && IsGenericPlaceholder(responseType))
                        {
                            responseType = null;
                        }
                    }

                    var sendsProps = new Dictionary<string, object>
                    {
                        ["service"] = usage.ServiceType,
                        ["invocation"] = usage.InvocationMethod ?? usage.Method ?? string.Empty,
                        ["request_type"] = requestType,
                        ["response_type"] = responseType ?? string.Empty
                    };

                    var pipelineLabels = ResolvePipelineBehaviorsForRequest(requestType);
                    if (pipelineLabels.Count > 0)
                    {
                        sendsProps["pipeline_behaviors"] = string.Join(", ", pipelineLabels);
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
                            Location = new GraphLocation { File = handler.FilePath, Line = usage.Line }
                        },
                        Props = sendsProps,
                        Evidence = CreateEvidence(handler.FilePath, usage.Line)
                    });

                    if (downstreamHandler is not null)
                    {
                        var downstreamHandlerId = StableId.For("cqrs.handler", downstreamHandler.Fqdn, downstreamHandler.Assembly, downstreamHandler.SymbolId);
                        _edges.Add(new GraphEdge
                        {
                            From = requestNodeId,
                            To = downstreamHandlerId,
                            Kind = "handled_by",
                            Source = "synthetic",
                            Confidence = 0.85,
                            Transform = new GraphTransform
                            {
                                Type = usage.DispatchKind!,
                                Location = new GraphLocation { File = handler.FilePath, Line = usage.Line }
                            },
                            Props = new Dictionary<string, object>
                            {
                                ["request_type"] = requestType,
                                ["handler"] = downstreamHandler.Fqdn,
                                ["response_type"] = responseType ?? string.Empty
                            },
                            Evidence = CreateEvidence(handler.FilePath, usage.Line)
                        });
                    }
                }
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
    private static bool TryGetHelperMethodName(ExpressionSyntax expression, out string helperName)
    {
        helperName = string.Empty;
        switch (expression)
        {
            case IdentifierNameSyntax identifier:
                helperName = identifier.Identifier.Text;
                return true;
            case MemberAccessExpressionSyntax { Expression: ThisExpressionSyntax, Name: IdentifierNameSyntax memberName }:
                helperName = memberName.Identifier.Text;
                return true;
            default:
                return false;
        }
    }

    private void MergeHandlerHelperDispatches(
        HandlerInfo handlerInfo,
        string helperName,
        string handlerFqdn,
        ProjectInfo project,
        SyntaxTree tree,
        IReadOnlyDictionary<string, FieldDescriptor> fieldLookup,
        IReadOnlyDictionary<string, List<MethodDeclarationSyntax>> methodLookup,
        HashSet<string> visited)
    {
        if (!methodLookup.TryGetValue(helperName, out var helperCandidates))
        {
            return;
        }

        foreach (var helper in helperCandidates)
        {
            if (helper.Modifiers.Any(m => m.IsKind(SyntaxKind.PublicKeyword)))
            {
                continue;
            }

            var helperQualifiedName = $"{handlerFqdn}.{helper.Identifier.Text}";
            if (!visited.Add(helperQualifiedName))
            {
                continue;
            }

            var parameterTypes = helper.ParameterList.Parameters
                .Where(p => !string.IsNullOrWhiteSpace(p.Identifier.Text))
                .ToDictionary(
                    p => p.Identifier.Text,
                    p => p.Type is null ? null : QualifyTypeName(p.Type.ToString(), project.AssemblyName, project.RelativeDirectory),
                    StringComparer.OrdinalIgnoreCase);

            var localVariables = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var local in Descendants<LocalDeclarationStatementSyntax>(helper))
            {
                var declaredType = local.Declaration.Type.ToString();
                foreach (var variable in local.Declaration.Variables)
                {
                    var resolvedType = declaredType;
                    if (string.Equals(resolvedType, "var", StringComparison.OrdinalIgnoreCase) &&
                        variable.Initializer is { Value: { } initializer })
                    {
                        switch (initializer)
                        {
                            case ObjectCreationExpressionSyntax creation:
                                resolvedType = creation.Type.ToString();
                                break;
                            case InvocationExpressionSyntax invocation:
                                if (TryResolveInvocationReturnType(
                                        invocation,
                                        parameterTypes,
                                        localVariables,
                                        project.AssemblyName,
                                        project.RelativeDirectory,
                                        fieldLookup) is { } inferredType)
                                {
                                    resolvedType = inferredType;
                                }

                                break;
                        }
                    }

                    resolvedType = QualifyTypeName(resolvedType, project.AssemblyName, project.RelativeDirectory);
                    localVariables[variable.Identifier.Text] = resolvedType;
                }
            }

            foreach (var invocation in Descendants<InvocationExpressionSyntax>(helper))
            {
                if (invocation.Expression is MemberAccessExpressionSyntax accessExpression)
                {
                    var methodIdentifier = accessExpression.Name.Identifier.Text;
                    if (!string.Equals(methodIdentifier, "Process", StringComparison.OrdinalIgnoreCase) &&
                        !string.Equals(methodIdentifier, "ProcessAsync", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    var targetType = ResolveHelperInvocationTargetType(accessExpression.Expression, fieldLookup, parameterTypes, localVariables, project);
                    if (string.IsNullOrWhiteSpace(targetType) || !targetType.Contains("RequestProcessor", StringComparison.Ordinal))
                    {
                        continue;
                    }

                    var argumentExpression = invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression;
                    string? requestType = null;
                    if (argumentExpression is ObjectCreationExpressionSyntax creationArgument)
                    {
                        requestType = QualifyTypeName(creationArgument.Type.ToString(), project.AssemblyName, project.RelativeDirectory);
                    }
                    else if (argumentExpression is IdentifierNameSyntax identifierArgument)
                    {
                        var resolved = TryResolveExpressionType(identifierArgument, parameterTypes, localVariables, project.AssemblyName, project.RelativeDirectory, fieldLookup);
                        if (!string.IsNullOrWhiteSpace(resolved))
                        {
                            requestType = QualifyTypeName(resolved!, project.AssemblyName, project.RelativeDirectory);
                        }
                    }

                    if (string.IsNullOrWhiteSpace(requestType))
                    {
                        continue;
                    }

                    var normalizedServiceType = NormalizeServiceType(targetType!);
                    var line = GetLineNumber(tree, invocation);
                    handlerInfo.ServiceUsages.Add(new ServiceUsage(
                        normalizedServiceType,
                        line,
                        methodIdentifier,
                        methodIdentifier,
                        requestType,
                        null,
                        "requestprocessor.dispatch"));

                    if (TryGetHelperMethodName(accessExpression.Expression, out var nestedHelper))
                    {
                        MergeHandlerHelperDispatches(handlerInfo, nestedHelper, handlerFqdn, project, tree, fieldLookup, methodLookup, visited);
                    }
                }
                else if (TryGetHelperMethodName(invocation.Expression, out var nestedHelperName))
                {
                    MergeHandlerHelperDispatches(handlerInfo, nestedHelperName, handlerFqdn, project, tree, fieldLookup, methodLookup, visited);
                }
            }
        }
    }

    private string? ResolveHelperInvocationTargetType(
        ExpressionSyntax expression,
        IReadOnlyDictionary<string, FieldDescriptor> fieldLookup,
        IReadOnlyDictionary<string, string?> parameterTypes,
        IReadOnlyDictionary<string, string> localVariables,
        ProjectInfo project)
    {
        switch (expression)
        {
            case IdentifierNameSyntax identifier:
                var identifierText = identifier.Identifier.Text;
                if (localVariables.TryGetValue(identifierText, out var localType) && !string.IsNullOrWhiteSpace(localType))
                {
                    return localType;
                }

                var trimmed = identifierText.TrimStart('_');
                if (fieldLookup.TryGetValue(trimmed, out var descriptor))
                {
                    return descriptor.Type;
                }

                if (fieldLookup.TryGetValue(identifierText, out descriptor))
                {
                    return descriptor.Type;
                }

                if (parameterTypes.TryGetValue(identifierText, out var parameterType) && !string.IsNullOrWhiteSpace(parameterType))
                {
                    return QualifyTypeName(parameterType!, project.AssemblyName, project.RelativeDirectory);
                }

                break;
            case MemberAccessExpressionSyntax memberAccess when memberAccess.Expression is ThisExpressionSyntax && memberAccess.Name is IdentifierNameSyntax memberIdentifier:
                var memberName = memberIdentifier.Identifier.Text.TrimStart('_');
                if (fieldLookup.TryGetValue(memberName, out var memberDescriptor))
                {
                    return memberDescriptor.Type;
                }

                break;
        }

        return null;
    }

    private void EnrichClientPropsFromHttpClient(HandlerClientInvocation invocation, IDictionary<string, object> props, string? contextAssembly, string? contextProject)
    {
        if (!TryResolveHttpClient(invocation.ClientType, out var client, contextAssembly, contextProject) || client.OutboundCalls.Count == 0)
        {
            return;
        }

        var candidates = client.OutboundCalls;
        if (!string.IsNullOrWhiteSpace(invocation.ClientMethod))
        {
            var methodMatches = candidates
                .Where(call => string.Equals(call.DeclaringMethod, invocation.ClientMethod, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (methodMatches.Count == 0 && invocation.ClientMethod!.EndsWith("Async", StringComparison.OrdinalIgnoreCase))
            {
                var trimmed = invocation.ClientMethod[..^5];
                methodMatches = candidates
                    .Where(call => string.Equals(call.DeclaringMethod, trimmed, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (methodMatches.Count > 0)
            {
                candidates = methodMatches;
            }
        }

        var candidatesWithRoute = candidates
            .Where(call => !string.IsNullOrWhiteSpace(call.Route))
            .ToList();

        var effectiveCandidates = candidatesWithRoute.Count > 0 ? candidatesWithRoute : candidates;

        if (effectiveCandidates.Count == 0)
        {
            return;
        }

        string? resolvedRoute = null;
        if (candidatesWithRoute.Count > 0)
        {
            var distinctRoutes = candidatesWithRoute
                .Select(call => call.Route)
                .Where(route => !string.IsNullOrWhiteSpace(route))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (distinctRoutes.Count == 1)
            {
                resolvedRoute = distinctRoutes[0];
            }
        }

        if (!string.IsNullOrWhiteSpace(resolvedRoute) &&
            (!props.TryGetValue("route", out var existingRoute) || string.IsNullOrWhiteSpace(existingRoute?.ToString())))
        {
            props["route"] = resolvedRoute!;
        }

        var preferredCall = resolvedRoute is null
            ? effectiveCandidates.OrderBy(call => call.Line).First()
            : effectiveCandidates
                .Where(call => string.Equals(call.Route, resolvedRoute, StringComparison.OrdinalIgnoreCase))
                .OrderBy(call => call.Line)
                .FirstOrDefault() ?? effectiveCandidates.OrderBy(call => call.Line).First();

        if (!props.ContainsKey("verb") || string.IsNullOrWhiteSpace(props["verb"]?.ToString()))
        {
            if (!string.IsNullOrWhiteSpace(preferredCall.HttpMethod))
            {
                props["verb"] = preferredCall.HttpMethod!;
            }
        }

        if (!props.ContainsKey("query_params") && preferredCall.QueryParameters is { Count: > 0 })
        {
            props["query_params"] = preferredCall.QueryParameters
                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                .Select(name => $"{name}={{*}}")
                .ToArray();
        }
    }

    private bool TryResolveHttpClient(string clientType, [NotNullWhen(true)] out HttpClientInfo? client, string? preferredAssembly = null, string? preferredProject = null)
    {
        client = null;
        if (string.IsNullOrWhiteSpace(clientType))
        {
            return false;
        }

        var preferredAssemblyRoot = GetAssemblyRoot(preferredAssembly);
        var clientNamespaceRoot = GetNamespaceRoot(clientType);

        var candidates = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var simpleCandidates = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        void RecordCandidate(string? candidate)
        {
            if (string.IsNullOrWhiteSpace(candidate))
            {
                return;
            }

            var trimmed = TrimGlobalAlias(candidate.Trim());
            if (!candidates.Add(trimmed))
            {
                return;
            }

            var simple = GetTopLevelSimpleIdentifier(trimmed);
            if (!string.IsNullOrWhiteSpace(simple))
            {
                simpleCandidates.Add(simple);

                if (simple.Length > 1 && simple[0] == 'I' && char.IsUpper(simple[1]))
                {
                    var withoutInterface = simple[1..];
                    simpleCandidates.Add(withoutInterface);

                    var namespacePart = GetTypeNamespace(trimmed);
                    if (!string.IsNullOrWhiteSpace(namespacePart))
                    {
                        simpleCandidates.Add($"{namespacePart}.{withoutInterface}");
                    }
                }
            }
        }

        void RecordWithQualifiers(string? candidate)
        {
            if (string.IsNullOrWhiteSpace(candidate))
            {
                return;
            }

            RecordCandidate(candidate);

            var qualified = QualifyTypeName(candidate);
            if (!string.IsNullOrWhiteSpace(qualified))
            {
                RecordCandidate(qualified);
            }
        }

        RecordWithQualifiers(clientType);

        var resolvedType = ResolveImplementationType(clientType, preferredAssembly, preferredProject);
        if (!string.IsNullOrWhiteSpace(resolvedType))
        {
            RecordWithQualifiers(resolvedType);
        }

        var qualifiedOriginal = QualifyTypeName(clientType);
        if (!string.IsNullOrWhiteSpace(qualifiedOriginal))
        {
            RecordWithQualifiers(qualifiedOriginal);

            var resolvedQualified = ResolveImplementationType(qualifiedOriginal, preferredAssembly, preferredProject);
            if (!string.IsNullOrWhiteSpace(resolvedQualified))
            {
                RecordWithQualifiers(resolvedQualified);
            }
        }

        var simpleNamespaceMap = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
        foreach (var candidate in candidates)
        {
            if (string.IsNullOrWhiteSpace(candidate))
            {
                continue;
            }

            var root = GetNamespaceRoot(candidate);
            if (string.IsNullOrWhiteSpace(root))
            {
                continue;
            }

            var simple = GetTopLevelSimpleIdentifier(candidate);
            if (string.IsNullOrWhiteSpace(simple))
            {
                continue;
            }

            if (!simpleNamespaceMap.TryGetValue(simple, out var roots))
            {
                roots = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                simpleNamespaceMap[simple] = roots;
            }
            roots.Add(root);

            if (simple.Length > 1 && simple[0] == 'I' && char.IsUpper(simple[1]))
            {
                var trimmed = simple[1..];
                if (!string.IsNullOrWhiteSpace(trimmed))
                {
                    if (!simpleNamespaceMap.TryGetValue(trimmed, out var trimmedRoots))
                    {
                        trimmedRoots = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        simpleNamespaceMap[trimmed] = trimmedRoots;
                    }

                    trimmedRoots.Add(root);
                }
            }
        }

        var matches = new List<(HttpClientInfo Client, string Source, bool Exact)>();
        var seenMatches = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        void AddMatch(HttpClientInfo info, string source, bool exact)
        {
            if (seenMatches.Add(info.Fqdn))
            {
                matches.Add((info, source, exact));
            }
        }

        foreach (var candidate in candidates)
        {
            if (_httpClients.TryGetValue(candidate, out var resolved))
            {
                AddMatch(resolved, candidate, true);
            }
        }

        foreach (var simple in simpleCandidates)
        {
            if (_httpClients.TryGetValue(simple, out var resolved))
            {
                AddMatch(resolved, simple, false);
            }
        }

        foreach (var candidate in candidates)
        {
            var simple = GetTopLevelSimpleIdentifier(candidate);
            var matched = _httpClients.Values.Where(c =>
                    c.Fqdn.Equals(candidate, StringComparison.OrdinalIgnoreCase) ||
                    c.Name.Equals(candidate, StringComparison.OrdinalIgnoreCase) ||
                    (!string.IsNullOrWhiteSpace(simple) &&
                     (c.Fqdn.Equals(simple, StringComparison.OrdinalIgnoreCase) ||
                      c.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))))
                .ToList();

            foreach (var match in matched)
            {
                AddMatch(match, candidate, false);
            }
        }

        foreach (var simple in simpleCandidates)
        {
            var matched = _httpClients.Values.Where(c =>
                    c.Fqdn.Equals(simple, StringComparison.OrdinalIgnoreCase) ||
                    c.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var match in matched)
            {
                AddMatch(match, simple, false);
            }
        }

        foreach (var (simple, roots) in simpleNamespaceMap)
        {
            foreach (var info in _httpClients.Values)
            {
                if (!EndsWithHttpClient(info))
                {
                    continue;
                }

                var infoSimple = GetTopLevelSimpleIdentifier(info.Fqdn);
                if (!infoSimple.StartsWith(simple, StringComparison.OrdinalIgnoreCase) &&
                    !info.Name.StartsWith(simple, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var infoRoot = GetNamespaceRoot(info.Fqdn);
                if (roots.Count > 0 && (string.IsNullOrWhiteSpace(infoRoot) || !roots.Contains(infoRoot)))
                {
                    continue;
                }

                AddMatch(info, simple, false);
            }
        }

        if (!string.IsNullOrWhiteSpace(preferredAssemblyRoot))
        {
            var preferredMatches = matches
                .Where(match => string.Equals(GetAssemblyRoot(match.Client.Assembly), preferredAssemblyRoot, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (preferredMatches.Count > 0)
            {
                matches = preferredMatches;
            }
        }

        if (!string.IsNullOrWhiteSpace(preferredProject))
        {
            var projectMatches = matches
                .Where(match => !string.IsNullOrWhiteSpace(match.Client.Project) &&
                                 string.Equals(match.Client.Project, preferredProject, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (projectMatches.Count > 0)
            {
                matches = projectMatches;
            }
        }

        if (matches.Count == 0)
        {
            client = null;
            return false;
        }

        static bool EndsWithHttpClient(HttpClientInfo info)
            => info.Name.EndsWith("HttpClient", StringComparison.OrdinalIgnoreCase) ||
               info.Fqdn.EndsWith("HttpClient", StringComparison.OrdinalIgnoreCase);

        HttpClientInfo? best = null;
        var bestScore = int.MinValue;

        foreach (var (match, source, exact) in matches)
        {
            var score = 0;

            if (exact)
            {
                score += 500;
            }

            if (string.Equals(match.Fqdn, clientType, StringComparison.OrdinalIgnoreCase))
            {
                score += 1_000;
            }

            if (EndsWithHttpClient(match))
            {
                score += 350;
            }

            if (!string.IsNullOrWhiteSpace(source))
            {
                score += LongestCommonPrefixLength(source, match.Fqdn);
                if (NamespaceRootMatches(match.Fqdn, source))
                {
                    score += 200;
                }
            }

            if (!string.IsNullOrWhiteSpace(preferredAssembly))
            {
                if (string.Equals(match.Assembly, preferredAssembly, StringComparison.OrdinalIgnoreCase))
                {
                    score += 600;
                }

                var matchAssemblyRoot = GetAssemblyRoot(match.Assembly);
                if (!string.IsNullOrWhiteSpace(matchAssemblyRoot) &&
                    !string.IsNullOrWhiteSpace(preferredAssemblyRoot) &&
                    string.Equals(matchAssemblyRoot, preferredAssemblyRoot, StringComparison.OrdinalIgnoreCase))
                {
                    score += 350;
                }
            }

            if (!string.IsNullOrWhiteSpace(preferredProject) &&
                !string.IsNullOrWhiteSpace(match.Project) &&
                string.Equals(match.Project, preferredProject, StringComparison.OrdinalIgnoreCase))
            {
                score += 500;
            }

            if (!string.IsNullOrWhiteSpace(clientNamespaceRoot))
            {
                var matchNamespaceRoot = GetNamespaceRoot(match.Fqdn);
                if (!string.IsNullOrWhiteSpace(matchNamespaceRoot) &&
                    string.Equals(matchNamespaceRoot, clientNamespaceRoot, StringComparison.OrdinalIgnoreCase))
                {
                    score += 250;
                }
            }

            var sourceSimple = GetTopLevelSimpleIdentifier(source);
            if (!string.IsNullOrWhiteSpace(sourceSimple) &&
                simpleNamespaceMap.TryGetValue(sourceSimple, out var desiredRoots) && desiredRoots.Count > 0)
            {
                var candidateRoot = GetNamespaceRoot(match.Fqdn);
                if (!string.IsNullOrWhiteSpace(candidateRoot) && desiredRoots.Contains(candidateRoot))
                {
                    score += 150;
                }
            }

            if (best is null || score > bestScore)
            {
                best = match;
                bestScore = score;
            }
        }

        client = best ?? matches[0].Client;
        return true;
    }
}
