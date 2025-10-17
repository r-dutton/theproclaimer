using System.Collections.Concurrent;
using GraphKit.Analyzers;
using GraphKit.Graph;
using Xunit;

namespace GraphKit.Tests
{
    public class SyntheticClientLinkerTests
    {
        [Fact]
        public void Synthetic_Calls_Edge_Created_When_Missing()
        {
            var endpoint = new GraphNode
            {
                Id = "ep",
                Type = "endpoint.controller",
                Name = "GetThing",
                Fqdn = "Api.ThingController.GetThing",
                Assembly = "Api",
                Project = "Api",
                FilePath = "ThingController.cs",
                SymbolId = "T:Api.ThingController.GetThing",
                Props = new System.Collections.Generic.Dictionary<string, object>
                {
                    {"http_method", "GET"},
                    {"route", "/thing"},
                    {"status_codes", new[]{200}}
                }
            };
            var client = new GraphNode
            {
                Id = "cl",
                Type = "http.client",
                Name = "ThingClient",
                Fqdn = "App.ThingClient",
                Assembly = "App",
                Project = "App",
                FilePath = "ThingClient.cs",
                SymbolId = "T:App.ThingClient"
            };
            // uses_client edge referencing route + verb metadata but missing direct calls edge
            var usesClient = new GraphEdge
            {
                From = endpoint.Id, // caller endpoint uses the client
                To = client.Id,
                Kind = "uses_client",
                Source = "static",
                Transform = new GraphTransform { Location = new GraphLocation { File = "ThingController.cs", Line = 42 } },
                Props = new System.Collections.Generic.Dictionary<string, object>
                {
                    {"verb", "GET"},
                    {"route", "/thing"}
                }
            };
            var nodes = new ConcurrentDictionary<string, GraphNode>();
            nodes[endpoint.Id] = endpoint;
            nodes[client.Id] = client;
            var edges = new ConcurrentBag<GraphEdge>();
            edges.Add(usesClient);
            var clientTargets = new ConcurrentDictionary<string, string>();
            // Invoke synthetic linker
            ClientLinker.EmitClientUseCallEdges(nodes, edges, clientTargets);
            // Validate a calls edge now exists
            Assert.Contains(edges, e => e.Kind == "calls" && e.From == client.Id && e.To == endpoint.Id);
        }
    }
}
