using System;
using System.Linq;
using System.Collections.Generic;
using GraphKit.Graph;
using GraphKit.Outputs;
using Xunit;

namespace GraphKit.Tests;

public class Phase1EnhancementsTests
{
    [Fact]
    public void Mapping_Deduplicates_Identical_Edges()
    {
        var endpoint = new GraphNode
        {
            Id = "ep",
            Type = "endpoint.controller",
            Name = "Post",
            Fqdn = "Api.Controllers.Dummy.Post",
            Assembly = "Api",
            Project = "Api",
            FilePath = "Controllers/DummyController.cs",
            SymbolId = "Api.Controllers.Dummy.Post",
            Span = new GraphSpan { StartLine = 10, EndLine = 40 },
            Props = new Dictionary<string, object>{{"http_method","POST"},{"route","/api/dummy"},{"status_codes", new[]{200}}}
        };
        var dto = new GraphNode
        {
            Id = "dto",
            Type = "dto",
            Name = "DummyDto",
            Fqdn = "Api.DummyDto",
            Assembly = "Api",
            Project = "Api",
            FilePath = "Dtos/DummyDto.cs",
            SymbolId = "Api.DummyDto"
        };
        // Two identical maps_to edges
        var e1 = new GraphEdge { From = endpoint.Id, To = dto.Id, Kind = "maps_to", Source = "analysis", Props = new Dictionary<string, object>{{"variable","model"}}, Transform = new GraphTransform{ Location = new GraphLocation{ File = endpoint.FilePath, Line = 20 }}};
        var e2 = new GraphEdge { From = endpoint.Id, To = dto.Id, Kind = "maps_to", Source = "analysis", Props = new Dictionary<string, object>{{"variable","model"}}, Transform = new GraphTransform{ Location = new GraphLocation{ File = endpoint.FilePath, Line = 21 }}};
        var doc = new GraphDocument{ Version = "1", Nodes = new[]{ endpoint, dto }, Edges = new[]{ e1, e2 } };
        var flow = FlowBuilder.BuildFlows(doc, n => true);
        var occurrences = flow.Split("maps_to DummyDto", StringSplitOptions.None).Length - 1;
        Assert.Equal(1, occurrences);
    }

    [Fact]
    public void Cache_Deduplicates_Identical_Usages()
    {
        var endpoint = new GraphNode
        {
            Id = "ep2",
            Type = "endpoint.controller",
            Name = "Get",
            Fqdn = "Api.Controllers.Cache.Get",
            Assembly = "Api",
            Project = "Api",
            FilePath = "Controllers/CacheController.cs",
            SymbolId = "Api.Controllers.Cache.Get",
            Span = new GraphSpan { StartLine = 5, EndLine = 25 },
            Props = new Dictionary<string, object>{{"http_method","GET"},{"route","/api/cache"},{"status_codes", new[]{200}}}
        };
        var cache = new GraphNode
        {
            Id = "cache",
            Type = "cache.memory",
            Name = "MemoryCache",
            Fqdn = "Microsoft.Extensions.Caching.Memory.MemoryCache",
            Assembly = "Microsoft.Extensions.Caching.Memory",
            Project = "Microsoft.Extensions.Caching.Memory",
            FilePath = "MemoryCache.cs",
            SymbolId = "MemoryCache"
        };
        var c1 = new GraphEdge { From = endpoint.Id, To = cache.Id, Kind = "uses_cache", Source = "analysis", Props = new Dictionary<string, object>{{"method","Get"},{"operation","read"},{"key","k"}}, Transform = new GraphTransform{ Location = new GraphLocation{ File = endpoint.FilePath, Line = 12 }}};
        var c2 = new GraphEdge { From = endpoint.Id, To = cache.Id, Kind = "uses_cache", Source = "analysis", Props = new Dictionary<string, object>{{"method","Get"},{"operation","read"},{"key","k"}}, Transform = new GraphTransform{ Location = new GraphLocation{ File = endpoint.FilePath, Line = 13 }}};
        var doc = new GraphDocument{ Version = "1", Nodes = new[]{ endpoint, cache }, Edges = new[]{ c1, c2 } };
        var flow = FlowBuilder.BuildFlows(doc, n => true);
        var occurrences = flow.Split("uses_cache MemoryCache.Get [read] (key=k)", StringSplitOptions.None).Length - 1;
        Assert.Equal(1, occurrences);
    }

