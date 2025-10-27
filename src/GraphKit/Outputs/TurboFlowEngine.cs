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
    public string Build(GraphDocument document, FlowWorkspaceIndex? workspace, string format = "md", int? maxDepth = null)
    {
        if (maxDepth.HasValue)
            return FlowBuilder.BuildFlows(document, FlowFilter.Passes, workspace, maxDepth);

        var gix  = GraphIndex.Build(document);
        var scc  = SccIndex.Build(gix);
        var rech = Bitset.BuildReach(scc);

        var groups = FlowBuilder.GroupControllers(document);
        var sb = new StringBuilder(16 * 1024);

        foreach (var group in groups)
        {
            var startNodeIdx = group.Actions
                .Select(a => gix.IdxById.TryGetValue(a.Id, out var i) ? i : -1)
                .Where(i => i >= 0)
                .Select(i => scc.SccOf[i])
                .Distinct()
                .ToArray();

            if (startNodeIdx.Length == 0) continue;

            var allowedScc = new uint[rech[0].Length];
            foreach (var s in startNodeIdx) Bitset.OrInto(allowedScc, rech[s]);

            var allowedIds = new System.Collections.Generic.HashSet<string>(System.StringComparer.Ordinal);
            for (int nodeIdx = 0; nodeIdx < gix.Nodes.Length; nodeIdx++)
                if (Bitset.Has(allowedScc, scc.SccOf[nodeIdx]))
                    allowedIds.Add(gix.Nodes[nodeIdx].Id);

            var state = FlowBuilder.CreateState(document, workspace, maxDepth);
            state.AllowedIds = allowedIds;

            var displayName = FlowBuilder.ResolveControllerDisplayName(group.Actions[0], group.Key);
            FlowBuilder.AppendControllerFlow(sb, state, displayName, group.Actions);
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
