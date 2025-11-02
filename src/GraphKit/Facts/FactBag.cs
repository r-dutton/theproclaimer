using System.Collections.Generic;
using System.Linq;

namespace GraphKit.Facts
{
    public sealed class FactBag
    {
        public IReadOnlyList<NodeFact> Nodes { get; }
        public IReadOnlyList<EdgeFact> Edges { get; }

        public FactBag(IReadOnlyList<NodeFact> nodes, IReadOnlyList<EdgeFact> edges)
        {
            Nodes = nodes;
            Edges = edges;
        }

        public FactBag WithAppended(FactBag other)
            => new FactBag(Nodes.Concat(other.Nodes).ToList(), Edges.Concat(other.Edges).ToList());
    }
}
