using System.Collections.Generic;
using GraphKit.Graph;
using GraphKit.Outputs;
using Xunit;

namespace GraphKit.Tests;

public class FlowBuilderTests
{
    [Fact]
    public void BuildFlows_ProducesContentForControllers()
    {
        var controller = new GraphNode
        {
            Id = "Sample.Web.Controllers.ReportsController.Create",
            Type = "endpoint.controller",
            Name = "Create",
            Fqdn = "Sample.Web.Controllers.ReportsController.Create",
            Assembly = "Sample.Web",
            Project = "Sample.Web",
            FilePath = "Controllers/ReportsController.cs",
            SymbolId = "Sample.Web.Controllers.ReportsController.Create",
            Span = new GraphSpan { StartLine = 10, EndLine = 30 },
            Props = new Dictionary<string, object>
            {
                ["route"] = "/api/reports",
                ["http_method"] = "POST"
            }
        };
        var request = new GraphNode
        {
            Id = "Sample.App.Commands.CreateReport",
            Type = "cqrs.request",
            Name = "CreateReport",
            Fqdn = "Sample.App.Commands.CreateReport",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Commands/CreateReport.cs",
            SymbolId = "Sample.App.Commands.CreateReport"
        };

        var document = new GraphDocument
        {
            Version = "0.0.1",
            Nodes = new[] { controller, request },
            Edges = new[]
            {
                new GraphEdge
                {
                    From = controller.Id,
                    To = request.Id,
                    Kind = "maps_to",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = controller.FilePath, Line = 20 }
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["variable"] = "command"
                    }
                }
            }
        };

        var flows = FlowBuilder.BuildFlows(document, FlowFilter.BuildPredicate(new[] { "Sample.Web.Controllers.*" }));

        Assert.Contains("[web] POST /api/reports", flows);
        Assert.Contains("maps_to CreateReport", flows);
    }

    [Fact]
    public void BuildFlows_ExpandsNotificationHandlers()
    {
        var controller = new GraphNode
        {
            Id = "Controller",
            Type = "endpoint.controller",
            Name = "Create",
            Fqdn = "Controller",
            Span = new GraphSpan { StartLine = 1, EndLine = 10 },
            Props = new Dictionary<string, object>
            {
                ["route"] = "/api/notifications",
                ["http_method"] = "POST"
            }
        };

        var request = new GraphNode
        {
            Id = "Request",
            Type = "cqrs.request",
            Name = "CreateThing",
            Fqdn = "Request",
            Span = new GraphSpan { StartLine = 5, EndLine = 6 }
        };

        var handler = new GraphNode
        {
            Id = "Handler",
            Type = "cqrs.handler",
            Name = "CreateThingHandler",
            Fqdn = "Handler",
            Span = new GraphSpan { StartLine = 10, EndLine = 30 }
        };

        var notification = new GraphNode
        {
            Id = "Notification",
            Type = "cqrs.notification",
            Name = "ThingCreated",
            Fqdn = "Notification",
            Span = new GraphSpan { StartLine = 15, EndLine = 20 }
        };

        var notificationHandler = new GraphNode
        {
            Id = "NotificationHandler",
            Type = "cqrs.notification_handler",
            Name = "ThingCreatedHandler",
            Fqdn = "NotificationHandler",
            Span = new GraphSpan { StartLine = 20, EndLine = 40 }
        };

        var repository = new GraphNode
        {
            Id = "Repository",
            Type = "app.repository",
            Name = "ThingRepository",
            Fqdn = "Repository",
            Span = new GraphSpan { StartLine = 50, EndLine = 60 }
        };

        var document = new GraphDocument
        {
            Version = "1",
            Nodes = new[] { controller, request, handler, notification, notificationHandler, repository },
            Edges = new[]
            {
                new GraphEdge
                {
                    From = controller.Id,
                    To = request.Id,
                    Kind = "maps_to",
                    Transform = new GraphTransform { Location = new GraphLocation { File = "Controller.cs", Line = 5 } }
                },
                new GraphEdge
                {
                    From = request.Id,
                    To = handler.Id,
                    Kind = "handled_by",
                    Transform = new GraphTransform { MethodSpan = handler.Span }
                },
                new GraphEdge
                {
                    From = handler.Id,
                    To = notification.Id,
                    Kind = "publishes_notification",
                    Transform = new GraphTransform { Location = new GraphLocation { File = "Handler.cs", Line = 25 } }
                },
                new GraphEdge
                {
                    From = notification.Id,
                    To = notificationHandler.Id,
                    Kind = "handled_by",
                    Transform = new GraphTransform { MethodSpan = notificationHandler.Span }
                },
                new GraphEdge
                {
                    From = notificationHandler.Id,
                    To = repository.Id,
                    Kind = "calls",
                    Props = new Dictionary<string, object> { ["method"] = "RecordAsync" },
                    Transform = new GraphTransform { Location = new GraphLocation { File = "NotificationHandler.cs", Line = 30 } }
                }
            }
        };

        var flows = FlowBuilder.BuildFlows(document, FlowFilter.BuildPredicate(new[] { "Controller" }));

        Assert.Contains("publishes_notification ThingCreated", flows);
        Assert.Contains("handled_by NotificationHandler.Handle", flows);
        Assert.Contains("calls ThingRepository.RecordAsync", flows);
    }

    [Fact]
    public void BuildFlows_StopsNotificationCycles()
    {
        var controller = new GraphNode
        {
            Id = "Controller",
            Type = "endpoint.controller",
            Name = "Create",
            Fqdn = "Controller",
            Span = new GraphSpan { StartLine = 1, EndLine = 10 },
            Props = new Dictionary<string, object>
            {
                ["route"] = "/api/notifications",
                ["http_method"] = "POST"
            }
        };

        var notification = new GraphNode
        {
            Id = "Notification",
            Type = "cqrs.notification",
            Name = "ThingCreated",
            Fqdn = "Notification",
            Span = new GraphSpan { StartLine = 5, EndLine = 6 }
        };

        var handler = new GraphNode
        {
            Id = "NotificationHandler",
            Type = "cqrs.notification_handler",
            Name = "ThingCreatedHandler",
            Fqdn = "NotificationHandler",
            Span = new GraphSpan { StartLine = 10, EndLine = 20 }
        };

        var document = new GraphDocument
        {
            Version = "1",
            Nodes = new[] { controller, notification, handler },
            Edges = new[]
            {
                new GraphEdge
                {
                    From = controller.Id,
                    To = notification.Id,
                    Kind = "publishes_notification",
                    Transform = new GraphTransform { Location = new GraphLocation { File = "Controller.cs", Line = 5 } }
                },
                new GraphEdge
                {
                    From = notification.Id,
                    To = handler.Id,
                    Kind = "handled_by",
                    Transform = new GraphTransform { MethodSpan = handler.Span }
                },
                new GraphEdge
                {
                    From = handler.Id,
                    To = notification.Id,
                    Kind = "publishes_notification",
                    Transform = new GraphTransform { Location = new GraphLocation { File = "Handler.cs", Line = 15 } }
                }
            }
        };

        var flows = FlowBuilder.BuildFlows(document, FlowFilter.BuildPredicate(new[] { "Controller" }));

        Assert.Contains("publishes_notification ThingCreated", flows);
        var occurrences = flows.Split("handled_by NotificationHandler.Handle", StringSplitOptions.None).Length - 1;
        Assert.Equal(1, occurrences);
    }

    [Fact]
    public void BuildFlows_FormatsCacheAndOptionsEdges()
    {
        var controller = new GraphNode
        {
            Id = "Controller",
            Type = "endpoint.controller",
            Name = "GetReport",
            Fqdn = "Controller",
            Span = new GraphSpan { StartLine = 1, EndLine = 20 },
            Props = new Dictionary<string, object>
            {
                ["route"] = "/api/reports/{id}",
                ["http_method"] = "GET"
            }
        };

        var cache = new GraphNode
        {
            Id = "Cache",
            Type = "cache.memory",
            Name = "MemoryCache",
            Fqdn = "Microsoft.Extensions.Caching.Memory.MemoryCache"
        };

        var options = new GraphNode
        {
            Id = "Options",
            Type = "config.options",
            Name = "ReportRetentionOptions",
            Fqdn = "Sample.App.Options.ReportRetentionOptions",
            Props = new Dictionary<string, object>
            {
                ["section"] = "Retention"
            }
        };

        var document = new GraphDocument
        {
            Version = "1",
            Nodes = new[] { controller, cache, options },
            Edges = new[]
            {
                new GraphEdge
                {
                    From = controller.Id,
                    To = cache.Id,
                    Kind = "uses_cache",
                    Props = new Dictionary<string, object>
                    {
                        ["method"] = "GetOrCreateAsync",
                        ["operation"] = "read_write",
                        ["key"] = "report:{id}"
                    },
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = "Controller.cs", Line = 12 }
                    }
                },
                new GraphEdge
                {
                    From = controller.Id,
                    To = options.Id,
                    Kind = "uses_options",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = "Controller.cs", Line = 15 }
                    }
                }
            }
        };

        var flows = FlowBuilder.BuildFlows(document, node => true);

        Assert.Contains("uses_cache MemoryCache.GetOrCreateAsync [read_write] (key=report:{id}) [L12]", flows);
        Assert.Contains("uses_options ReportRetentionOptions (Retention) [L15]", flows);
    }
}