    [Fact]
    public void Pseudo_Service_Suppressed()
    {
        var endpoint = new GraphNode
        {
            Id = "ep3",
            Type = "endpoint.controller",
            Name = "GetTime",
            Fqdn = "Api.Controllers.Time.GetTime",
            Assembly = "Api",
            Project = "Api",
            FilePath = "Controllers/TimeController.cs",
            SymbolId = "Api.Controllers.Time.GetTime",
            Span = new GraphSpan { StartLine = 1, EndLine = 15 },
            Props = new Dictionary<string, object>{{"http_method","GET"},{"route","/api/time"},{"status_codes", new[]{200}}}
        };
        var timeProvider = new GraphNode { Id = "tp", Type = "app.service", Name = "ITimeProvider", Fqdn = "Infrastructure.ITimeProvider", Assembly = "Infra", Project = "Infra", FilePath = "external:ITimeProvider.cs", SymbolId = "ITimeProvider" };
        var realInterface = new GraphNode { Id = "ri", Type = "app.service", Name = "IDataService", Fqdn = "Api.IDataService", Assembly = "Api", Project = "Api", FilePath = "IDataService.cs", SymbolId = "IDataService" };
        var realImpl = new GraphNode { Id = "rimpl", Type = "app.service", Name = "DataService", Fqdn = "Api.DataService", Assembly = "Api", Project = "Api", FilePath = "DataService.cs", SymbolId = "DataService", Span = new GraphSpan{ StartLine=5, EndLine=20 } };
        var implEdge = new GraphEdge{ From = realInterface.Id, To = realImpl.Id, Kind = "implemented_by", Source = "static" };
        var useTime = new GraphEdge{ From = endpoint.Id, To = timeProvider.Id, Kind = "uses_service", Source = "analysis", Transform = new GraphTransform{ Location = new GraphLocation{ File = endpoint.FilePath, Line = 5 } }, Props = new Dictionary<string, object>{{"method","Now"}} };
        var useReal = new GraphEdge{ From = endpoint.Id, To = realInterface.Id, Kind = "uses_service", Source = "analysis", Transform = new GraphTransform{ Location = new GraphLocation{ File = endpoint.FilePath, Line = 6 } }, Props = new Dictionary<string, object>{{"method","Fetch"}} };
        var doc = new GraphDocument{ Version = "1", Nodes = new[]{ endpoint, timeProvider, realInterface, realImpl }, Edges = new[]{ implEdge, useTime, useReal } };
        var flow = FlowBuilder.BuildFlows(doc, n => true);
        Assert.DoesNotContain("uses_service ITimeProvider", flow);
        Assert.Contains("implementation Api.DataService.Fetch", flow);
    }

    [Fact]
    public void Subtree_Request_Reused_Shows_Reused_Marker()
    {
        var endpoint = new GraphNode { Id = "ep4", Type = "endpoint.controller", Name = "Do", Fqdn = "Api.Controllers.Work.Do", Assembly = "Api", Project = "Api", FilePath = "Controllers/WorkController.cs", SymbolId = "Do", Span = new GraphSpan{ StartLine=1, EndLine=20 }, Props = new Dictionary<string, object>{{"http_method","POST"},{"route","/api/do"},{"status_codes", new[]{200}}} };
        var serviceInterface = new GraphNode { Id = "si", Type = "app.service", Name = "IWorkService", Fqdn = "Api.IWorkService", Assembly = "Api", Project = "Api", FilePath = "IWorkService.cs", SymbolId = "IWorkService" };
        var serviceImpl = new GraphNode { Id = "simpl", Type = "app.service", Name = "WorkService", Fqdn = "Api.WorkService", Assembly = "Api", Project = "Api", FilePath = "WorkService.cs", SymbolId = "WorkService", Span = new GraphSpan{ StartLine=5, EndLine=50 } };
        var request = new GraphNode { Id = "req", Type = "cqrs.request", Name = "DoWork", Fqdn = "Api.DoWork", Assembly = "Api", Project = "Api", FilePath = "DoWork.cs", SymbolId = "DoWork" };
        var handler = new GraphNode { Id = "hdl", Type = "cqrs.handler", Name = "DoWorkHandler", Fqdn = "Api.DoWorkHandler", Assembly = "Api", Project = "Api", FilePath = "DoWorkHandler.cs", SymbolId = "DoWorkHandler", Span = new GraphSpan{ StartLine=10, EndLine=30 } };
        var implEdge = new GraphEdge{ From = serviceInterface.Id, To = serviceImpl.Id, Kind = "implemented_by", Source = "static" };
        // Two sends_request edges from implementation to same request
        var sr1 = new GraphEdge{ From = serviceImpl.Id, To = request.Id, Kind = "sends_request", Source = "analysis", Transform = new GraphTransform{ Location = new GraphLocation{ File = serviceImpl.FilePath, Line = 15 } } };
        var sr2 = new GraphEdge{ From = serviceImpl.Id, To = request.Id, Kind = "sends_request", Source = "analysis", Transform = new GraphTransform{ Location = new GraphLocation{ File = serviceImpl.FilePath, Line = 25 } } };
        var handled = new GraphEdge{ From = request.Id, To = handler.Id, Kind = "handled_by", Source = "analysis", Transform = new GraphTransform{ MethodSpan = handler.Span } };
        var useSvc = new GraphEdge{ From = endpoint.Id, To = serviceInterface.Id, Kind = "uses_service", Source = "analysis", Props = new Dictionary<string, object>{{"method","Execute"}}, Transform = new GraphTransform{ Location = new GraphLocation{ File = endpoint.FilePath, Line = 8 } } };
        var doc = new GraphDocument{ Version = "1", Nodes = new[]{ endpoint, serviceInterface, serviceImpl, request, handler }, Edges = new[]{ implEdge, sr1, sr2, handled, useSvc } };
        var flow = FlowBuilder.BuildFlows(doc, n => true);
        var firstOccurrences = flow.Split("sends_request DoWork", StringSplitOptions.None).Length - 1;
        Assert.True(firstOccurrences >= 2, flow); // we printed two lines (one reused)
    Assert.Contains("... (reused)", flow);
    }

