using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphKit.Graph;
using GraphKit.Workspace;

using static GraphKit.Outputs.Utilities;
namespace GraphKit.Outputs;

public static partial class FlowBuilder
{
    public static readonly HashSet<string> WriteOperationKinds = new(StringComparer.OrdinalIgnoreCase)
    {
        "writes_to",
        "inserts_into",
        "updates",
        "deletes_from",
        "upserts"
    };

    public static readonly HashSet<string> ReadOperationKinds = new(StringComparer.OrdinalIgnoreCase)
    {
        "queries",
        "reads_from",
        "selects"
    };

    public static bool IsWriteOperationKind(string? operationKind)
        => !string.IsNullOrWhiteSpace(operationKind) && WriteOperationKinds.Contains(operationKind!);

    public static bool IsReadOperationKind(string? operationKind)
    {
        if (string.IsNullOrWhiteSpace(operationKind))
        {
            return true;
        }

        if (WriteOperationKinds.Contains(operationKind!))
        {
            return false;
        }

        if (ReadOperationKinds.Contains(operationKind!))
        {
            return true;
        }

        return string.Equals(operationKind, "read", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(operationKind, "query", StringComparison.OrdinalIgnoreCase);
    }

    public static string BuildFlows(GraphDocument document, Func<GraphNode, bool> controllerPredicate, FlowWorkspaceIndex? workspace = null, int? maxDepth = null)
    {
        ArgumentNullException.ThrowIfNull(document);
        controllerPredicate ??= static _ => true;

        var groups = GroupControllers(document, controllerPredicate).ToList();
        if (groups.Count == 0)
        {
            return string.Empty;
        }

        var index = FlowGraphIndex.Build(document);
        var builder = new StringBuilder(groups.Count * 512);

        foreach (var (key, controllerActions) in groups)
        {
            if (controllerActions is null || controllerActions.Count == 0)
            {
                continue;
            }

            var actionNodes = controllerActions
                .Select(static action => action.Node)
                .ToList();

            if (actionNodes.Count == 0)
            {
                continue;
            }

            var allowed = CollectReachable(actionNodes.Select(static n => n.Id), index.EdgesByFrom, maxDepth);
            var state = CreateState(index, workspace, maxDepth);
            state.AllowedIds = allowed;

            var displayName = ResolveControllerDisplayName(actionNodes[0], key);
            AppendControllerFlow(builder, state, displayName, actionNodes);
            builder.AppendLine();
        }

        return builder.ToString();
    }
    /// <summary>
    /// Breadth-first reachability from a set of start node IDs.
    /// Avoids repeated traversals across convergent flows.
    /// </summary>
    private static HashSet<string> CollectReachable(IEnumerable<string> startIds,
                                                    IReadOnlyDictionary<string, List<GraphEdge>> edgesByFrom,
                                                    int? maxDepth = null,
                                                    System.Threading.CancellationToken ct = default)
    {
        var allowed = new HashSet<string>(StringComparer.Ordinal);
        var q = new Queue<(string Id, int Depth)>();
        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (var s in startIds)
        {
            if (string.IsNullOrWhiteSpace(s)) continue;
            if (seen.Add(s)) q.Enqueue((s, 0));
        }
        while (q.Count > 0)
        {
            ct.ThrowIfCancellationRequested();
            var (id, d) = q.Dequeue();
            allowed.Add(id);
            if (maxDepth.HasValue && d >= maxDepth.Value) continue;
            if (!edgesByFrom.TryGetValue(id, out var outs) || outs is null) continue;
            foreach (var e in outs)
            {
                var to = e.To;
                if (to is null) continue;
                if (seen.Add(to)) q.Enqueue((to, d + 1));
            }
        }
        return allowed;
    }


    private static string ResolveControllerKey(GraphNode action)
    {
        var fromProps = GetNodeProp(action, "controller_type");
        if (!string.IsNullOrWhiteSpace(fromProps))
        {
            return fromProps!;
        }

        var fqdn = action.Fqdn;
        if (!string.IsNullOrWhiteSpace(fqdn))
        {
            var lastDot = fqdn!.LastIndexOf('.');
            if (lastDot > 0)
            {
                return fqdn[..lastDot];
            }
        }

        return action.Name ?? action.Id;
    }

    public static string ResolveControllerDisplayName(GraphNode action, string controllerKey)
    {
        var fromProps = GetNodeProp(action, "controller_name");
        if (!string.IsNullOrWhiteSpace(fromProps))
        {
            return fromProps!;
        }

        if (!string.IsNullOrWhiteSpace(controllerKey))
        {
            return controllerKey;
        }

        return action.Name ?? action.Id;
    }
    public static bool IsMutationVerb(string verb)
        => !string.Equals(verb, "GET", StringComparison.OrdinalIgnoreCase)
           && !string.Equals(verb, "HEAD", StringComparison.OrdinalIgnoreCase)
           && !string.Equals(verb, "OPTIONS", StringComparison.OrdinalIgnoreCase);

    // Extract core entity name from possible service / repository contract names.
    private static string ExtractEntityName(string? serviceName)
    {
        if (string.IsNullOrWhiteSpace(serviceName)) return string.Empty;
        var name = serviceName.Trim();

        // Remove interface prefix
        if (name.Length > 1 && name[0] == 'I' && char.IsUpper(name[1]))
        {
            name = name[1..];
        }

        // Strip generic part
        var genericIndex = name.IndexOf('<');
        if (genericIndex > 0)
        {
            name = name[..genericIndex];
        }

        // Remove common suffixes
        string[] suffixes = ["Repository", "Repo", "Service", "DataAccess", "DataStore", "Dal", "ControlledRepository"];
        foreach (var s in suffixes)
        {
            if (name.EndsWith(s, StringComparison.OrdinalIgnoreCase))
            {
                name = name[..^s.Length];
                break; // remove only one outermost suffix
            }
        }

        // Collapse remaining generic wrappers like RequestProcessorWrapper<Foo,Bar>
        if (name.Contains('`'))
        {
            var tickIndex = name.IndexOf('`');
            if (tickIndex > 0) name = name[..tickIndex];
        }

        return name;
    }

    // Basic pattern match: case-insensitive equality. Allows generic patterns (IRepository<Entity>) direct compare.
    private static bool MatchesPattern(string candidate, string pattern)
        => string.Equals(candidate, pattern, StringComparison.OrdinalIgnoreCase);

    // Reuse existing repository detection via Utilities.
    private static bool IsRepositoryType(GraphNode node)
        => Utilities.IsRepositoryNode(node);

    // Select a best repository implementation candidate given caller context.
    private static GraphNode SelectBestMatch(List<GraphNode> candidates, GraphNode caller)
    {
        if (candidates.Count == 1) return candidates[0];

        var sameSolution = candidates.Where(c => Utilities.IsWithinCallerSolution(caller, c)).ToList();
        if (sameSolution.Count == 1)
        {
            return sameSolution[0];
        }
        if (sameSolution.Count > 1)
        {
            candidates = sameSolution;
        }

        // Prefer same solution / assembly root then concrete file presence then outgoing edges (more behavior).
        var callerRoot = Utilities.GetAssemblyRoot(caller.Assembly);
        var sameRoot = candidates.Where(c => string.Equals(Utilities.GetAssemblyRoot(c.Assembly), callerRoot, StringComparison.OrdinalIgnoreCase)).ToList();
        if (sameRoot.Count == 1) return sameRoot[0];
        if (sameRoot.Count > 1) candidates = sameRoot;

        var withFile = candidates.Where(c => !string.IsNullOrWhiteSpace(c.FilePath) && !c.FilePath.StartsWith("external:", StringComparison.OrdinalIgnoreCase)).ToList();
        if (withFile.Count == 1) return withFile[0];
        if (withFile.Count > 1) candidates = withFile;

        // Prefer candidates that have outgoing edges (likely concrete)
        // Need access to edges; fallback to first ordered for stability if not available
        // We cannot access state here; treat count of edges by scanning global document via caller reference
        // (state not available; thus heuristic simplified)

        var ordered = candidates
            .OrderByDescending(c => !string.IsNullOrWhiteSpace(c.FilePath))
            .ThenBy(c => c.Fqdn ?? c.Name ?? c.Id, StringComparer.OrdinalIgnoreCase)
            .ToList();
        return ordered[0];
    }

    internal static bool IsSyntheticRequestProcessorDispatch(GraphEdge edge)
    {
        if (!string.Equals(edge.Kind, "sends_request", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (!string.Equals(edge.Source, "synthetic", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return string.Equals(edge.Transform?.Type, "requestprocessor.dispatch", StringComparison.OrdinalIgnoreCase);
    }

    private static string BuildSyntheticDispatchKey(GraphEdge edge)
    {
        var file = edge.Transform?.Location?.File ?? string.Empty;
        var line = edge.Transform?.Location?.Line ?? -1;
        return $"{edge.From}::{edge.To}::{file}::{line}";
    }

    internal static void MarkSyntheticDispatchRendered(FlowRenderState state, GraphEdge edge)
    {
        if (!IsSyntheticRequestProcessorDispatch(edge))
        {
            return;
        }

        var key = BuildSyntheticDispatchKey(edge);
        state.RenderedSyntheticDispatches.Add(key);
    }

    internal static bool ShouldSkipSyntheticDispatch(FlowRenderState state, GraphEdge edge)
    {
        if (!IsSyntheticRequestProcessorDispatch(edge))
        {
            return false;
        }

        var key = BuildSyntheticDispatchKey(edge);
        return state.RenderedSyntheticDispatches.Contains(key);
    }

    public static GraphNode? TryResolveSingleImplementation(FlowRenderState state, GraphNode caller, GraphNode serviceNode)
    {
        var candidates = new List<GraphNode>();
        var seen = new HashSet<string>(StringComparer.Ordinal);

        if (state.EdgesByFrom.TryGetValue(serviceNode.Id, out var edges))
        {
            foreach (var edge in edges.Where(e => e.Kind == "implemented_by"))
            {
                if (!IsMatchingImplementationEdge(serviceNode, edge)) continue;
                if (!state.NodesById.TryGetValue(edge.To, out var impl)) continue;
                if (!ShouldIncludeImplementation(caller, impl)) continue;
                if (seen.Add(impl.Id)) candidates.Add(impl);
            }
        }
        if (candidates.Count == 0)
        {
            foreach (var c in state.FindCandidateImplementations(serviceNode))
            {
                if (!ShouldIncludeImplementation(caller, c)) continue;
                if (seen.Add(c.Id)) candidates.Add(c);
            }
        }
        if (candidates.Count == 0)
        {
            foreach (var c in state.FindHeuristicImplementations(serviceNode))
            {
                if (!ShouldIncludeImplementation(caller, c)) continue;
                if (seen.Add(c.Id)) candidates.Add(c);
            }
        }
        if (candidates.Count == 0) return null;

        var callerRoot = GetAssemblyRoot(caller.Assembly);
        var sameRoot = candidates.Where(c => string.Equals(GetAssemblyRoot(c.Assembly), callerRoot, StringComparison.OrdinalIgnoreCase)).ToList();
        if (sameRoot.Count > 0) candidates = sameRoot;

        var grouped = candidates
            .GroupBy(c => c.Fqdn, StringComparer.OrdinalIgnoreCase)
            .Select(g => g.OrderByDescending(c => state.EdgesByFrom.ContainsKey(c.Id)).ThenBy(c => c.Fqdn, StringComparer.OrdinalIgnoreCase).First())
            .ToList();
        return grouped.Count == 1 ? grouped[0] : null;
    }


    private static GraphNode? TryResolveRepositoryPattern(
    FlowRenderState state,
    GraphNode caller,
    GraphNode serviceNode)
    {
        // Extract entity name from various patterns
        var entityName = ExtractEntityName(serviceNode.Name);
        if (string.IsNullOrEmpty(entityName)) return null;

        // Try multiple repository patterns
        var patterns = new[]
        {
            $"I{entityName}Repository",
            $"{entityName}Repository",
            $"I{entityName}Repo",
            $"{entityName}Repo",
            $"I{entityName}DataAccess",
            $"{entityName}DataAccess",
            $"I{entityName}Dal",
            $"{entityName}Dal",
            $"IRepository<{entityName}>",
            $"Repository<{entityName}>",
            $"IControlledRepository<{entityName}>",
            $"ControlledRepository<{entityName}>"
        };

        foreach (var pattern in patterns)
        {
            var candidates = new List<GraphNode>();
            if (state.NodesByName.TryGetValue(pattern, out var nameMatches))
            {
                candidates.AddRange(nameMatches);
            }

            if (state.NodesByFqdn.TryGetValue(pattern, out var fqdnMatches))
            {
                candidates.AddRange(fqdnMatches);
            }

            if (candidates.Count == 0 && pattern.Contains('<'))
            {
                // Attempt to match without generic adornment for interface names stored without generic metadata
                var simplePattern = pattern[..pattern.IndexOf('<')];
                if (state.NodesByName.TryGetValue(simplePattern, out var genericNameMatches))
                {
                    candidates.AddRange(genericNameMatches);
                }
            }

            if (candidates.Count == 0)
            {
                continue;
            }

            candidates = candidates
                .Where(IsRepositoryType)
                .Where(candidate => Utilities.IsWithinCallerSolution(caller, candidate))
                .DistinctBy(candidate => candidate.Id)
                .ToList();

            if (candidates.Count == 0)
            {
                continue;
            }

            return SelectBestMatch(candidates, caller);
        }

        return null;
    }


    public static GraphNode? TryResolveControlledRepository(FlowRenderState state, GraphNode caller, GraphNode serviceNode)
    {
        var name = serviceNode.Name ?? serviceNode.Fqdn ?? string.Empty;
        var genericStart = name.IndexOf('<');
        var genericEnd = name.LastIndexOf('>');
        if (genericStart < 0 || genericEnd <= genericStart + 1)
        {
            return null;
        }

        var genericArgument = name.Substring(genericStart + 1, genericEnd - genericStart - 1).Trim();
        if (string.IsNullOrWhiteSpace(genericArgument))
        {
            return null;
        }

        var simpleArg = genericArgument.Split('.').Last();
        if (string.IsNullOrWhiteSpace(simpleArg))
        {
            return null;
        }

        var candidateNames = new[]
        {
            simpleArg + "Repository",
            simpleArg + "ControlledRepository",
            "I" + simpleArg + "Repository",
            "I" + simpleArg + "ControlledRepository",
            simpleArg + "Repo",
            "I" + simpleArg + "Repo",
            simpleArg + "DataAccess",
            "I" + simpleArg + "DataAccess"
        };

        GraphNode? best = null;
        var callerRoot = GetAssemblyRoot(caller.Assembly);

        foreach (var candidateName in candidateNames)
        {
            if (!state.NodesByName.TryGetValue(candidateName, out var matches))
            {
                continue;
            }

            var repositories = matches
                .Where(n => n.Type is "app.repository" or "repository")
                .DistinctBy(r => r.Id)
                .ToList();

            if (repositories.Count == 0)
            {
                continue;
            }

            var sameSolution = repositories
                .Where(r => IsWithinCallerSolution(caller, r))
                .ToList();

            if (sameSolution.Count > 0)
            {
                repositories = sameSolution;
            }

            if (repositories.Count == 1)
            {
                return repositories[0];
            }

            var sameRoot = repositories
                .Where(r => string.Equals(GetAssemblyRoot(r.Assembly), callerRoot, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (sameRoot.Count == 1)
            {
                return sameRoot[0];
            }

            if (sameRoot.Count > 1)
            {
                best ??= sameRoot
                    .OrderBy(r => r.Fqdn ?? r.Name, StringComparer.OrdinalIgnoreCase)
                    .First();
            }
            else
            {
                best ??= repositories
                    .OrderBy(r => r.Fqdn ?? r.Name, StringComparer.OrdinalIgnoreCase)
                    .First();
            }
        }

        return best;
    }


    public static void AppendRequestProcessorFlow(
        StringBuilder builder,
        FlowRenderState state,
        GraphNode caller,
        int indent)
    {
        // Discover all synthetic requestprocessor dispatch edges from the caller context
        if (!state.EdgesByFrom.TryGetValue(caller.Id, out var edges))
        {
            AppendIndented(builder, indent, "... (no dispatches detected)");
            return;
        }

        var dispatches = edges
            .Where(e => IsSyntheticRequestProcessorDispatch(e) && state.NodesById.TryGetValue(e.To, out _))
            .Select(e => new { Edge = e, Node = state.NodesById[e.To] })
            .ToList();

        if (dispatches.Count == 0)
        {
            AppendIndented(builder, indent, "... (no dispatches detected)");
            return;
        }

        dispatches.Sort(static (a, b) =>
        {
            var lineA = a.Edge.Transform?.Location?.Line ?? int.MaxValue;
            var lineB = b.Edge.Transform?.Location?.Line ?? int.MaxValue;
            var cmp = lineA.CompareTo(lineB);
            if (cmp != 0) return cmp;
            return string.Compare(a.Node.Name, b.Node.Name, StringComparison.OrdinalIgnoreCase);
        });

        foreach (var d in dispatches)
        {
            var requestName = d.Node.Name ?? d.Node.Fqdn ?? d.Node.Id;
            var rawResponseType = d.Edge.Props is { } props && props.TryGetValue("response_type", out var rt) ? rt?.ToString() : null;
            var simpleResponseType = string.IsNullOrWhiteSpace(rawResponseType) ? "Unit" : GetSimpleType(rawResponseType!);
            if (string.IsNullOrWhiteSpace(simpleResponseType))
            {
                simpleResponseType = "Unit";
            }

            AppendIndented(builder, indent, $"constructs RequestProcessorWrapper<{requestName},{simpleResponseType}>");
            AppendIndented(builder, indent, $"resolves IPipelineBehavior<{requestName},{simpleResponseType}> chain");
            AppendIndented(builder, indent, $"invokes IAsyncRequestHandler<{requestName},{simpleResponseType}>.Handle");

            var responsePart = string.IsNullOrWhiteSpace(rawResponseType) ? string.Empty : $" : {rawResponseType}";
            var baseLabel = $"dispatches {requestName}{responsePart}";
            AppendIndented(builder, indent, FormatLinkedCode(baseLabel, d.Edge.Transform?.Location));
            MarkSyntheticDispatchRendered(state, d.Edge);

            // Expand pipeline behaviors (processed_by edges) under the request
            if (state.EdgesByFrom.TryGetValue(d.Node.Id, out var requestEdges))
            {
                var behaviors = requestEdges.Where(e => e.Kind == "processed_by").ToList();
                foreach (var be in behaviors)
                {
                    if (!state.NodesById.TryGetValue(be.To, out var behaviorNode)) continue;
                    var stage = behaviorNode.Props is { } bProps && bProps.TryGetValue("stage", out var stageVal) ? stageVal?.ToString() : null;
                    var stageText = string.IsNullOrWhiteSpace(stage) ? string.Empty : $" [{stage}]";
                    AppendIndented(builder, indent + 1, $"processed_by {behaviorNode.Name}{stageText}");
                }
            }

            // Expand handler
            AppendCommandFlow(builder, state, d.Node, indent + 1);
        }
    }



    public static IEnumerable<(string Key, List<ControllerAction> Actions)> GroupControllers(
        GraphDocument document,
        Func<GraphNode, bool>? controllerPredicate = null)
    {
        controllerPredicate ??= static _ => true;

        var actionNodes = document.Nodes
            .Where(n => string.Equals(n.Type, "endpoint.controller", StringComparison.OrdinalIgnoreCase) && controllerPredicate(n))
            .OrderBy(n => n.Fqdn, StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (actionNodes.Count == 0)
        {
            yield break;
        }

        var groups = new Dictionary<string, List<ControllerAction>>(StringComparer.OrdinalIgnoreCase);
        foreach (var node in actionNodes)
        {
            var action = new ControllerAction(node);
            var key = ResolveControllerKey(node);
            (groups.TryGetValue(key, out var list) ? list : groups[key] = new()).Add(action);
        }

        foreach (var kv in groups.OrderBy(kv => kv.Key, StringComparer.OrdinalIgnoreCase))
        {
            yield return (kv.Key, kv.Value.OrderBy(a => a.Fqdn ?? a.Name, StringComparer.OrdinalIgnoreCase).ToList());
        }
    }

    public sealed record ControllerAction
    {
        public ControllerAction(GraphNode node)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            Id = node.Id;
            Name = node.Name;
            Fqdn = node.Fqdn;
            Route = GetNodeProp(node, "route");
            HttpMethod = GetNodeProp(node, "http_method") ?? GetNodeProp(node, "method");
        }

        public GraphNode Node { get; }
        public string Id { get; }
        public string? Name { get; }
        public string? Fqdn { get; }
        public string? Route { get; }
        public string? HttpMethod { get; }
    }

    public static FlowRenderState CreateState(GraphDocument document, FlowWorkspaceIndex? workspace, int? maxDepth)
    {
        ArgumentNullException.ThrowIfNull(document);
        var index = FlowGraphIndex.Build(document);
        return CreateState(index, workspace, maxDepth);
    }

    internal static FlowRenderState CreateState(FlowGraphIndex index, FlowWorkspaceIndex? workspace, int? maxDepth)
    {
        ArgumentNullException.ThrowIfNull(index);
        return new FlowRenderState(
            index.Document,
            index.NodesById,
            index.EdgesByFrom,
            index.NodesByFqdn,
            index.NodesByName,
            index.MapLookup,
            workspace,
            maxDepth,
            index);
    }

}
