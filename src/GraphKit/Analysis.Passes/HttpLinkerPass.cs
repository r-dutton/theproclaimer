using System.Collections.Generic;
using System.Linq;
using GraphKit.Facts;

namespace GraphKit.Analysis.Passes
{
    // Connect http.client → endpoint.* within/ across solutions using verb + route props.
    public static class HttpLinkerPass
    {
        public static FactBag Run(FactBag bag)
        {
            var nodes = bag.Nodes.ToDictionary(n => n.Id);
            var edges = new List<EdgeFact>(bag.Edges);

            var endpoints = bag.Nodes
                .Where(n => n.Type == "endpoint.controller" || n.Type == "endpoint.minimal_api")
                .ToList();

            var clients = bag.Nodes.Where(n => n.Type == "http.client").ToList();

            foreach (var client in clients)
            {
                // Expect edges with Kind "uses_client" originating from methods/services
                var clientUses = bag.Edges.Where(e => e.ToId == client.Id && e.Kind == "uses_client" && e.Props is { }).ToList();
                foreach (var use in clientUses)
                {
                    if (use.Props is not { } useProps)
                    {
                        continue;
                    }

                    var target = useProps.TryGetValue("target_service", out var targetValue) ? targetValue?.ToString() : null;

                    // Find matching endpoint by verb+route props if available
                    var verb = useProps.TryGetValue("verb", out var v) ? v?.ToString() : null;
                    var route = useProps.TryGetValue("route", out var r) ? r?.ToString() : null;
                    if (string.IsNullOrWhiteSpace(verb) || string.IsNullOrWhiteSpace(route)) continue;

                    var match = endpoints.FirstOrDefault(ep =>
                        (ep.Props.TryGetValue("verb", out var ev) ? ev?.ToString()?.ToUpperInvariant() : null) == verb.ToUpperInvariant() &&
                        (ep.Props.TryGetValue("route", out var er) ? er?.ToString() : null) == route);

                    if (match is not null)
                    {
                        var props = new Dictionary<string, object?> {
                            ["provenance"] = "Linker",
                            ["confidence"] = "High"
                        };
                        if (!string.IsNullOrWhiteSpace(target))
                        {
                            props["target_service"] = target;
                        }
                        edges.Add(new EdgeFact(client.Id, match.Id, "calls", props));
                    }
                }
            }
            return new FactBag(nodes.Values.ToList(), edges);
        }
    }
}
