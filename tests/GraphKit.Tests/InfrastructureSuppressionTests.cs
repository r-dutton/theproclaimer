using GraphKit.Graph;
using GraphKit.Outputs;
using Xunit;

namespace GraphKit.Tests
{
    public class InfrastructureSuppressionTests
    {
    [Fact]
    public void ILogger_And_IMapper_UsesService_Suppressed()
    {
        var endpoint = new GraphNode
        {
            Id = "e",
            Type = "endpoint.controller",
            Name = "Do",
            Fqdn = "Api.C.Do",
            Assembly = "Api",
            Project = "Api",
            FilePath = "C.cs",
            SymbolId = "T:Api.C.Do",
            Props = new System.Collections.Generic.Dictionary<string, object>
            {
                { "http_method", "GET" },
                { "route", "/do" },
                { "status_codes", new[] { 200 } }
            }
        };
        var logger = new GraphNode
        {
            Id = "l",
            Type = "app.service",
            Name = "ILogger<DoHandler>",
            Fqdn = "Microsoft.Extensions.Logging.ILogger<Api.C.DoHandler>",
            Assembly = "Logging",
            Project = "Logging",
            FilePath = "external:ILogger.cs",
            SymbolId = "T:Microsoft.Extensions.Logging.ILogger`1"
        };
        var mapper = new GraphNode
        {
            Id = "m",
            Type = "app.service",
            Name = "IMapper",
            Fqdn = "AutoMapper.IMapper",
            Assembly = "AutoMapper",
            Project = "AutoMapper",
            FilePath = "external:IMapper.cs",
            SymbolId = "T:AutoMapper.IMapper"
        };
        var handlerInterface = new GraphNode
        {
            Id = "hI",
            Type = "app.service",
            Name = "IDoHandler",
            Fqdn = "App.IDoHandler",
            Assembly = "App",
            Project = "App",
            FilePath = "IDoHandler.cs",
            SymbolId = "T:App.IDoHandler"
        };
        var handlerImpl = new GraphNode
        {
            Id = "h",
            Type = "cqrs.handler",
            Name = "DoHandler",
            Fqdn = "App.DoHandler",
            Assembly = "App",
            Project = "App",
            FilePath = "DoHandler.cs",
            SymbolId = "T:App.DoHandler",
            Span = new GraphSpan { StartLine = 5, EndLine = 25 }
        };
        var implEdge = new GraphEdge { From = handlerInterface.Id, To = handlerImpl.Id, Kind = "implemented_by", Source = "static" };
        var usesLogger = new GraphEdge
        {
            From = endpoint.Id,
            To = logger.Id,
            Kind = "uses_service",
            Source = "static",
            Transform = new GraphTransform { Location = new GraphLocation { File = "C.cs", Line = 10 } },
            Props = new System.Collections.Generic.Dictionary<string, object> { { "method", "LogInformation" } }
        };
        var usesMapper = new GraphEdge
        {
            From = endpoint.Id,
            To = mapper.Id,
            Kind = "uses_service",
            Source = "static",
            Transform = new GraphTransform { Location = new GraphLocation { File = "C.cs", Line = 11 } },
            Props = new System.Collections.Generic.Dictionary<string, object> { { "method", "Map" } }
        };
        var usesHandler = new GraphEdge
        {
            From = endpoint.Id,
            To = handlerInterface.Id,
            Kind = "uses_service",
            Source = "static",
            Transform = new GraphTransform { Location = new GraphLocation { File = "C.cs", Line = 12 } },
            Props = new System.Collections.Generic.Dictionary<string, object> { { "method", "Handle" } }
        };
        var doc = new GraphDocument
        {
            Version = "1.0",
            Nodes = new[] { endpoint, logger, mapper, handlerInterface, handlerImpl },
            Edges = new[] { implEdge, usesLogger, usesMapper, usesHandler }
        };
        var flow = FlowBuilder.BuildFlows(doc, n => true);
        Assert.DoesNotContain("uses_service ILogger", flow);
        Assert.DoesNotContain("uses_service IMapper", flow);
        Assert.Contains("implementation App.DoHandler.Handle", flow);
    }

    [Fact]
    public void PipelineBehavior_Resolves_Inner_Services()
    {
        var endpoint = new GraphNode
        {
            Id = "e2",
            Type = "endpoint.controller",
            Name = "Do2",
            Fqdn = "Api.C.Do2",
            Assembly = "Api",
            Project = "Api",
            FilePath = "C2.cs",
            SymbolId = "T:Api.C.Do2",
            Props = new System.Collections.Generic.Dictionary<string, object>
            {
                { "http_method", "POST" },
                { "route", "/do2" },
                { "status_codes", new[] { 200 } }
            }
        };
        var behavior = new GraphNode
        {
            Id = "pb",
            Type = "app.service",
            Name = "IPipelineBehavior<Req,Res>",
            Fqdn = "MediatR.IPipelineBehavior<App.Req,App.Res>",
            Assembly = "App",
            Project = "App",
            FilePath = "Pipeline.cs",
            SymbolId = "T:MediatR.IPipelineBehavior`2"
        };
        var innerInterface = new GraphNode
        {
            Id = "svcI",
            Type = "app.service",
            Name = "IInnerSvc",
            Fqdn = "App.IInnerSvc",
            Assembly = "App",
            Project = "App",
            FilePath = "IInnerSvc.cs",
            SymbolId = "T:App.IInnerSvc"
        };
        var innerImpl = new GraphNode
        {
            Id = "svc",
            Type = "app.service",
            Name = "InnerSvc",
            Fqdn = "App.InnerSvc",
            Assembly = "App",
            Project = "App",
            FilePath = "InnerSvc.cs",
            SymbolId = "T:App.InnerSvc",
            Span = new GraphSpan { StartLine = 3, EndLine = 15 }
        };
        var implEdge = new GraphEdge { From = innerInterface.Id, To = innerImpl.Id, Kind = "implemented_by", Source = "static" };
        var usesBehavior = new GraphEdge
        {
            From = endpoint.Id,
            To = behavior.Id,
            Kind = "uses_service",
            Source = "static",
            Transform = new GraphTransform { Location = new GraphLocation { File = "C2.cs", Line = 5 } },
            Props = new System.Collections.Generic.Dictionary<string, object> { { "method", "Handle" } }
        };
        var behaviorUsesInner = new GraphEdge
        {
            From = behavior.Id,
            To = innerInterface.Id,
            Kind = "uses_service",
            Source = "static",
            Transform = new GraphTransform { Location = new GraphLocation { File = "Pipeline.cs", Line = 22 } },
            Props = new System.Collections.Generic.Dictionary<string, object> { { "method", "Handle" } }
        };
        var doc = new GraphDocument
        {
            Version = "1.0",
            Nodes = new[] { endpoint, behavior, innerInterface, innerImpl },
            Edges = new[] { implEdge, usesBehavior, behaviorUsesInner }
        };
        var flow = FlowBuilder.BuildFlows(doc, n => true);
        Assert.True(flow.Contains("uses_service IInnerSvc") || flow.Contains("uses_service InnerSvc"), flow);
    }
    }
}