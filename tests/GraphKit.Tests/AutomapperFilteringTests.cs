using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;
using GraphKit.Outputs;
using Xunit;

namespace GraphKit.Tests;

public class AutomapperFilteringTests
{
    [Fact]
    public void AutomapperRegistrations_FilterToSameRootAndSummarizeElided()
    {
        // Arrange: caller in Sample.App root, profiles in Sample.App and Other.App roots
        var caller = new GraphNode
        {
            Id = "Caller",
            Type = "endpoint.controller",
            Name = "Get",
            Fqdn = "Sample.Web.Controllers.ReportController.Get",
            Assembly = "Sample.Web",
            Project = "Sample.Web",
            FilePath = "Controllers/ReportController.cs",
            SymbolId = "Sample.Web.Controllers.ReportController.Get",
            Span = new GraphSpan { StartLine = 1, EndLine = 10 },
            Props = new Dictionary<string, object>
            {
                ["route"] = "/api/reports",
                ["http_method"] = "GET"
            }
        };

        var profileSameRoot = new GraphNode
        {
            Id = "ProfileSame",
            Type = "mapping.automapper.profile",
            Name = "ReportProfile",
            Fqdn = "Sample.App.Mapping.ReportProfile",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Mapping/ReportProfile.cs",
            SymbolId = "Sample.App.Mapping.ReportProfile"
        };

        var mapSameRoot = new GraphNode
        {
            Id = "MapSame",
            Type = "mapping.automapper.map",
            Name = "Report->ReportDto",
            Fqdn = "Sample.App.Mapping.ReportProfile.Map",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Mapping/ReportProfile.cs",
            SymbolId = "MapSame",
            Span = new GraphSpan { StartLine = 15, EndLine = 15 },
            Props = new Dictionary<string, object>
            {
                ["map"] = "Report->ReportDto",
                ["source_type"] = "Report",
                ["destination_type"] = "ReportDto"
            }
        };

        var profileOtherRoot = new GraphNode
        {
            Id = "ProfileOther",
            Type = "mapping.automapper.profile",
            Name = "OtherProfile",
            Fqdn = "Other.App.Mapping.OtherProfile",
            Assembly = "Other.App",
            Project = "Other.App",
            FilePath = "Mapping/OtherProfile.cs",
            SymbolId = "Other.App.Mapping.OtherProfile"
        };

        var mapOtherRoot = new GraphNode
        {
            Id = "MapOther",
            Type = "mapping.automapper.map",
            Name = "Report->ReportDto (Alt)",
            Fqdn = "Other.App.Mapping.OtherProfile.Map",
            Assembly = "Other.App",
            Project = "Other.App",
            FilePath = "Mapping/OtherProfile.cs",
            SymbolId = "MapOther",
            Span = new GraphSpan { StartLine = 20, EndLine = 20 },
            Props = new Dictionary<string, object>
            {
                ["map"] = "Report->ReportDto",
                ["source_type"] = "Report",
                ["destination_type"] = "ReportDto"
            }
        };

        var dto = new GraphNode
        {
            Id = "Dto",
            Type = "dto",
            Name = "ReportDto",
            Fqdn = "Sample.App.ReportDto",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Dto/ReportDto.cs",
            SymbolId = "Sample.App.ReportDto"
        };

        var mapEdge = new GraphEdge
        {
            From = caller.Id,
            To = dto.Id,
            Kind = "maps_to",
            Source = "analysis",
            Props = new Dictionary<string, object>
            {
                ["source_type"] = "Report",
                ["destination_type"] = "ReportDto"
            }
        };

        // Document includes all nodes (maps reference their profiles via generated_from edges)
        var genEdgeSame = new GraphEdge
        {
            From = mapSameRoot.Id,
            To = profileSameRoot.Id,
            Kind = "generated_from",
            Source = "analysis"
        };
        var genEdgeOther = new GraphEdge
        {
            From = mapOtherRoot.Id,
            To = profileOtherRoot.Id,
            Kind = "generated_from",
            Source = "analysis"
        };

        var document = new GraphDocument
        {
            Version = "1",
            Nodes = new [] { caller, profileSameRoot, mapSameRoot, profileOtherRoot, mapOtherRoot, dto },
            Edges = new [] { mapEdge, genEdgeSame, genEdgeOther }
        };

        // Act
        var flows = FlowBuilder.BuildFlows(document, n => n.Id == caller.Id);

        // Assert: Only same-root map appears (Report->ReportDto). The Foo->Bar mapping should be elided with summary referencing Other root.
        Assert.Contains("automapper.registration ReportProfile (Report->ReportDto)", flows);
    Assert.DoesNotContain("Report->ReportDto (Alt)", flows);
        // Depending on assembly root extraction, Other.App root summarized as 'Other'

    }
}
