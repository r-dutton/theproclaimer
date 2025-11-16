using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Constants;
using GraphKit.FlowAnalysis.Dependencies;
using GraphKit.FlowAnalysis.Interprocedural;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using FlowAnalysisCore = GraphKit.FlowAnalysis.Core.FlowAnalysis;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private static readonly HashSet<string> BaseServiceInvocationNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "Request",
        "AdminRequest",
        "RequestAsync",
        "ExecuteRequest"
    };

    private static readonly string[] LikelyServiceSuffixes =
    {
        "Service",
        "Services",
        "Provider",
        "Client",
        "Repository",
        "Manager",
        "Factory",
        "Context",
        "Accessor",
        "Handler",
        "Processor",
        "Publisher",
        "Bus",
        "Store",
        "Dispatcher",
        "Cache"
    };

    private static readonly HashSet<string> KnownServiceNameHints = new(StringComparer.OrdinalIgnoreCase)
    {
        "Mediator",
        "IMediator",
        "ISender",
        "IPublisher",
        "IServiceProvider",
        "IServiceScopeFactory",
        "ILogger",
        "ILoggerFactory",
        "IMapper",
        "IMemoryCache",
        "IDistributedCache",
        "IHttpContextAccessor",
        "IOptions",
        "IOptionsSnapshot",
        "IOptionsMonitor",
        "IOptionsFactory",
        "IConfiguration",
        "IConfigurationRoot",
        "IConfigurationSection",
        "IHttpClientFactory",
        "IDbContextFactory",
        "DbContext",
        "IUnitOfWork",
        "IValidator",
        "IBackgroundJobClient"
    };

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
        var model = project.GetModel(tree);
        INamedTypeSymbol? classSymbol = null;
        try
        {
            classSymbol = model.GetDeclaredSymbol(classDeclaration) as INamedTypeSymbol;
        }
        catch (ArgumentException)
        {
            classSymbol = null;
        }

        classSymbol ??= project.Compilation.GetTypeByMetadataName(fqdn);
        var callsitePredicate = ComposeInterproceduralPredicate(ShouldExpandForCqrsEfHttpMap);
        var pointsTo = CreatePointsToFacade(callsitePredicate);
        var valueContent = CreateValueContentFacade(callsitePredicate);

        var fieldLookup = fieldTypes.ToDictionary(pair => pair.Key.TrimStart('_'), pair => pair.Value, StringComparer.OrdinalIgnoreCase);
        var baseTypeCandidates = classDeclaration.BaseList?.Types
            .Select(t => QualifyTypeName(t.Type.ToString(), project.AssemblyName, project.RelativeDirectory))
            .Where(t => !string.IsNullOrWhiteSpace(t))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList()
            ?? new List<string>();

        foreach (var method in classDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
            var serviceMethodName = method.Identifier.Text;
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
                        variable.Initializer?.Value is ObjectCreationExpressionSyntax creation)
                    {
                        resolvedType = creation.Type.ToString();
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
                if (!TryResolveFieldDescriptor(memberAccess.Expression, fieldLookup, out var descriptor, out _))
                {
                    continue;
                }

                var typeName = descriptor.Type;
                var resolvedType = ResolveImplementationType(typeName, serviceInfo.Assembly, serviceInfo.Project) ?? typeName;
                var invocation = memberAccess.Parent as InvocationExpressionSyntax;

                if (IsConfigurationType(resolvedType) || IsConfigurationType(typeName))
                {
                    if (invocation is not null &&
                        TryCaptureConfigurationUsage(memberAccess, invocation, resolvedType ?? typeName, tree, model, valueContent) is { } configurationUsage)
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
                    serviceInfo.ValidationCalls.Add(new HandlerValidationCall(
                        memberAccess.Expression.ToString(),
                        methodName ?? string.Empty,
                        line));
                    recordedUsage = true;
                }

                // HTTP wrappers and direct HttpClient calls handled via ServiceOperationVisitor (CFG).

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

                if (IsRepositoryType(resolvedType) || IsRepositoryType(typeName))
                {
                    var repositoryType = IsRepositoryType(resolvedType) ? resolvedType : typeName;
                    var operation = DetermineRepositoryOperation(methodName ?? string.Empty);
                    if (!string.IsNullOrWhiteSpace(repositoryType))
                    {
                        var entityType = ResolveRepositoryEntityType(
                            repositoryType!,
                            typeName,
                            project.AssemblyName,
                            project.RelativeDirectory,
                            memberAccess.Name,
                            invocation);

                        serviceInfo.RepositoryCalls.Add(new HandlerRepositoryCall(repositoryType!, entityType, methodName ?? string.Empty, line, operation));
                    }

                    continue;
                }

                if (typeName.Contains("IMapper", StringComparison.Ordinal) &&
                    memberAccess.Name is GenericNameSyntax mapperGeneric &&
                    mapperGeneric.Identifier.Text == "Map")
                {
                    var destination = mapperGeneric.TypeArgumentList.Arguments.LastOrDefault()?.ToString();
                    var sourceExpression = invocation?.ArgumentList.Arguments.FirstOrDefault()?.Expression?.ToString();
                    var sourceType = sourceExpression is not null && parameterTypes.TryGetValue(sourceExpression, out var resolved)
                        ? resolved
                        : null;
                    serviceInfo.MapperCalls.Add(new HandlerMapperCall(sourceType, destination, line));
                    recordedUsage = true;
                }

                var normalizedServiceType = NormalizeServiceType(resolvedType ?? typeName, project.AssemblyName, project.RelativeDirectory);
                var normalizedSimple = GetTopLevelSimpleIdentifier(normalizedServiceType);

                var shouldSkipServiceUsage = !descriptor.IsReadOnly &&
                    !IsLikelyInjectedServiceType(typeName) &&
                    !IsLikelyInjectedServiceType(resolvedType) &&
                    !IsLikelyInjectedServiceType(normalizedServiceType);
                var isSelfReference =
                    string.Equals(normalizedServiceType, serviceInfo.Fqdn, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(normalizedSimple, serviceInfo.Name, StringComparison.OrdinalIgnoreCase);

                string? dispatchRequestType = null;
                string? dispatchResponseType = null;
                string? dispatchKind = null;

                if (invocation is not null && !string.IsNullOrWhiteSpace(methodName))
                {
                    var isRequestProcessor =
                        (typeName?.Contains("RequestProcessor", StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (resolvedType?.Contains("RequestProcessor", StringComparison.OrdinalIgnoreCase) ?? false);

                    if (isRequestProcessor &&
                        (string.Equals(methodName, "Process", StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(methodName, "ProcessAsync", StringComparison.OrdinalIgnoreCase)))
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

                        if (invocation.ArgumentList.Arguments.FirstOrDefault()?.Expression is { } argumentExpression)
                        {
                            switch (argumentExpression)
                            {
                                case ObjectCreationExpressionSyntax creationExpression:
                                    dispatchRequestType = QualifyTypeName(creationExpression.Type.ToString(), project.AssemblyName, project.RelativeDirectory);
                                    break;
                                case IdentifierNameSyntax identifierArgument:
                                    dispatchRequestType = TryResolveExpressionType(identifierArgument, parameterTypes, localVariables, project.AssemblyName, project.RelativeDirectory, fieldLookup);
                                    break;
                                case MemberAccessExpressionSyntax memberAccessExpr when memberAccessExpr.Expression is IdentifierNameSyntax memberRoot:
                                    dispatchRequestType = TryResolveExpressionType(memberRoot, parameterTypes, localVariables, project.AssemblyName, project.RelativeDirectory, fieldLookup);
                                    break;
                            }
                        }

                        if (!string.IsNullOrWhiteSpace(dispatchRequestType))
                        {
                            var qualifiedRequest = QualifyTypeName(dispatchRequestType, project.AssemblyName, project.RelativeDirectory);
                            if (!string.IsNullOrWhiteSpace(qualifiedRequest))
                            {
                                dispatchRequestType = qualifiedRequest;
                            }

                            if (string.IsNullOrWhiteSpace(dispatchResponseType))
                            {
                                var requestInfo = FindRequestByType(dispatchRequestType, preferredAssembly: project.AssemblyName, preferredProject: project.RelativeDirectory, serviceType: resolvedType ?? typeName);
                                if (!string.IsNullOrWhiteSpace(requestInfo?.ResponseType) && !IsGenericPlaceholder(requestInfo.ResponseType))
                                {
                                    dispatchResponseType = requestInfo.ResponseType;
                                }
                            }

                            EnsureHandlerAnalysis(dispatchRequestType);

                            dispatchKind = "requestprocessor.dispatch";
                        }
                    }
                }

                if (!isSelfReference && (!recordedUsage || !IsRepositoryType(normalizedServiceType)))
                {
                    if (!shouldSkipServiceUsage || dispatchKind is not null)
                    {
                        serviceInfo.ServiceUsages.Add(new ServiceUsage(normalizedServiceType, line, serviceMethodName, methodName, dispatchRequestType, dispatchResponseType, dispatchKind));
                    }
                }
            }

            foreach (var elementAccess in Descendants<ElementAccessExpressionSyntax>(method))
            {
                if (!TryResolveFieldDescriptor(elementAccess.Expression, fieldLookup, out var descriptor, out _))
                {
                    continue;
                }

                var resolvedType = ResolveImplementationType(descriptor.Type, serviceInfo.Assembly, serviceInfo.Project) ?? descriptor.Type;
                if (!IsConfigurationType(resolvedType) && !IsConfigurationType(descriptor.Type))
                {
                    continue;
                }

                if (TryCaptureConfigurationIndexer(elementAccess, resolvedType ?? descriptor.Type, tree, model, valueContent) is { } configurationUsage)
                {
                    serviceInfo.ConfigurationUsages.Add(configurationUsage);
                }
            }

            // Projection mapping is handled elsewhere; HTTP wrappers now handled in ServiceOperationVisitor.

            IMethodSymbol? methodSymbol = null;
            try
            {
                methodSymbol = model.GetDeclaredSymbol(method) as IMethodSymbol;
            }
            catch (ArgumentException)
            {
                methodSymbol = null;
            }

            if (methodSymbol is null && classSymbol is not null)
            {
                var candidates = classSymbol
                    .GetMembers(method.Identifier.Text)
                    .OfType<IMethodSymbol>()
                    .Where(m => m.Parameters.Length == method.ParameterList.Parameters.Count)
                    .ToList();

                if (candidates.Count == 1)
                {
                    methodSymbol = candidates[0];
                }
            }

            if (methodSymbol is null)
            {
                continue;
            }

            TryAcquireMethodAnalysis(methodSymbol);

            var visitor = new ServiceOperationVisitor(
                this,
                model,
                project.AssemblyName,
                project.RelativeDirectory,
                method.Identifier.Text,
                pointsTo,
                valueContent,
                serviceInfo);

            var analysis = FlowAnalysisCore.GetOrCreateMethodAnalysis(
                project.Compilation,
                methodSymbol,
                InterproceduralConfiguration);
            analysis.Context.Accept(visitor);
        }

        _services[fqdn] = serviceInfo;
    }

    private void EmitServices()
    {
        foreach (var service in _services.Values)
        {
            PromoteServiceHttpClientInvocations(service);

            var id = StableId.For("app.service", service.Fqdn, service.Assembly, service.SymbolId);
            var implementationEdgeKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
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

            var tags = new List<string> { "app" };
            if (service.LogInvocations.Count > 0)
            {
                tags.Add("logging");
            }
            if (service.CacheInvocations.Count > 0)
            {
                tags.Add("cache");
            }
            if (service.FrameworkInteractions.Count > 0)
            {
                tags.Add("infra");
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
                Tags = tags.Count > 0 ? tags.ToArray() : null,
                Props = serviceProps
            };

            var serviceNode = _nodes[id];
            var handlerCache = new Dictionary<string, HandlerInfo>(StringComparer.OrdinalIgnoreCase);

            foreach (var repositoryCall in service.RepositoryCalls)
            {
                var targetType = ResolveImplementationType(repositoryCall.RepositoryType, service.Assembly, service.Project) ?? repositoryCall.RepositoryType;
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

                if (!TryResolveNodeReference(mapper.DestinationType, out var destination, service.Assembly, service.Project))
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

            var seenHttpClientInvocations = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var clientInvocation in service.HttpClientInvocations
                .OrderBy(c => c.Line))
            {
                if (string.IsNullOrWhiteSpace(clientInvocation.ClientType))
                {
                    continue;
                }

                var queryKey = clientInvocation.QueryParameters is { Count: > 0 }
                    ? string.Join("&", clientInvocation.QueryParameters.OrderBy(p => p, StringComparer.OrdinalIgnoreCase))
                    : string.Empty;

                var dedupeKey = string.Join("|",
                    clientInvocation.ClientType,
                    clientInvocation.HttpMethod ?? string.Empty,
                    clientInvocation.RelativePath ?? string.Empty,
                    queryKey,
                    clientInvocation.OwnerMethod ?? string.Empty,
                    clientInvocation.Line.ToString());
                if (!seenHttpClientInvocations.Add(dedupeKey))
                {
                    continue;
                }

                RecordServiceClientType(service, clientInvocation.ClientType);

                var props = new Dictionary<string, object>();
                if (!string.IsNullOrWhiteSpace(clientInvocation.HttpMethod))
                {
                    props["method"] = clientInvocation.HttpMethod!;
                }

                if (!string.IsNullOrWhiteSpace(clientInvocation.RelativePath))
                {
                    props["relative_path"] = clientInvocation.RelativePath!;
                }

                if (clientInvocation.QueryParameters is { Count: > 0 })
                {
                    props["query_params"] = clientInvocation.QueryParameters.ToArray();
                }

                if (!string.IsNullOrWhiteSpace(clientInvocation.ClientMethod))
                {
                    props["client_method"] = clientInvocation.ClientMethod!;
                }

                if (!string.IsNullOrWhiteSpace(clientInvocation.TargetService))
                {
                    props["target_service"] = clientInvocation.TargetService!;
                }

                if (!string.IsNullOrWhiteSpace(clientInvocation.OwnerMethod))
                {
                    props["owner_method"] = clientInvocation.OwnerMethod!;
                }

                var propsOrNull = props.Count > 0 ? props : null;

                if (TryResolveHttpClient(clientInvocation.ClientType, out var clientInfo, service.Assembly))
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
                if (IsFrameworkServiceType(usage.ServiceType))
                {
                    if (!TryEnsureServiceNode(usage.ServiceType, out var frameworkId, out _, usage.TargetType, service.Assembly, service.Project) ||
                        frameworkId is null)
                    {
                        continue;
                    }

                    var frameworkProps = new Dictionary<string, object>
                    {
                        ["service_type"] = usage.ServiceType
                    };

                    if (!string.IsNullOrWhiteSpace(usage.InvocationMethod))
                    {
                        frameworkProps["method"] = usage.InvocationMethod!;
                    }
                    else if (!string.IsNullOrWhiteSpace(usage.Method))
                    {
                        frameworkProps["method"] = usage.Method!;
                    }

                    if (!string.IsNullOrWhiteSpace(usage.InvocationMethod) &&
                        !string.IsNullOrWhiteSpace(usage.Method) &&
                        !string.Equals(usage.InvocationMethod, usage.Method, StringComparison.OrdinalIgnoreCase))
                    {
                        frameworkProps["invoked_method"] = usage.Method!;
                    }

                    _edges.Add(new GraphEdge
                    {
                        From = id,
                        To = frameworkId,
                        Kind = "uses_service",
                        Source = "static",
                        Confidence = 0.6,
                        Transform = new GraphTransform
                        {
                            Type = "framework.access",
                            Location = new GraphLocation { File = service.FilePath, Line = usage.Line }
                        },
                        Props = frameworkProps,
                        Evidence = CreateEvidence(service.FilePath, usage.Line)
                    });

                    continue;
                }

                if (!IsServiceUsageInScope(usage, service.Assembly, service.Project))
                {
                    continue;
                }

                if (!TryEnsureServiceNode(usage.ServiceType, out var serviceId, out var registration, usage.TargetType, service.Assembly, service.Project))
                {
                    continue;
                }

                if (serviceId is not null &&
                    usage.ImplementationTypes is { Count: > 0 })
                {
                    foreach (var implementation in usage.ImplementationTypes)
                    {
                        if (string.IsNullOrWhiteSpace(implementation) ||
                            string.Equals(implementation, usage.ServiceType, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }

                        if (!_services.TryGetValue(implementation, out var implInfo))
                        {
                            continue;
                        }

                        var implNodeId = StableId.For("app.service", implInfo.Fqdn, implInfo.Assembly, implInfo.SymbolId);
                        var edgeKey = $"{serviceId}->{implNodeId}";
                        if (!implementationEdgeKeys.Add(edgeKey))
                        {
                            continue;
                        }

                        _edges.Add(new GraphEdge
                        {
                            From = serviceId,
                            To = implNodeId,
                            Kind = EdgeKinds.Implements,
                            Source = "static",
                            Confidence = 0.7,
                            Transform = new GraphTransform
                            {
                                Type = "service.implementation",
                                Location = new GraphLocation { File = service.FilePath, Line = usage.Line }
                            },
                            Props = new Dictionary<string, object>
                            {
                                ["service_type"] = usage.ServiceType,
                                ["implementation_type"] = implementation
                            },
                            Evidence = CreateEvidence(service.FilePath, usage.Line)
                        });
                    }
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

                if (!string.IsNullOrWhiteSpace(usage.TargetType))
                {
                    props["target_type"] = usage.TargetType!;
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

                if (!string.IsNullOrWhiteSpace(usage.DispatchKind) && !string.IsNullOrWhiteSpace(usage.RequestType))
                {
                    var requestInfo = FindRequestByType(usage.RequestType, preferredAssembly: service.Assembly, preferredProject: service.Project, serviceType: usage.ServiceType) ??
                                      ResolveRequestInfo(usage.RequestType, null, usage.ServiceType);

                    if (requestInfo is not null)
                    {
                        var requestId = StableId.For("cqrs.request", requestInfo.Fqdn, requestInfo.Assembly, requestInfo.SymbolId);
                        if (_nodes.ContainsKey(requestId))
                        {
                            var responseValue = usage.ResponseType;
                            if (string.IsNullOrWhiteSpace(responseValue) && !string.IsNullOrWhiteSpace(requestInfo.ResponseType) && !IsGenericPlaceholder(requestInfo.ResponseType))
                            {
                                responseValue = requestInfo.ResponseType;
                            }

                            var requestProps = new Dictionary<string, object>
                            {
                                ["service"] = usage.ServiceType,
                                ["invocation"] = usage.InvocationMethod ?? usage.Method ?? string.Empty,
                                ["request_type"] = requestInfo.Fqdn,
                                ["response_type"] = responseValue ?? string.Empty
                            };

                            var pipelineLabels = ResolvePipelineBehaviorsForRequest(requestInfo.Fqdn);
                            if (pipelineLabels.Count > 0)
                            {
                                requestProps["pipeline_behaviors"] = string.Join(", ", pipelineLabels);
                            }

                            _edges.Add(new GraphEdge
                            {
                                From = id,
                                To = requestId,
                                Kind = "sends_request",
                                Source = "synthetic",
                                Confidence = 0.9,
                                Transform = new GraphTransform
                                {
                                    Type = usage.DispatchKind!,
                                    Location = new GraphLocation { File = service.FilePath, Line = usage.Line }
                                },
                                Props = requestProps,
                                Evidence = CreateEvidence(service.FilePath, usage.Line)
                            });

                            var handler = ResolvePreferredHandler(requestInfo.Fqdn, serviceNode, handlerCache) ??
                                          FindHandlerForRequest(requestInfo.Fqdn);
                            if (handler is not null)
                            {
                                var handlerId = StableId.For("cqrs.handler", handler.Fqdn, handler.Assembly, handler.SymbolId);
                                var handlerResponse = usage.ResponseType;
                                if (string.IsNullOrWhiteSpace(handlerResponse) && !string.IsNullOrWhiteSpace(handler.ResponseType) && !IsGenericPlaceholder(handler.ResponseType))
                                {
                                    handlerResponse = handler.ResponseType;
                                }
                                if (string.IsNullOrWhiteSpace(handlerResponse) && !string.IsNullOrWhiteSpace(requestInfo.ResponseType) && !IsGenericPlaceholder(requestInfo.ResponseType))
                                {
                                    handlerResponse = requestInfo.ResponseType;
                                }

                                var handlerProps = new Dictionary<string, object>
                                {
                                    ["request_type"] = requestInfo.Fqdn,
                                    ["handler"] = handler.Fqdn,
                                    ["response_type"] = handlerResponse ?? string.Empty
                                };

                                _handlersByRequestType[requestInfo.Fqdn] = handler;

                                _edges.Add(new GraphEdge
                                {
                                    From = requestId,
                                    To = handlerId,
                                    Kind = "handled_by",
                                    Source = "synthetic",
                                    Confidence = 0.85,
                                    Transform = new GraphTransform
                                    {
                                        Type = usage.DispatchKind!,
                                        Location = new GraphLocation { File = service.FilePath, Line = usage.Line }
                                    },
                                    Props = handlerProps,
                                    Evidence = CreateEvidence(service.FilePath, usage.Line)
                                });
                            }
                        }
                    }
                }
            }

            foreach (var grouping in service.FrameworkInteractions
                .GroupBy(i => i.ServiceType + "|" + i.Member, StringComparer.OrdinalIgnoreCase))
            {
                var interaction = grouping.OrderBy(i => i.Line).First();
                var keyParts = grouping.Key.Split('|');
                var interactionServiceType = keyParts.Length > 0 ? keyParts[0] : interaction.ServiceType;
                var interactionMember = keyParts.Length > 1 ? keyParts[1] : interaction.Member;

                if (!TryEnsureServiceNode(interactionServiceType, out var frameworkId, out _, preferredAssembly: service.Assembly, preferredProject: service.Project) ||
                    frameworkId is null)
                {
                    continue;
                }

                var frameworkProps = new Dictionary<string, object>
                {
                    ["service_type"] = interactionServiceType,
                    ["method"] = interactionMember
                };

                _edges.Add(new GraphEdge
                {
                    From = id,
                    To = frameworkId,
                    Kind = "uses_service",
                    Source = "static",
                    Confidence = 0.55,
                    Transform = new GraphTransform
                    {
                        Type = "framework.access",
                        Location = new GraphLocation { File = service.FilePath, Line = interaction.Line }
                    },
                    Props = frameworkProps,
                    Evidence = CreateEvidence(service.FilePath, interaction.Line)
                });
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

    private void PromoteServiceHttpClientInvocations(ServiceInfo service)
    {
        if (service.BaseServiceClientInvocations.Count == 0)
        {
            return;
        }

        foreach (var invocation in service.BaseServiceClientInvocations)
        {
            var clientTypes = new HashSet<string>(ResolveClientTypesForService(invocation.BaseServiceType, invocation.ServiceAssembly), StringComparer.OrdinalIgnoreCase);
            if (clientTypes.Count == 0 && invocation.CandidateClientTypes is { Count: > 0 })
            {
                foreach (var candidate in invocation.CandidateClientTypes)
                {
                    if (!string.IsNullOrWhiteSpace(candidate))
                    {
                        clientTypes.Add(candidate);
                    }
                }
            }

            if (clientTypes.Count == 0)
            {
                continue;
            }

            foreach (var clientType in clientTypes)
            {
                service.HttpClientInvocations.Add(new HandlerClientInvocation(
                    clientType,
                    invocation.HttpMethod,
                    invocation.Route,
                    invocation.Line,
                    invocation.InvokedMethod,
                    ResolveClientTargetService(clientType),
                    invocation.QueryParameters,
                    invocation.DeclaringMethod));

                RecordServiceClientType(service, clientType);
            }
        }

        service.BaseServiceClientInvocations.Clear();
    }

    // Legacy direct HttpClient invocation capture removed; handled via ServiceOperationVisitor.

    // Legacy HTTP route helpers removed; handled via ServiceOperationVisitor.

    private void RecordServiceClientType(ServiceInfo serviceInfo, string clientType)
    {
        var normalizedClientType = GetTypeNameWithoutGenerics(clientType);
        foreach (var key in EnumerateTypeKeys(serviceInfo.Fqdn).Concat(EnumerateTypeKeys(serviceInfo.Name)))
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                continue;
            }

            var compositeKey = BuildServiceHttpClientKey(serviceInfo.Assembly, key);
            var map = _serviceHttpClientTypes.GetOrAdd(compositeKey, _ => new ConcurrentDictionary<string, byte>(StringComparer.OrdinalIgnoreCase));
            map[normalizedClientType] = 0;
        }
    }

    private static string BuildServiceHttpClientKey(string? assembly, string key)
    {
        var assemblyPart = string.IsNullOrWhiteSpace(assembly) ? string.Empty : assembly.Trim();
        return $"{assemblyPart}|{key}";
    }

    private IEnumerable<string> ResolveClientTypesForService(string baseServiceType, string? serviceAssembly)
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var key in EnumerateTypeKeys(baseServiceType))
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                continue;
            }

            var compositeKey = BuildServiceHttpClientKey(serviceAssembly, key);
            if (!_serviceHttpClientTypes.TryGetValue(compositeKey, out var clients))
            {
                continue;
            }

            foreach (var client in clients.Keys)
            {
                if (seen.Add(client))
                {
                    yield return client;
                }
            }
        }
    }

    private static IEnumerable<string> EnumerateTypeKeys(string? candidate)
    {
        if (string.IsNullOrWhiteSpace(candidate))
        {
            yield break;
        }

        var queue = new Queue<string>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        queue.Enqueue(candidate.Trim());

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (!seen.Add(current))
            {
                continue;
            }

            yield return current;

            var noGenerics = GetTypeNameWithoutGenerics(current);
            if (!string.Equals(noGenerics, current, StringComparison.Ordinal))
            {
                queue.Enqueue(noGenerics);
            }

            var simple = GetTopLevelSimpleIdentifier(current);
            if (!string.IsNullOrWhiteSpace(simple) && !string.Equals(simple, current, StringComparison.Ordinal))
            {
                queue.Enqueue(simple);
            }

            if (!string.IsNullOrWhiteSpace(simple) && simple.Length > 1 && simple[0] == 'I' && char.IsUpper(simple[1]))
            {
                queue.Enqueue(simple[1..]);
            }
        }
    }

    private static bool TryResolveFieldDescriptor(
        SyntaxNode expression,
        IReadOnlyDictionary<string, FieldDescriptor> fieldLookup,
        out FieldDescriptor descriptor,
        out string fieldName)
    {
        descriptor = default!;
        fieldName = string.Empty;

        static string? ExtractName(SyntaxNode node)
        {
            return node switch
            {
                IdentifierNameSyntax identifier => identifier.Identifier.Text.TrimStart('_'),
                MemberAccessExpressionSyntax { Expression: ThisExpressionSyntax, Name: IdentifierNameSyntax name } => name.Identifier.Text.TrimStart('_'),
                MemberAccessExpressionSyntax { Expression: IdentifierNameSyntax parent, Name: IdentifierNameSyntax child }
                    when string.Equals(parent.Identifier.Text, "this", StringComparison.OrdinalIgnoreCase) => child.Identifier.Text.TrimStart('_'),
                _ => null
            };
        }

        var candidate = ExtractName(expression);
        if (string.IsNullOrWhiteSpace(candidate))
        {
            if (expression is MemberAccessExpressionSyntax nestedAccess &&
                TryResolveFieldDescriptor(nestedAccess.Expression, fieldLookup, out descriptor, out fieldName))
            {
                return true;
            }

            return false;
        }

        if (!fieldLookup.TryGetValue(candidate, out var resolvedDescriptor))
        {
            if (expression is MemberAccessExpressionSyntax nestedExpression &&
                TryResolveFieldDescriptor(nestedExpression.Expression, fieldLookup, out descriptor, out fieldName))
            {
                return true;
            }

            return false;
        }

        descriptor = resolvedDescriptor;
        fieldName = candidate!;
        return true;
    }

    private static bool IsLikelyInjectedServiceType(string? typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName))
        {
            return false;
        }

        if (IsLikelyServiceSimpleName(GetTopLevelSimpleIdentifier(typeName)))
        {
            return true;
        }

        foreach (var argument in SplitGenericArguments(typeName))
        {
            if (IsLikelyServiceSimpleName(GetTopLevelSimpleIdentifier(argument)))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsLikelyServiceSimpleName(string? candidate)
    {
        if (string.IsNullOrWhiteSpace(candidate))
        {
            return false;
        }

        if (KnownServiceNameHints.Contains(candidate))
        {
            return true;
        }

        foreach (var suffix in LikelyServiceSuffixes)
        {
            if (candidate.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        if (candidate.Length > 1 && candidate[0] == 'I' && char.IsUpper(candidate[1]))
        {
            var trimmed = candidate[1..];
            if (KnownServiceNameHints.Contains(trimmed))
            {
                return true;
            }

            foreach (var suffix in LikelyServiceSuffixes)
            {
                if (trimmed.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool TryGetInvocationName(SyntaxNode expression, out string name)
    {
        switch (expression)
        {
            case IdentifierNameSyntax identifier:
                name = identifier.Identifier.Text;
                return !string.IsNullOrWhiteSpace(name);
            case GenericNameSyntax generic:
                name = generic.Identifier.Text;
                return !string.IsNullOrWhiteSpace(name);
            case MemberAccessExpressionSyntax member:
                return TryGetInvocationName(member.Name, out name);
            default:
                name = string.Empty;
                return false;
        }
    }

    private static bool TryGetInvocationName(SimpleNameSyntax nameSyntax, out string name)
    {
        switch (nameSyntax)
        {
            case IdentifierNameSyntax identifier:
                name = identifier.Identifier.Text;
                return !string.IsNullOrWhiteSpace(name);
            case GenericNameSyntax generic:
                name = generic.Identifier.Text;
                return !string.IsNullOrWhiteSpace(name);
            default:
                name = string.Empty;
                return false;
        }
    }

    private static (string? Route, IReadOnlyCollection<string>? QueryParameters) NormalizeRouteWithQuery(string? route)
    {
        if (string.IsNullOrWhiteSpace(route))
        {
            return (null, null);
        }

        var normalized = NormalizeRoute(route);
        var queryIndex = normalized.IndexOf('?', StringComparison.Ordinal);
        if (queryIndex < 0)
        {
            return (normalized, null);
        }

        var path = normalized[..queryIndex];
        var query = normalized[(queryIndex + 1)..];
        var parameters = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var part in query.Split('&', StringSplitOptions.RemoveEmptyEntries))
        {
            var key = part.Split('=')[0];
            var normalizedKey = NormalizeQueryKeyLocal(key);
            if (string.IsNullOrWhiteSpace(normalizedKey))
            {
                normalizedKey = "{*}";
            }

            parameters.Add(normalizedKey);
        }

        var canonicalRoute = parameters.Count > 0
            ? $"{path}?{string.Join("&", parameters.OrderBy(p => p, StringComparer.OrdinalIgnoreCase).Select(p => $"{p}={{*}}"))}"
            : path;

        return (canonicalRoute, parameters.Count > 0 ? parameters.ToArray() : null);
    }

    private static string NormalizeQueryKeyLocal(string? key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return "{*}";
        }

        var trimmed = key.Trim();
        return trimmed.Replace(" ", string.Empty);
    }

}
