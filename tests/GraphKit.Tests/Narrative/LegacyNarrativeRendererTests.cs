using System.Collections.Generic;
using GraphKit.Constants;
using GraphKit.Facts;
using GraphKit.Outputs.Narrative;
using Xunit;

namespace GraphKit.Tests.Narrative;

public sealed class LegacyNarrativeRendererTests
{
    [Fact]
    public void ServiceWithoutEdgesEmitsNoEdgeMarker()
    {
        var endpoint = new NodeFact(
            "endpoint",
            NodeTypes.EndpointController,
            new Dictionary<string, object?>
            {
                ["controller_display"] = "Sample.Api.SampleController",
                ["name"] = "Create",
                ["route"] = "/sample",
                ["verb"] = "POST",
                ["file"] = "/repo/Controllers/SampleController.cs",
                ["start_line"] = 10,
                ["end_line"] = 20
            });

        var request = new NodeFact(
            "request",
            NodeTypes.CqrsRequest,
            new Dictionary<string, object?>
            {
                ["name"] = "SampleRequest",
                ["file"] = "/repo/Application/SampleRequest.cs",
                ["start_line"] = 1,
                ["end_line"] = 10
            });

        var handler = new NodeFact(
            "handler",
            NodeTypes.CqrsHandler,
            new Dictionary<string, object?>
            {
                ["name"] = "SampleHandler",
                ["handler"] = "SampleHandler.Handle",
                ["file"] = "/repo/Application/SampleHandler.cs",
                ["start_line"] = 1,
                ["end_line"] = 10
            });

        var service = new NodeFact(
            "service",
            NodeTypes.AppService,
            new Dictionary<string, object?>
            {
                ["name"] = "RequestInfoService",
                ["file"] = "/repo/Services/RequestInfoService.cs",
                ["start_line"] = 1,
                ["end_line"] = 10
            });

        var nodes = new[] { endpoint, request, handler, service };

        var edges = new[]
        {
            new EdgeFact(
                "endpoint",
                "request",
                EdgeKinds.SendsRequest,
                new Dictionary<string, object?>
                {
                    ["request_type"] = "SampleRequest",
                    ["response_type"] = "Unit",
                    ["line"] = 15,
                    ["file"] = "/repo/Controllers/SampleController.cs",
                    ["verb"] = "POST",
                    ["route"] = "/sample"
                }),
            new EdgeFact(
                "request",
                "handler",
                EdgeKinds.HandledBy,
                new Dictionary<string, object?>
                {
                    ["handler"] = "SampleHandler.Handle",
                    ["line"] = 5,
                    ["file"] = "/repo/Application/SampleHandler.cs"
                }),
            new EdgeFact(
                "handler",
                "service",
                EdgeKinds.UsesService,
                new Dictionary<string, object?>
                {
                    ["service_type"] = "RequestInfoService",
                    ["method"] = "IsValid",
                    ["line"] = 7,
                    ["file"] = "/repo/Application/SampleHandler.cs"
                })
        };

        var bag = new FactBag(nodes, edges);
        var narratives = LegacyNarrativeRenderer.Collect(bag, "/repo");
        var text = Assert.Single(narratives).Text;

        Assert.Contains("(no additional service edges recorded)", text);
    }
}