    [Fact]
    public void Heuristic_Tag_Only_For_External_Pure_Heuristic()
    {
        var endpoint = new GraphNode { Id = "ep5", Type = "endpoint.controller", Name = "Ext", Fqdn = "Api.Controllers.Ext.Get", Assembly = "Api", Project = "Api", FilePath = "Controllers/ExtController.cs", SymbolId = "Ext.Get", Span = new GraphSpan{ StartLine=1, EndLine=10 }, Props = new Dictionary<string, object>{{"http_method","GET"},{"route","/api/ext"},{"status_codes", new[]{200}}} };
        var extInterface = new GraphNode { Id = "ie", Type = "app.service", Name = "IExternalSvc", Fqdn = "Api.IExternalSvc", Assembly = "Api", Project = "Api", FilePath = "IExternalSvc.cs", SymbolId = "IExternalSvc" };
        var extImpl = new GraphNode { Id = "eimpl", Type = "app.service", Name = "ExternalSvc", Fqdn = "External.ExternalSvc", Assembly = "External", Project = "External", FilePath = "external:ExternalSvc.cs", SymbolId = "ExternalSvc" };
        var intInterface = new GraphNode { Id = "ii", Type = "app.service", Name = "IInternalSvc", Fqdn = "Api.IInternalSvc", Assembly = "Api", Project = "Api", FilePath = "IInternalSvc.cs", SymbolId = "IInternalSvc" };
        var intImpl = new GraphNode { Id = "iimpl", Type = "app.service", Name = "InternalSvc", Fqdn = "Api.InternalSvc", Assembly = "Api", Project = "Api", FilePath = "InternalSvc.cs", SymbolId = "InternalSvc", Span = new GraphSpan{ StartLine=5, EndLine=15 } };
        var extEdge = new GraphEdge{ From = endpoint.Id, To = extInterface.Id, Kind = "uses_service", Source = "analysis", Transform = new GraphTransform{ Location = new GraphLocation{ File = endpoint.FilePath, Line = 5 } }, Props = new Dictionary<string, object>{{"method","Run"}} };
        var intEdge = new GraphEdge{ From = endpoint.Id, To = intInterface.Id, Kind = "uses_service", Source = "analysis", Transform = new GraphTransform{ Location = new GraphLocation{ File = endpoint.FilePath, Line = 6 } }, Props = new Dictionary<string, object>{{"method","Run"}} };
        var implExt = new GraphEdge{ From = extInterface.Id, To = extImpl.Id, Kind = "implemented_by", Source = "analysis" };
        var implInt = new GraphEdge{ From = intInterface.Id, To = intImpl.Id, Kind = "implemented_by", Source = "analysis" };
        var doc = new GraphDocument{ Version = "1", Nodes = new[]{ endpoint, extInterface, extImpl, intInterface, intImpl }, Edges = new[]{ extEdge, intEdge, implExt, implInt } };
        var flow = FlowBuilder.BuildFlows(doc, n => true);
    Assert.Contains("implementation Api.IExternalSvc.Run", flow); // interface self treated as implementation
    Assert.DoesNotContain("implementation Api.IExternalSvc.Run [heuristic]", flow); // no heuristic tag due to internal file
    Assert.Contains("implementation Api.InternalSvc.Run", flow); // internal concrete printed
    Assert.DoesNotContain("implementation Api.InternalSvc.Run [heuristic]", flow); // internal has evidence
    Assert.DoesNotContain("External.ExternalSvc", flow); // external impl filtered out in current heuristic preference
    }
}
