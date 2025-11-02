using System.Collections.Generic;
using GraphKit.Facts;
using GraphKit.Outputs.Abstractions;

namespace GraphKit.Outputs.Facts
{
    public sealed class FactsGraphProvider : IGraphProvider
    {
        private readonly FactBag _bag;
        public FactsGraphProvider(FactBag bag) => _bag = bag;

        public IEnumerable<(string Id, string Type, IReadOnlyDictionary<string, object?> Props)> Nodes()
        {
            foreach (var n in _bag.Nodes)
                yield return (n.Id, n.Type, n.Props);
        }

        public IEnumerable<(string FromId, string ToId, string Kind, IReadOnlyDictionary<string, object?> Props)> Edges()
        {
            foreach (var e in _bag.Edges)
                yield return (e.FromId, e.ToId, e.Kind, e.Props);
        }
    }
}
