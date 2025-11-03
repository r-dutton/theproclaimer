using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using GraphKit.Facts;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private readonly string _workspaceRoot;
    private readonly FlowWorkspaceIndex _workspaceIndex;
    private readonly ConcurrentDictionary<string, GraphNode> _nodes = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentBag<GraphEdge> _edges = new();
    private readonly ConcurrentDictionary<string, ProjectInfo> _projectsByAssembly = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, byte> _analyzedHandlers = new(StringComparer.OrdinalIgnoreCase);

    private readonly ConcurrentDictionary<string, ControllerActionInfo> _controllerActions = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, MinimalEndpointInfo> _minimalEndpoints = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, RequestInfo> _requests = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, RequestInfo>> _requestsByInterfaceType = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, HandlerInfo> _handlers = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, HandlerInfo> _handlersByRequestType = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, ServiceInfo> _services = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, byte>> _serviceHttpClientTypes = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, PipelineBehaviorInfo> _pipelineBehaviors = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, RequestProcessorInfo> _requestProcessors = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, ConcurrentBag<string>> _requestPipelineRegistrations = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, ConcurrentBag<string>> _requestProcessorRegistrations = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, byte> _globalPipelineBehaviors = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, RepositoryInfo> _repositories = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, DtoInfo> _dtos = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentBag<ValidatorInfo> _validators = new();
    private readonly ConcurrentBag<MappingInfo> _mappings = new();
    private readonly ConcurrentDictionary<string, EntityInfo> _entities = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, TableInfo> _tables = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, HttpClientInfo> _httpClients = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentBag<HttpCallInfo> _httpCalls = new();
    private readonly ConcurrentDictionary<string, PublisherInfo> _publishers = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, MessageContractInfo> _messageContracts = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, PublisherProxyInfo> _publisherProxies = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, ConcurrentBag<string>> _publisherProxyContracts = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentBag<DerivedRequestCandidate> _derivedRequestCandidates = new();
    private readonly ConcurrentDictionary<string, ConcurrentBag<ServiceRegistrationInfo>> _serviceRegistrations = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, ConfigurationValue> _configurationValues = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, HttpClientBaseAddress> _httpClientBaseUrls = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, string> _clientTargetServices = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, string> _baseUrlServiceAliases = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, ConcurrentBag<ControllerActionInfo>> _controllerRoutes = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, NotificationInfo> _notifications = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, NotificationHandlerInfo> _notificationHandlers = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, DomainEventInfo> _domainEvents = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, DomainEventHandlerInfo> _domainEventHandlers = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentBag<DomainEventPublication> _domainEventPublications = new();
    private readonly ConcurrentDictionary<string, BackgroundServiceInfo> _backgroundServices = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, OptionsInfo> _options = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, CacheInfo> _caches = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, DbContextInfo> _dbContexts = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _interfaceMethodReturnTypes = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, string> _stringConstants = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, byte> _analyzedMethods = new(StringComparer.OrdinalIgnoreCase);
    private static readonly int MaxFileParseConcurrency = Math.Max(1, Environment.ProcessorCount - 1);
    private static readonly ConditionalWeakTable<SyntaxNode, NodeDescendantCache> DescendantCache = new();
    private readonly FactWriter _facts;
    private readonly AsyncLocal<ConcurrentDictionary<SyntaxTree, string>?> _treeRelativePaths = new();

    public ProjectAnalyzer(string workspaceRoot, FactWriter? facts = null)
    {
        _workspaceRoot = Path.GetFullPath(workspaceRoot);
        _workspaceIndex = FlowWorkspaceIndex.Load(_workspaceRoot);
        _facts = facts ?? new FactWriter();
        LoadFlowMap();
    }

    public int NodeCount => _nodes.Count;
    public int EdgeCount => _edges.Count;
    public FactWriter Facts => _facts;

    public Task AnalyzeProjectAsync(ProjectInfo project, CancellationToken cancellationToken)
        => AnalyzeProjectAsync(project, roslynProject: null, cancellationToken);

    public async Task AnalyzeProjectAsync(ProjectInfo project, RoslynProjectInfo? roslynProject, CancellationToken cancellationToken)
    {
        var previousTreeRelativePaths = _treeRelativePaths.Value;
        var projectTreeRelativePaths = new ConcurrentDictionary<SyntaxTree, string>();
        _treeRelativePaths.Value = projectTreeRelativePaths;

        try
        {
            LoadConfigurationValues(project);
            _projectsByAssembly[project.AssemblyName] = project;

            var documentEntries = await ParseProjectDocumentsAsync(project, roslynProject, cancellationToken).ConfigureAwait(false);

            foreach (var (_, root, _) in documentEntries)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var tree = root.SyntaxTree;
                if (tree is null)
                {
                    continue;
                }

                CollectStringConstants(project, tree, root, cancellationToken);
            }

            foreach (var (document, root, model) in documentEntries)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var tree = root.SyntaxTree;
                if (tree is null)
                {
                    continue;
                }

                _ = GetRelativePath(document, tree);

                foreach (var member in root.Members)
                {
                    ProcessMember(project, tree, member, null, cancellationToken);
                }

                AnalyzeServiceRegistrations(project, tree);
                AnalyzeHttpClientRegistrations(project, tree);

                var filePath = document.FilePath ?? tree.FilePath ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(filePath) &&
                    Path.GetFileName(filePath).Equals("Program.cs", StringComparison.OrdinalIgnoreCase))
                {
                    AnalyzeMinimalEndpoints(project, tree);
                }

                _ = model;
            }
        }
        finally
        {
            projectTreeRelativePaths.Clear();
            _treeRelativePaths.Value = previousTreeRelativePaths;
        }
    }

    public GraphDocument BuildDocument(string analyzerVersion)
    {
        PromoteDerivedRequests();

        EmitRequests();
        EmitNotifications();
        EmitDomainEvents();
        EmitOptions();
        EmitHandlers();
        EmitPipelineBehaviors();
        EmitRequestProcessors();
        EmitNotificationHandlers();
        EmitDomainEventHandlers();
        EmitRepositories();
        EmitControllers();
        EmitMinimalEndpoints();
        EmitDtos();
        EmitValidators();
        EmitDbContexts();
        EmitEntities();
        EmitMappings();
        EmitHttpClients();
        PropagateServicePublisherCalls();
        EmitPublishers();
        EmitServices();
        EmitServiceRegistrations();
        EmitHttpCalls();
        // Deferred synthetic call edges derived from uses_client edges (for cross-solution linking restoration)
        ClientLinker.EmitClientUseCallEdges(_nodes, _edges, _clientTargetServices);
        MessageLinker.EmitMessageContractLinks(_nodes, _edges, _workspaceIndex);
        EmitBackgroundServices();
        EmitDomainEventPublications();


        var nodes = _nodes.Values.ToList();
        var edges = _edges.ToList();
        edges = ResolveDeferredRequestDispatches(nodes, edges);
        AnnotateEdgeMetadata(edges);

        return new GraphDocument
        {
            Version = analyzerVersion,
            Nodes = nodes,
            Edges = edges
        };
    }

    private List<GraphEdge> ResolveDeferredRequestDispatches(IReadOnlyList<GraphNode> nodes, List<GraphEdge> edges)
    {
        if (edges.Count == 0)
        {
            return edges;
        }

        var nodesById = nodes
            .Where(n => !string.IsNullOrWhiteSpace(n.Id))
            .ToDictionary(n => n.Id, n => n, StringComparer.OrdinalIgnoreCase);

        var nodesByFqdn = nodes
            .Where(n => !string.IsNullOrWhiteSpace(n.Fqdn))
            .GroupBy(n => n.Fqdn!, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);

        var preferredHandlers = new Dictionary<string, HandlerInfo>(StringComparer.OrdinalIgnoreCase);
        var updatedEdges = new List<GraphEdge>(edges.Count);

        foreach (var edge in edges)
        {
            Dictionary<string, object>? mutableProps = null;
            var props = edge.Props;
            bool changed = false;
            var newTo = edge.To;

            if (string.Equals(edge.Kind, "sends_request", StringComparison.OrdinalIgnoreCase))
            {
                var callerNode = nodesById.TryGetValue(edge.From, out var caller) ? caller : null;
                var serviceType = TryGetString(props, "service");
                var requestType = TryGetString(props, "request_type");
                var requestInfo = ResolveRequestInfo(requestType, callerNode, serviceType);

                if (requestInfo is not null)
                {
                    if (!string.Equals(requestType, requestInfo.Fqdn, StringComparison.OrdinalIgnoreCase))
                    {
                        mutableProps ??= CloneProps(props);
                        mutableProps["request_type"] = requestInfo.Fqdn;
                        changed = true;
                    }

                    var recordedResponse = TryGetString(props, "response_type");
                    if (string.IsNullOrWhiteSpace(recordedResponse) &&
                        !string.IsNullOrWhiteSpace(requestInfo.ResponseType) &&
                        !IsGenericPlaceholder(requestInfo.ResponseType))
                    {
                        mutableProps ??= CloneProps(props);
                        mutableProps["response_type"] = requestInfo.ResponseType!;
                        changed = true;
                    }

                    requestType = requestInfo.Fqdn;

                    if (!string.IsNullOrWhiteSpace(requestType))
                    {
                        var handler = ResolvePreferredHandler(requestType, callerNode, preferredHandlers);
                        if (handler is not null)
                        {
                            _handlersByRequestType[requestType] = handler;
                        }
                    }
                }
            }
            else if (string.Equals(edge.Kind, "handled_by", StringComparison.OrdinalIgnoreCase))
            {
                var fromNode = nodesById.TryGetValue(edge.From, out var caller) ? caller : null;
                var requestType = TryGetString(props, "request_type");

                if (string.IsNullOrWhiteSpace(requestType) && fromNode is not null && !string.IsNullOrWhiteSpace(fromNode.Fqdn))
                {
                    if (IsRequestNode(fromNode))
                    {
                        requestType = fromNode.Fqdn;
                    }
                }

                RequestInfo? requestInfo = null;
                if (!string.IsNullOrWhiteSpace(requestType))
                {
                    requestInfo = ResolveRequestInfo(requestType, caller, null);
                    if (requestInfo is not null)
                    {
                        if (!string.Equals(requestType, requestInfo.Fqdn, StringComparison.OrdinalIgnoreCase))
                        {
                            mutableProps ??= CloneProps(props);
                            mutableProps["request_type"] = requestInfo.Fqdn;
                            changed = true;
                        }
                        requestType = requestInfo.Fqdn;
                    }
                }

                if (!string.IsNullOrWhiteSpace(requestType))
                {
                    var preferredHandler = ResolvePreferredHandler(requestType, caller, preferredHandlers);
                    if (preferredHandler is not null)
                    {
                        _handlersByRequestType[requestType] = preferredHandler;

                        var handlerNode = nodesByFqdn.TryGetValue(preferredHandler.Fqdn, out var node) ? node : null;
                        if (handlerNode is not null && !string.Equals(edge.To, handlerNode.Id, StringComparison.OrdinalIgnoreCase))
                        {
                            newTo = handlerNode.Id;
                            changed = true;
                        }

                        var handlerName = TryGetString(props, "handler");
                        if (!string.Equals(handlerName, preferredHandler.Fqdn, StringComparison.OrdinalIgnoreCase))
                        {
                            mutableProps ??= CloneProps(props);
                            mutableProps["handler"] = preferredHandler.Fqdn;
                            changed = true;
                        }

                        var recordedResponse = TryGetString(props, "response_type");
                        var handlerResponse = preferredHandler.ResponseType;
                        if (string.IsNullOrWhiteSpace(recordedResponse) &&
                            !string.IsNullOrWhiteSpace(handlerResponse) &&
                            !IsGenericPlaceholder(handlerResponse))
                        {
                            mutableProps ??= CloneProps(props);
                            mutableProps["response_type"] = handlerResponse!;
                            changed = true;
                        }
                    }
                }
            }

            if (changed)
            {
                var updatedEdge = new GraphEdge
                {
                    From = edge.From,
                    To = newTo,
                    Kind = edge.Kind,
                    Source = edge.Source,
                    Confidence = edge.Confidence,
                    Transform = edge.Transform,
                    Props = mutableProps ?? edge.Props,
                    Evidence = edge.Evidence
                };
                updatedEdges.Add(updatedEdge);
            }
            else
            {
                updatedEdges.Add(edge);
            }
        }

        return updatedEdges;
    }


    private static void AnnotateEdgeMetadata(IList<GraphEdge> edges)
    {
        if (edges.Count == 0)
        {
            return;
        }

        for (var index = 0; index < edges.Count; index++)
        {
            var edge = edges[index];
            var existingProps = edge.Props;
            Dictionary<string, object>? propsCopy = null;

            if (existingProps is null || !existingProps.ContainsKey("provenance"))
            {
                propsCopy ??= existingProps is null
                    ? new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
                    : new Dictionary<string, object>(existingProps, StringComparer.OrdinalIgnoreCase);
                propsCopy["provenance"] = DetermineEdgeProvenance(edge);
            }

            if (existingProps is null || !existingProps.ContainsKey("confidence"))
            {
                propsCopy ??= existingProps is null
                    ? new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
                    : new Dictionary<string, object>(existingProps, StringComparer.OrdinalIgnoreCase);
                propsCopy["confidence"] = DetermineEdgeConfidence(edge);
            }

            if (propsCopy is not null)
            {
                edges[index] = new GraphEdge
                {
                    From = edge.From,
                    To = edge.To,
                    Kind = edge.Kind,
                    Source = edge.Source,
                    Confidence = edge.Confidence,
                    Transform = edge.Transform,
                    Props = propsCopy,
                    Evidence = edge.Evidence
                };
            }
        }
    }

    private static string DetermineEdgeProvenance(GraphEdge edge)
    {
        if (edge.Props is { } props && props.TryGetValue("provenance", out var existing) && existing is string existingStr && !string.IsNullOrWhiteSpace(existingStr))
        {
            return existingStr;
        }

        if (string.Equals(edge.Source, "synthetic", StringComparison.OrdinalIgnoreCase))
        {
            return "Linker";
        }

        var transformType = edge.Transform?.Type ?? string.Empty;
        if (transformType.Contains("mediatr", StringComparison.OrdinalIgnoreCase) ||
            transformType.Contains("message", StringComparison.OrdinalIgnoreCase) ||
            transformType.Contains("httpclient", StringComparison.OrdinalIgnoreCase) ||
            transformType.Contains("pipeline", StringComparison.OrdinalIgnoreCase))
        {
            return "Interprocedural";
        }

        if (edge.Kind is "uses_client" or "uses_cache" or "uses_options" or "uses_configuration")
        {
            return "Static";
        }

        return "Static";
    }

    private static string DetermineEdgeConfidence(GraphEdge edge)
    {
        if (edge.Props is { } props && props.TryGetValue("confidence", out var existing) && existing is string existingStr && !string.IsNullOrWhiteSpace(existingStr))
        {
            return existingStr;
        }

        var magnitude = edge.Confidence;
        if (magnitude >= 0.9)
        {
            return "High";
        }

        if (magnitude >= 0.6)
        {
            return "Medium";
        }

        if (string.Equals(edge.Source, "synthetic", StringComparison.OrdinalIgnoreCase))
        {
            return "Medium";
        }

        return "Low";
    }


    private static Dictionary<string, object> CloneProps(IReadOnlyDictionary<string, object>? original)
    {
        if (original is null || original.Count == 0)
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        var clone = new Dictionary<string, object>(original.Count, StringComparer.OrdinalIgnoreCase);
        foreach (var kv in original)
        {
            clone[kv.Key] = kv.Value;
        }

        return clone;
    }

    private static string? TryGetString(IReadOnlyDictionary<string, object>? source, string key)
    {
        if (source is null)
        {
            return null;
        }

        if (!source.TryGetValue(key, out var value) || value is null)
        {
            return null;
        }

        return value switch
        {
            string s => s,
            _ => value.ToString()
        };
    }

    private bool TryAcquireMethodAnalysis(IMethodSymbol method)
    {
        if (method is null)
        {
            return false;
        }

        var key = method.GetDocumentationCommentId();
        if (string.IsNullOrWhiteSpace(key))
        {
            key = method.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }

        if (string.IsNullOrWhiteSpace(key))
        {
            key = method.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);
        }

        return _analyzedMethods.TryAdd(key, 0);
    }

    private static bool IsRequestNode(GraphNode node)
        => !string.IsNullOrWhiteSpace(node.Type) &&
           node.Type.Equals("cqrs.request", StringComparison.OrdinalIgnoreCase);

    private RequestInfo? ResolveRequestInfo(string? requestType, GraphNode? callerNode, string? serviceType)
    {
        if (string.IsNullOrWhiteSpace(requestType))
        {
            return null;
        }

        var preferredAssembly = callerNode?.Assembly;
        var preferredProject = callerNode?.Project;
        var simple = GetSimpleIdentifier(requestType);

        var requestInfo = FindRequestByType(requestType, preferredAssembly, preferredProject, serviceType);
        if (requestInfo is not null)
        {
            if (callerNode is not null && !string.IsNullOrWhiteSpace(callerNode.Assembly))
            {
                var callerRoot = GetAssemblyRoot(callerNode.Assembly);
                if (!string.IsNullOrWhiteSpace(callerRoot))
                {
                    var requestRoot = GetAssemblyRoot(requestInfo.Assembly);
                    if (!string.Equals(requestRoot, callerRoot, StringComparison.OrdinalIgnoreCase))
                    {
                        var rootMatches = _requests.Values
                            .Where(r => r.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
                            .Where(r => string.Equals(GetAssemblyRoot(r.Assembly), callerRoot, StringComparison.OrdinalIgnoreCase))
                            .ToList();

                        var preferred = SelectRequestCandidate(rootMatches, callerNode);
                        if (preferred is not null)
                        {
                            return preferred;
                        }
                    }
                }
            }

            return requestInfo;
        }

        var qualified = QualifyTypeName(requestType, preferredAssembly, preferredProject);
        if (!string.Equals(qualified, requestType, StringComparison.OrdinalIgnoreCase))
        {
            requestInfo = FindRequestByType(qualified, preferredAssembly, preferredProject, serviceType);
            if (requestInfo is not null)
            {
                return requestInfo;
            }
        }

        var candidates = _requests.Values
            .Where(r => r.Name.Equals(simple, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (candidates.Count == 0)
        {
            return null;
        }

        return SelectRequestCandidate(candidates, callerNode);
    }

    private HandlerInfo? ResolvePreferredHandler(string requestType, GraphNode? callerNode, IDictionary<string, HandlerInfo> cache)
    {
        if (string.IsNullOrWhiteSpace(requestType))
        {
            return null;
        }

        if (cache.TryGetValue(requestType, out var cached))
        {
            return cached;
        }

        var candidates = _handlers.Values
            .Where(h => h.RequestSignatures.Any(sig => sig.RequestType.Equals(requestType, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        if (candidates.Count == 0)
        {
            var simple = GetSimpleIdentifier(requestType);
            candidates = _handlers.Values
                .Where(h => h.RequestSignatures.Any(sig => GetSimpleIdentifier(sig.RequestType).Equals(simple, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        if (candidates.Count == 0)
        {
            return null;
        }

        var nonTest = candidates.Where(c => !IsTestAssembly(c.Assembly)).ToList();
        if (nonTest.Count > 0)
        {
            candidates = nonTest;
        }

        if (callerNode is not null && !string.IsNullOrWhiteSpace(callerNode.Assembly))
        {
            var callerRoot = GetAssemblyRoot(callerNode.Assembly);
            if (!string.IsNullOrWhiteSpace(callerRoot))
            {
                var sameRoot = candidates
                    .Where(c => string.Equals(GetAssemblyRoot(c.Assembly), callerRoot, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                if (sameRoot.Count == 1)
                {
                    candidates = sameRoot;
                }
                else if (sameRoot.Count > 1)
                {
                    candidates = sameRoot;
                }
            }
        }

        var ordered = candidates
            .OrderByDescending(c => !string.IsNullOrWhiteSpace(c.ResponseType) && !IsGenericPlaceholder(c.ResponseType))
            .ThenBy(c => c.Fqdn, StringComparer.OrdinalIgnoreCase)
            .ToList();

        var chosen = ordered.FirstOrDefault();
        if (chosen is not null)
        {
            cache[requestType] = chosen;
        }

        return chosen;
    }

    private static bool IsTestAssembly(string? assemblyName)
        => !string.IsNullOrWhiteSpace(assemblyName) &&
           assemblyName.Contains(".Tests", StringComparison.OrdinalIgnoreCase);

    private RequestInfo? SelectRequestCandidate(IReadOnlyList<RequestInfo> candidates, GraphNode? callerNode)
    {
        if (candidates.Count == 0)
        {
            return null;
        }

        var list = candidates.ToList();

        if (callerNode is not null && !string.IsNullOrWhiteSpace(callerNode.Assembly))
        {
            var sameAssembly = list
                .Where(r => string.Equals(r.Assembly, callerNode.Assembly, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (sameAssembly.Count == 1)
            {
                return sameAssembly[0];
            }

            if (sameAssembly.Count > 1)
            {
                list = sameAssembly;
            }

            var callerRoot = GetAssemblyRoot(callerNode.Assembly);
            if (!string.IsNullOrWhiteSpace(callerRoot))
            {
                var sameRoot = list
                    .Where(r => string.Equals(GetAssemblyRoot(r.Assembly), callerRoot, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                if (sameRoot.Count == 1)
                {
                    return sameRoot[0];
                }

                if (sameRoot.Count > 1)
                {
                    list = sameRoot;
                }
            }
        }

        var nonTest = list.Where(r => !IsTestAssembly(r.Assembly)).ToList();
        if (nonTest.Count == 1)
        {
            return nonTest[0];
        }

        if (nonTest.Count > 1)
        {
            list = nonTest;
        }

        return list
            .OrderBy(r => r.Fqdn, StringComparer.OrdinalIgnoreCase)
            .FirstOrDefault();
    }

    private string GetRelativePath(Document? document, SyntaxTree tree)
    {
        var absolutePath = document?.FilePath;
        if (string.IsNullOrWhiteSpace(absolutePath))
        {
            absolutePath = tree.FilePath;
        }

        var relative = GetRelativePath(absolutePath);
        var cache = _treeRelativePaths.Value;
        if (cache is not null)
        {
            cache[tree] = relative;
        }
        return relative;
    }

    private string GetRelativePath(SyntaxTree tree)
    {
        var cache = _treeRelativePaths.Value;
        if (cache is not null && cache.TryGetValue(tree, out var cached))
        {
            return cached;
        }

        return GetRelativePath(document: null, tree);
    }

    private string GetRelativePath(string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            return string.Empty;
        }

        return Path.GetRelativePath(_workspaceRoot, filePath).Replace('\\', '/');
    }

    private static GraphSpan ToGraphSpan(SyntaxTree tree, SyntaxNode node)
    {
        var span = tree.GetLineSpan(node.Span);
        return new GraphSpan
        {
            StartLine = span.StartLinePosition.Line + 1,
            EndLine = span.EndLinePosition.Line + 1
        };
    }

    private static int GetLineNumber(SyntaxTree tree, SyntaxNode node)
    {
        var span = tree.GetLineSpan(node.Span);
        return span.StartLinePosition.Line + 1;
    }

    private static GraphEvidence CreateEvidence(string filePath, int line)
        => new()
        {
            Files = new[]
            {
                new GraphEvidenceFile
                {
                    Path = filePath,
                    StartLine = line,
                    EndLine = line
                }
            }
        };

    private static GraphEvidence CreateEvidence(string filePath, GraphSpan span)
        => new()
        {
            Files = new[]
            {
                new GraphEvidenceFile
                {
                    Path = filePath,
                    StartLine = span.StartLine,
                    EndLine = span.EndLine
                }
            }
        };

    // Synthetic linking moved to ClientLinker.EmitClientUseCallEdges

    // Reuse existing route canonicalization logic defined elsewhere in analyzer (Controllers / Http). Provide fallback if not present.
    // Use existing CanonicalizeRoute(string route) defined in Controllers partial.

    private static IReadOnlyList<TSyntax> Descendants<TSyntax>(SyntaxNode node) where TSyntax : SyntaxNode
    {
        var cache = DescendantCache.GetValue(node, static key => new NodeDescendantCache(key));
        return cache.GetNodes<TSyntax>();
    }

    private async Task<List<(Document Document, CompilationUnitSyntax Root, SemanticModel Model)>> ParseProjectDocumentsAsync(
        ProjectInfo project,
        RoslynProjectInfo? roslynProject,
        CancellationToken cancellationToken)
    {
        if (roslynProject is not null)
        {
            return await ParseRoslynProjectDocumentsAsync(roslynProject, cancellationToken).ConfigureAwait(false);
        }

        return await ParseLegacyProjectDocumentsAsync(project, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<List<(Document Document, CompilationUnitSyntax Root, SemanticModel Model)>> ParseRoslynProjectDocumentsAsync(
        RoslynProjectInfo roslynProject,
        CancellationToken cancellationToken)
    {
        var results = new List<(Document, CompilationUnitSyntax, SemanticModel)>();

        foreach (var document in roslynProject.RoslynProject.Documents)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!document.SupportsSyntaxTree ||
                document.SourceCodeKind != SourceCodeKind.Regular)
            {
                continue;
            }

            var root = await document
                .GetSyntaxRootAsync(cancellationToken)
                .ConfigureAwait(false) as CompilationUnitSyntax;
            if (root is null)
            {
                continue;
            }

            var model = await document
                .GetSemanticModelAsync(cancellationToken)
                .ConfigureAwait(false);
            if (model is null)
            {
                continue;
            }

            results.Add((document, root, model));
        }

        return results;
    }

    private static async Task<List<(Document Document, CompilationUnitSyntax Root, SemanticModel Model)>> ParseLegacyProjectDocumentsAsync(
        ProjectInfo project,
        CancellationToken cancellationToken)
    {
        if (project.IsRoslyn)
        {
            return new List<(Document, CompilationUnitSyntax, SemanticModel)>();
        }

        var parsedFiles = await ParseProjectFilesAsync(project, cancellationToken).ConfigureAwait(false);
        if (parsedFiles.Count == 0)
        {
            return new List<(Document, CompilationUnitSyntax, SemanticModel)>();
        }

        var workspace = new AdhocWorkspace();
        var legacyProject = workspace.AddProject(project.AssemblyName, LanguageNames.CSharp);
        var results = new List<(Document, CompilationUnitSyntax, SemanticModel)>(parsedFiles.Count);

        foreach (var (filePath, tree, root) in parsedFiles)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var documentName = string.IsNullOrWhiteSpace(filePath)
                ? $"legacy_{results.Count:D4}.cs"
                : Path.GetFileName(filePath);

            var document = workspace.AddDocument(
                legacyProject.Id,
                documentName,
                tree.GetText(cancellationToken),
                filePath);

            var model = project.GetModel(tree);
            results.Add((document, root, model));
        }

        return results;
    }

    private static async Task<List<(string FilePath, SyntaxTree Tree, CompilationUnitSyntax Root)>> ParseProjectFilesAsync(ProjectInfo project, CancellationToken cancellationToken)
    {
        if (project.SourceFiles.Count == 0)
        {
            return new List<(string, SyntaxTree, CompilationUnitSyntax)>();
        }

        var results = new (string FilePath, SyntaxTree Tree, CompilationUnitSyntax Root)[project.SourceFiles.Count];
        var indexedFiles = project.SourceFiles.Select((file, index) => (File: file, Index: index));
        var parallelOptions = CreateParseParallelOptions(project.SourceFiles.Count, cancellationToken);

        await Parallel.ForEachAsync(indexedFiles, parallelOptions, async (entry, ct) =>
        {
            ct.ThrowIfCancellationRequested();

            var text = await File.ReadAllTextAsync(entry.File, ct).ConfigureAwait(false);
            var tree = CSharpSyntaxTree.ParseText(text, path: entry.File);
            var root = tree.GetCompilationUnitRoot(ct);
            results[entry.Index] = (entry.File, tree, root);
        }).ConfigureAwait(false);

        return results.ToList();
    }

    private static ParallelOptions CreateParseParallelOptions(int fileCount, CancellationToken cancellationToken)
    {
        var degree = Math.Min(MaxFileParseConcurrency, Math.Max(1, fileCount / 4));
        if (fileCount <= 4)
        {
            degree = Math.Min(2, MaxFileParseConcurrency);
        }

        return new ParallelOptions
        {
            CancellationToken = cancellationToken,
            MaxDegreeOfParallelism = degree
        };
    }

    private sealed class NodeDescendantCache
    {
        private readonly SyntaxNode[] _all;
        private Dictionary<Type, object>? _typed;

        public NodeDescendantCache(SyntaxNode node)
        {
            _all = node.DescendantNodes(descendIntoTrivia: false).ToArray();
        }

        public IReadOnlyList<TSyntax> GetNodes<TSyntax>() where TSyntax : SyntaxNode
        {
            var type = typeof(TSyntax);
            _typed ??= new Dictionary<Type, object>();
            if (_typed.TryGetValue(type, out var existing))
            {
                return (IReadOnlyList<TSyntax>)existing;
            }

            var typed = _all.OfType<TSyntax>().ToArray();
            _typed[type] = typed;
            return typed;
        }
    }
}
