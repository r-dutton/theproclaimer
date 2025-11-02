using System.Collections.Generic;
using GraphKit.Outputs.Abstractions;

namespace GraphKit.Outputs.FlowBuilder
{
    public static class FlowBuilderCore
    {
        public static IEnumerable<(string Id, string Type, IReadOnlyDictionary<string, object?> Props)> EnumerateNodes(IGraphProvider provider)
            => provider.Nodes();

        public static IEnumerable<(string FromId, string ToId, string Kind, IReadOnlyDictionary<string, object?> Props)> EnumerateEdges(IGraphProvider provider)
            => provider.Edges();

        public static FlowGraph BuildGraph(IGraphProvider provider)
        {
            var nodes = new List<FlowNode>();
            foreach (var (id, type, props) in EnumerateNodes(provider))
            {
                nodes.Add(new FlowNode(id, type, props));
            }

            var edges = new List<FlowEdge>();
            foreach (var (fromId, toId, kind, props) in EnumerateEdges(provider))
            {
                edges.Add(new FlowEdge(fromId, toId, kind, props));
            }

            return new FlowGraph(nodes, edges);
        }
    }
}
