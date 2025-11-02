using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GraphKit.Graph;
using GraphKit.Workspace;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GraphKit.Analyzers;

public sealed partial class ProjectAnalyzer
{
    private readonly string _workspaceRoot;
    private readonly ConcurrentDictionary<string, GraphNode> _nodes = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentBag<GraphEdge> _edges = new();

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
    private readonly ConcurrentDictionary<string, BackgroundServiceInfo> _backgroundServices = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, OptionsInfo> _options = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, CacheInfo> _caches = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, DbContextInfo> _dbContexts = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _interfaceMethodReturnTypes = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, string> _stringConstants = new(StringComparer.OrdinalIgnoreCase);
    private static readonly int MaxFileParseConcurrency = Math.Max(1, Environment.ProcessorCount - 1);
    private static readonly ConditionalWeakTable<SyntaxNode, NodeDescendantCache> DescendantCache = new();

    public ProjectAnalyzer(string workspaceRoot)
    {
        _workspaceRoot = Path.GetFullPath(workspaceRoot);
        LoadFlowMap();
    }

    public int NodeCount => _nodes.Count;
    public int EdgeCount => _edges.Count;

    public async Task AnalyzeProjectAsync(ProjectInfo project, CancellationToken cancellationToken)
    {
        LoadConfigurationValues(project);

        var parsedFiles = await ParseProjectFilesAsync(project, cancellationToken);

        foreach (var (_, tree, root) in parsedFiles)
        {
            CollectStringConstants(project, tree, root, cancellationToken);
        }

        foreach (var (file, tree, root) in parsedFiles)
        {
            foreach (var member in root.Members)
            {
                ProcessMember(project, tree, member, null, cancellationToken);
            }

            AnalyzeServiceRegistrations(project, tree);
            AnalyzeHttpClientRegistrations(project, tree);

            if (Path.GetFileName(file).Equals("Program.cs", StringComparison.OrdinalIgnoreCase))
            {
                AnalyzeMinimalEndpoints(project, tree);
            }
        }
    }

    public GraphDocument BuildDocument(string analyzerVersion)
    {
        PromoteDerivedRequests();

        EmitRequests();
        EmitNotifications();
        EmitOptions();
        EmitHandlers();
        EmitPipelineBehaviors();
        EmitRequestProcessors();
        EmitNotificationHandlers();
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
        EmitBackgroundServices();


        var nodes = _nodes.Values.ToList();
        var edges = _edges.ToList();

        return new GraphDocument
        {
            Version = analyzerVersion,
            Nodes = nodes,
            Edges = edges
        };
    }


    private string GetRelativePath(string filePath)
        => Path.GetRelativePath(_workspaceRoot, filePath).Replace('\\', '/');

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
