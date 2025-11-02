using System.Collections.Generic;
using System.Linq;
using GraphKit.Facts;

namespace GraphKit.Analysis.Passes
{
    public static class DeduplicatePass
    {
        public static FactBag Run(FactBag bag)
        {
            var nodeSet = new Dictionary<string, NodeFact>();
            foreach (var n in bag.Nodes)
                nodeSet[n.Id] = n;

            var seen = new HashSet<string>();
            var edges = new List<EdgeFact>();
            foreach (var e in bag.Edges)
            {
                var key = $"{e.FromId}|{e.Kind}|{e.ToId}|{HashProps(e.Props)}";
                if (seen.Add(key)) edges.Add(e);
            }
            return new FactBag(nodeSet.Values.ToList(), edges);
        }

        private static string HashProps(Dictionary<string, object?> props)
            => string.Join("&", props.OrderBy(kv => kv.Key).Select(kv => $"{kv.Key}={kv.Value}"));
    }
}
