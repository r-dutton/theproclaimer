using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;
using GraphKit.Outputs;
using Xunit;

namespace GraphKit.Tests;

public class GenericRepositoryExpansionTests
{
    [Fact]
    public void ControlledRepository_WriteQuery_Fallback_Expands_When_No_Method_Tagged()
    {
        var endpoint = new GraphNode
        {
            Id = "endpoint",
            Type = "endpoint.controller",
            Name = "Do",
            Fqdn = "Api.Controller.Do",
            Assembly = "Api",
            Project = "Api",
            FilePath = "Controller.cs",
            SymbolId = "T:Api.Controller.Do",
            Props = new Dictionary<string, object>
            {
                ["http_method"] = "GET",
                ["route"] = "/do",
                ["status_codes"] = new[] { 200 }
            }
        };

        var repoInterface = new GraphNode
        {
            Id = "irepo",
            Type = "app.service_contract",
            Name = "IControlledRepository<Firm>",
            Fqdn = "App.Data.IControlledRepository<Firm>",
            Assembly = "App",
            Project = "App",
            FilePath = "IControlledRepository.cs",
            SymbolId = "T:App.Data.IControlledRepository`1"
        };

        var repoImpl = new GraphNode
        {
            Id = "repo",
            Type = "app.repository",
            Name = "FirmRepository",
            Fqdn = "App.Data.FirmRepository",
            Assembly = "App",
            Project = "App",
            FilePath = "FirmRepository.cs",
            SymbolId = "T:App.Data.FirmRepository",
            Span = new GraphSpan { StartLine = 10, EndLine = 80 }
        };

        var firmEntity = new GraphNode
        {
            Id = "firm",
            Type = "ef.entity",
            Name = "Firm",
            Fqdn = "App.Domain.Firm",
            Assembly = "App",
            Project = "App",
            FilePath = "Firm.cs",
            SymbolId = "T:App.Domain.Firm",
            Span = new GraphSpan { StartLine = 5, EndLine = 120 }
        };

        var document = new GraphDocument
        {
            Version = "1.0",
            Nodes = new[] { endpoint, repoInterface, repoImpl, firmEntity },
            Edges = new[]
            {
                new GraphEdge
                {
                    From = repoInterface.Id,
                    To = repoImpl.Id,
                    Kind = "implemented_by",
                    Source = "static"
                },
                new GraphEdge
                {
                    From = endpoint.Id,
                    To = repoInterface.Id,
                    Kind = "uses_service",
                    Source = "static",
                    Props = new Dictionary<string, object>
                    {
                        ["method"] = "WriteQuery"
                    }
                },
                new GraphEdge
                {
                    From = repoImpl.Id,
                    To = firmEntity.Id,
                    Kind = "writes_to",
                    Source = "static"
                }
            }
        };

        var flow = FlowBuilder.BuildFlows(document, _ => true);

        Assert.Contains("calls FirmRepository.WriteQuery", flow);
        Assert.DoesNotContain("uses_service IControlledRepository<Firm>", flow);
        Assert.Contains("writes_to Firm", flow);
    }

    [Fact]
    public void ControlledRepository_UsesService_Dedupes_When_Direct_Call_Edge_Exists()
    {
        var endpoint = new GraphNode
        {
            Id = "endpoint",
            Type = "endpoint.controller",
            Name = "Do",
            Fqdn = "Api.Controller.Do",
            Assembly = "Api",
            Project = "Api",
            FilePath = "Controller.cs",
            SymbolId = "T:Api.Controller.Do",
            Span = new GraphSpan { StartLine = 20, EndLine = 60 },
            Props = new Dictionary<string, object>
            {
                ["http_method"] = "POST",
                ["route"] = "/do",
                ["status_codes"] = new[] { 200 }
            }
        };

        var repoInterface = new GraphNode
        {
            Id = "irepo",
            Type = "app.service_contract",
            Name = "IControlledRepository<Firm>",
            Fqdn = "App.Data.IControlledRepository<Firm>",
            Assembly = "App",
            Project = "App",
            FilePath = "IControlledRepository.cs",
            SymbolId = "T:App.Data.IControlledRepository`1"
        };

        var repoImpl = new GraphNode
        {
            Id = "repo",
            Type = "app.repository",
            Name = "FirmRepository",
            Fqdn = "App.Data.FirmRepository",
            Assembly = "App",
            Project = "App",
            FilePath = "FirmRepository.cs",
            SymbolId = "T:App.Data.FirmRepository"
        };

        var document = new GraphDocument
        {
            Version = "1.0",
            Nodes = new[] { endpoint, repoInterface, repoImpl },
            Edges = new[]
            {
                new GraphEdge
                {
                    From = endpoint.Id,
                    To = repoImpl.Id,
                    Kind = "calls",
                    Source = "static",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = endpoint.FilePath!, Line = 40 }
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["method"] = "WriteQuery"
                    }
                },
                new GraphEdge
                {
                    From = endpoint.Id,
                    To = repoInterface.Id,
                    Kind = "uses_service",
                    Source = "static",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = endpoint.FilePath!, Line = 42 }
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["method"] = "WriteQuery"
                    }
                }
            }
        };

        var flow = FlowBuilder.BuildFlows(document, _ => true);

        Assert.Contains("calls FirmRepository.WriteQuery [L40]", flow);
        Assert.DoesNotContain("uses_service IControlledRepository<Firm>", flow);
        Assert.Equal(1, flow.Split('\n').Count(line => line.Contains("calls FirmRepository.WriteQuery")));
    }
}
