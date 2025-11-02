using System.Collections.Generic;
using System.Linq;
using GraphKit.Facts;

namespace GraphKit.Analysis.Passes
{
    // Connect message.publisher → notification handler(s) by exact contract type/name.
    public static class MessageLinkerPass
    {
        public static FactBag Run(FactBag bag)
        {
            var nodes = bag.Nodes.ToDictionary(n => n.Id);
            var edges = new List<EdgeFact>(bag.Edges);

            // Build contract -> consumer map
            var consumers = bag.Nodes
                .Where(n => n.Type == "notification.handler" || n.Type == "message.consumer")
                .ToList();

            var byContract = consumers
                .Where(n => n.Props.TryGetValue("contract", out var _))
                .GroupBy(n => n.Props["contract"]!.ToString()!)
                .ToDictionary(g => g.Key, g => g.Select(n => n).ToList());

            var publishers = bag.Nodes
                .Where(n => n.Type == "message.publisher" || n.Type == "domain.event.publisher")
                .ToList();

            foreach (var pub in publishers)
            {
                if (!pub.Props.TryGetValue("contract", out var cobj) || cobj is null) continue;
                var contract = cobj.ToString()!;
                if (!byContract.TryGetValue(contract, out var sinks)) continue;

                foreach (var sink in sinks)
                {
                    var props = new Dictionary<string, object?> {
                        ["provenance"] = "Linker",
                        ["confidence"] = "High"
                    };
                    edges.Add(new EdgeFact(pub.Id, sink.Id, "processed_by", props));
                }
            }
            return new FactBag(nodes.Values.ToList(), edges);
        }
    }
}
