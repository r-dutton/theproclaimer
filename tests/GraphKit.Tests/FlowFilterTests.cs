using System.Linq;
using GraphKit.Graph;
using GraphKit.Outputs;
using Xunit;

namespace GraphKit.Tests;

public class FlowFilterTests
{
    [Fact]
    public void Matches_AllPattern_ReturnsTrue()
    {
        var controller = CreateController("Sample.Web.Controllers.ReportsController.GetReport", "/api/reports/{id:guid}");

        Assert.True(FlowFilter.Matches(controller, "*"));
        Assert.True(FlowFilter.Matches(controller, "ALL"));
    }

    [Fact]
    public void Matches_WildcardPattern_UsesGlobSemantics()
    {
        var controller = CreateController("Sample.Web.Controllers.ReportsController.GetReport", "/api/reports/{id:guid}");

        Assert.True(FlowFilter.Matches(controller, "Sample.Web.Controllers.*"));
        Assert.True(FlowFilter.Matches(controller, "*.GetReport"));
        Assert.True(FlowFilter.Matches(controller, "*/reports/*"));
    }

    [Fact]
    public void BuildPredicate_ComposesMultiplePatterns()
    {
        var controller = CreateController("Sample.Web.Controllers.ReportsController.CreateDetailedReport", "/api/reports");
        var predicate = FlowFilter.BuildPredicate(new[] { "Sample.Web.Controllers.*", "Does.Not.Match" });

        Assert.True(predicate(controller));
    }

    private static GraphNode CreateController(string id, string route)
        => new()
        {
            Id = id,
            Type = "endpoint.controller",
            Name = id.Split('.').Last(),
            Fqdn = id,
            Assembly = "Sample.Web",
            Project = "Sample.Web",
            FilePath = "Controllers/ReportsController.cs",
            SymbolId = id,
            Props = new Dictionary<string, object>
            {
                ["route"] = route,
                ["http_method"] = "GET"
            }
        };
}
