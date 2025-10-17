using System.Linq;
using GraphKit.Graph;
using GraphKit.Outputs;
using Xunit;

namespace GraphKit.Tests;

public class RequestProcessorFlowTests
{
    [Fact]
    public void RequestProcessorImplementation_ExpandsDispatchesRecursively()
    {
        // Arrange: create nodes
        var controller = new GraphNode {
            Id = "endpoint-1",
            Type = "endpoint.controller",
            Name = "GetFoo",
            Fqdn = "Api.Controller.GetFoo",
            Assembly = "Api",
            Project = "Api",
            FilePath = "Controller.cs",
            SymbolId = "T:Api.Controller.GetFoo",
            Props = new System.Collections.Generic.Dictionary<string, object>{{"http_method","GET"},{"route","/foo"},{"status_codes", new[]{200}}}
        };
        var request = new GraphNode {
            Id = "req-1",
            Type = "cqrs.request",
            Name = "GetFooQuery",
            Fqdn = "App.Queries.GetFooQuery",
            Assembly = "App",
            Project = "App",
            FilePath = "Request.cs",
            SymbolId = "T:App.Queries.GetFooQuery"
        };
        var handler = new GraphNode {
            Id = "handler-1",
            Type = "cqrs.handler",
            Name = "GetFooQueryHandler",
            Fqdn = "App.Queries.GetFooQueryHandler",
            Assembly = "App",
            Project = "App",
            FilePath = "Handler.cs",
            SymbolId = "T:App.Queries.GetFooQueryHandler",
            Span = new GraphSpan{ StartLine = 10, EndLine = 20 }
        };
        var processorImpl = new GraphNode {
            Id = "proc-impl",
            Type = "app.service",
            Name = "RequestProcessor",
            Fqdn = "App.Services.RequestProcessor",
            Assembly = "App",
            Project = "App",
            FilePath = "RequestProcessor.cs",
            SymbolId = "T:App.Services.RequestProcessor"
        };

        // Edges: controller uses service RequestProcessor.ProcessAsync and synthetic dispatch to request, then handled_by
        var usesService = new GraphEdge {
            From = controller.Id,
            To = processorImpl.Id,
            Kind = "uses_service",
            Source = "static",
            Props = new System.Collections.Generic.Dictionary<string, object>{{"method","ProcessAsync"}},
            Transform = new GraphTransform{ Location = new GraphLocation{ File = "Controller.cs", Line = 5 } }
        };
        var sends = new GraphEdge {
            From = controller.Id,
            To = request.Id,
            Kind = "sends_request",
            Source = "synthetic",
            Transform = new GraphTransform{ Type = "requestprocessor.dispatch", Location = new GraphLocation{ File = "Controller.cs", Line = 5 } },
            Props = new System.Collections.Generic.Dictionary<string, object>{{"response_type","FooDto"}}
        };
        var handled = new GraphEdge {
            From = request.Id,
            To = handler.Id,
            Kind = "handled_by",
            Source = "synthetic",
            Transform = new GraphTransform{ Type = "requestprocessor.dispatch" }
        };

        var doc = new GraphDocument
        {
            Version = "1.0",
            Nodes = new[] { controller, request, handler, processorImpl },
            Edges = new[] { usesService, sends, handled }
        };

        // Act
        var flow = FlowBuilder.BuildFlows(doc, n => true);

        // Assert: ensure the RequestProcessor implementation expanded dispatches
        Assert.Contains("dispatches GetFooQuery", flow);
        Assert.Contains("handled_by App.Queries.GetFooQueryHandler.Handle", flow);
    }
}
