using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;
using GraphKit.Outputs.Abstractions;

namespace GraphKit.Outputs.Legacy
{
    public sealed class LegacyGraphProvider : IGraphProvider
    {
        private static readonly IReadOnlyDictionary<string, object?> EmptyProps = new Dictionary<string, object?>();

        private readonly IEnumerable<GraphNode> _nodes;
        private readonly IEnumerable<GraphEdge> _edges;

        public LegacyGraphProvider(IEnumerable<GraphNode> nodes, IEnumerable<GraphEdge> edges)
        {
            _nodes = nodes;
            _edges = edges;
        }

        public IEnumerable<(string Id, string Type, IReadOnlyDictionary<string, object?> Props)> Nodes()
        {
            foreach (var n in _nodes)
            {
                var props = n.Props is null
                    ? EmptyProps
                    : n.Props.ToDictionary(static kvp => kvp.Key, static kvp => (object?)kvp.Value);
                yield return (n.Id, n.Type, props);
            }
        }

        public IEnumerable<(string FromId, string ToId, string Kind, IReadOnlyDictionary<string, object?> Props)> Edges()
        {
            foreach (var e in _edges)
            {
                var props = e.Props is null
                    ? EmptyProps
                    : e.Props.ToDictionary(static kvp => kvp.Key, static kvp => (object?)kvp.Value);
                yield return (e.From, e.To, e.Kind, props);
            }
        }
    }
}
