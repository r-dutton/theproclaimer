using System.Collections.Concurrent;
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
    private readonly ConcurrentDictionary<string, HandlerInfo> _handlers = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, PipelineBehaviorInfo> _pipelineBehaviors = new(StringComparer.OrdinalIgnoreCase);
    private readonly ConcurrentDictionary<string, RequestProcessorInfo> _requestProcessors = new(StringComparer.OrdinalIgnoreCase);
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

    public ProjectAnalyzer(string workspaceRoot)
    {
        _workspaceRoot = Path.GetFullPath(workspaceRoot);
        LoadFlowMap();
    }

    public async Task AnalyzeProjectAsync(ProjectInfo project, CancellationToken cancellationToken)
    {
        LoadConfigurationValues(project);

        foreach (var file in project.SourceFiles)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var text = await File.ReadAllTextAsync(file, cancellationToken);
            var tree = CSharpSyntaxTree.ParseText(text, path: file);
            var root = tree.GetCompilationUnitRoot(cancellationToken);

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
        EmitPublishers();
        EmitServiceRegistrations();
        EmitHttpCalls();
        EmitBackgroundServices();

        var nodes = _nodes.Values
            .OrderBy(node => node.Id, StringComparer.Ordinal)
            .ToList();

        var edges = _edges
            .OrderBy(edge => edge.From, StringComparer.Ordinal)
            .ThenBy(edge => edge.To, StringComparer.Ordinal)
            .ThenBy(edge => edge.Kind, StringComparer.Ordinal)
            .ToList();

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
}
