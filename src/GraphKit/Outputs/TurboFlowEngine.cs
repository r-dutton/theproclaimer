using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GraphKit.Graph;
using GraphKit.Workspace;

namespace GraphKit.Outputs;

/// <summary>
/// Turbo engine: SCC + DAG + bitset reachability.
/// Depth-limited runs fall back to legacy to preserve identical semantics.
/// </summary>
public sealed class TurboFlowEngine : IFlowEngine
{
    public sealed record FlowNarrative(string DisplayName, IReadOnlyList<GraphNode> Actions, string Text);

    public IReadOnlyList<FlowNarrative> BuildNarratives(
        GraphDocument document,
        FlowWorkspaceIndex? workspace,
        Func<GraphNode, bool>? controllerPredicate = null,
        int? maxDepth = null)
    {
        controllerPredicate ??= static _ => true;

        if (maxDepth.HasValue)
        {
            var legacy = FlowBuilder.BuildFlows(document, controllerPredicate, workspace, maxDepth);
            if (string.IsNullOrWhiteSpace(legacy))
            {
                return Array.Empty<FlowNarrative>();
            }

            return new[] { new FlowNarrative(string.Empty, Array.Empty<GraphNode>(), legacy) };
        }

        var groups = FlowBuilder.GroupControllers(document, controllerPredicate).ToList();
        if (groups.Count == 0)
        {
            return Array.Empty<FlowNarrative>();
        }

        var gix = GraphIndex.Build(document);
        if (gix.Nodes.Length == 0)
        {
            return Array.Empty<FlowNarrative>();
        }

        var scc = SccIndex.Build(gix);
        var reachability = Bitset.BuildReach(scc);
        if (reachability.Length == 0)
        {
            return Array.Empty<FlowNarrative>();
        }

        var flowIndex = FlowGraphIndex.Build(document);
        var results = new List<FlowNarrative>(groups.Count);

        foreach (var (key, controllerActions) in groups)
        {
            var actionNodes = controllerActions.Select(a => a.Node).ToList();
            if (actionNodes.Count == 0)
            {
                continue;
            }

            var startNodeIdx = actionNodes
                .Select(a => gix.IdxById.TryGetValue(a.Id, out var idx) ? idx : -1)
                .Where(idx => idx >= 0)
                .Select(idx => scc.SccOf[idx])
                .Distinct()
                .ToArray();

            if (startNodeIdx.Length == 0)
            {
                continue;
            }

            var allowedScc = new uint[reachability[0].Length];
            foreach (var s in startNodeIdx)
            {
                if ((uint)s < reachability.Length)
                {
                    Bitset.OrInto(allowedScc, reachability[s]);
                }
            }

            var allowedIds = new HashSet<string>(StringComparer.Ordinal);
            for (int nodeIdx = 0; nodeIdx < gix.Nodes.Length; nodeIdx++)
            {
                if (Bitset.Has(allowedScc, scc.SccOf[nodeIdx]))
                {
                    allowedIds.Add(gix.Nodes[nodeIdx].Id);
                }
            }

            foreach (var action in actionNodes)
            {
                allowedIds.Add(action.Id);
            }

            var state = FlowBuilder.CreateState(flowIndex, workspace, maxDepth);
            var reachableIds = CollectReachableIds(actionNodes, state.EdgesByFrom, maxDepth);
            allowedIds.UnionWith(reachableIds);
            state.AllowedIds = allowedIds;

            var builder = new StringBuilder(2048);
            var displayName = FlowBuilder.ResolveControllerDisplayName(actionNodes[0], key);
            FlowBuilder.AppendControllerFlow(builder, state, displayName, actionNodes);
            builder.AppendLine();

            results.Add(new FlowNarrative(displayName, actionNodes, builder.ToString()));
        }

        return results;
    }

    private static HashSet<string> CollectReachableIds(
        IReadOnlyList<GraphNode> actionNodes,
        IReadOnlyDictionary<string, List<GraphEdge>> edgesByFrom,
        int? maxDepth)
    {
        var allowed = new HashSet<string>(StringComparer.Ordinal);
        var seen = new HashSet<string>(StringComparer.Ordinal);
        var queue = new Queue<(string Id, int Depth)>();

        foreach (var action in actionNodes)
        {
            if (string.IsNullOrWhiteSpace(action?.Id))
            {
                continue;
            }

            if (seen.Add(action.Id))
            {
                queue.Enqueue((action.Id, 0));
            }
        }

        while (queue.Count > 0)
        {
            var (currentId, depth) = queue.Dequeue();
            allowed.Add(currentId);

            if (maxDepth.HasValue && depth >= maxDepth.Value)
            {
                continue;
            }

            if (!edgesByFrom.TryGetValue(currentId, out var edges) || edges is null)
            {
                continue;
            }

            foreach (var edge in edges)
            {
                var to = edge.To;
                if (string.IsNullOrWhiteSpace(to))
                {
                    continue;
                }

                if (seen.Add(to))
                {
                    queue.Enqueue((to, depth + 1));
                }
            }
        }

        return allowed;
    }

    public string Build(
        GraphDocument document,
        FlowWorkspaceIndex? workspace,
        Func<GraphNode, bool>? controllerPredicate = null,
        string format = "md",
        int? maxDepth = null)
    {
        var narratives = BuildNarratives(document, workspace, controllerPredicate, maxDepth);
        if (narratives.Count == 0)
        {
            return string.Empty;
        }

        var total = narratives.Sum(n => n.Text.Length);
        var sb = new StringBuilder(total > 0 ? total : 16 * 1024);
        foreach (var narrative in narratives)
        {
            sb.Append(narrative.Text);
        }

        return sb.ToString();
    }
}
