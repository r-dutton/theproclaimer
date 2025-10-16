using GraphKit.Graph;
using GraphKit.Outputs;
using Xunit;

namespace GraphKit.Tests;

public class LoggerResolutionTests
{
    [Fact]
    public void ILogger_Generic_Wrapper_Suppressed_No_Placeholder()
    {
        // Arrange: Define an interface IFooService with an implementation FooService, and ILogger<IFooService> usage edge.
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
        var fooInterface = new GraphNode {
            Id = "foo-intf",
            Type = "app.service",
            Name = "IFooService",
            Fqdn = "App.Services.IFooService",
            Assembly = "App",
            Project = "App",
            FilePath = "IFooService.cs",
            SymbolId = "T:App.Services.IFooService"
        };
        var fooImpl = new GraphNode {
            Id = "foo-impl",
            Type = "app.service",
            Name = "FooService",
            Fqdn = "App.Services.FooService",
            Assembly = "App",
            Project = "App",
            FilePath = "FooService.cs",
            SymbolId = "T:App.Services.FooService",
            Span = new GraphSpan{ StartLine = 10, EndLine = 50 }
        };
        var loggerWrapper = new GraphNode {
            Id = "logger-wrapper",
            Type = "app.service",
            Name = "ILogger<IFooService>",
            Fqdn = "Microsoft.Extensions.Logging.ILogger<App.Services.IFooService>",
            Assembly = "Microsoft.Extensions.Logging.Abstractions",
            Project = "Microsoft.Extensions.Logging.Abstractions",
            FilePath = "external:ILogger.cs",
            SymbolId = "T:Microsoft.Extensions.Logging.ILogger`1"
        };

        var implEdge = new GraphEdge {
            From = fooInterface.Id,
            To = fooImpl.Id,
            Kind = "implemented_by",
            Source = "static"
        };
        var usesLoggerEdge = new GraphEdge {
            From = controller.Id,
            To = loggerWrapper.Id,
            Kind = "uses_service",
            Source = "static",
            Props = new System.Collections.Generic.Dictionary<string, object>{{"method","GetFoo"}},
            Transform = new GraphTransform { Location = new GraphLocation { File = "Controller.cs", Line = 5 } }
        };

        var doc = new GraphDocument {
            Version = "1.0",
            Nodes = new[] { controller, fooInterface, fooImpl, loggerWrapper },
            Edges = new[] { implEdge, usesLoggerEdge }
        };

        // Act
        var flow = FlowBuilder.BuildFlows(doc, n => true);

        // Assert: ILogger wrapper should be suppressed entirely (no uses_service ILogger...) and no placeholder line
        Assert.DoesNotContain("uses_service ILogger<IFooService>", flow);
        Assert.DoesNotContain("no implementation details available", flow);
    }
}
