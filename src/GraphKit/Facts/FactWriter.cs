using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace GraphKit.Facts
{
    public sealed class FactWriter
    {
        private readonly ConcurrentDictionary<string, NodeFact> _nodes = new();
        private readonly ConcurrentBag<EdgeFact> _edges = new();

        public void AddNode(NodeFact node) => _nodes.TryAdd(node.Id, node);
        public void AddEdge(EdgeFact edge) => _edges.Add(edge);

        public FactBag ToBag() => new FactBag(_nodes.Values.ToList(), _edges.ToList());
    }
}
