using System.Collections.Generic;
using System.Linq;
using GraphKit.Facts;

namespace GraphKit.Analysis.Passes
{
    public static class SanityPass
    {
        public static FactBag Run(FactBag bag)
        {
            var nodes = bag.Nodes.ToDictionary(n => n.Id);
            var edges = bag.Edges
                .Where(e => e.FromId != e.ToId)
                .Where(e => nodes.ContainsKey(e.FromId) && nodes.ContainsKey(e.ToId))
                .ToList();
            return new FactBag(nodes.Values.ToList(), edges);
        }
    }
}
