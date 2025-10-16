using System;
using System.Collections.Generic;
using System.Linq;
using GraphKit.Graph;
using GraphKit.Outputs;
using GraphKit.Workspace;
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
                ["http_method"] = "POST",
                ["status_codes"] = new[] { 201 }
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
        Assert.Contains("status=201", flows);
    }

    [Fact]
    public void BuildFlows_AppendsImpactSummary()
    {
        var controller = new GraphNode
        {
            Id = "Controller",
            Type = "endpoint.controller",
            Name = "Get",
            Fqdn = "Sample.Web.Controllers.ThingController.Get",
            Assembly = "Sample.Web",
            Project = "Sample.Web",
            FilePath = "Controller.cs",
            SymbolId = "Controller",
            Span = new GraphSpan { StartLine = 10, EndLine = 40 },
            Props = new Dictionary<string, object>
            {
                ["route"] = "/api/thing",
                ["http_method"] = "GET"
            }
        };

        var repository = new GraphNode
        {
            Id = "Repository",
            Type = "app.repository",
            Name = "ThingRepository",
            Fqdn = "Sample.Data.ThingRepository",
            Assembly = "Sample.Data",
            Project = "Sample.Data",
            FilePath = "ThingRepository.cs",
            SymbolId = "Repository"
        };

        var entity = new GraphNode
        {
            Id = "Entity",
            Type = "ef.entity",
            Name = "ThingEntity",
            Fqdn = "Sample.Data.Entities.ThingEntity",
            Assembly = "Sample.Data",
            Project = "Sample.Data",
            FilePath = "ThingEntity.cs",
            SymbolId = "Entity"
        };

        var table = new GraphNode
        {
            Id = "Table",
            Type = "sql.table",
            Name = "dbo.Thing",
            Fqdn = "Sample.Data.Tables.dbo.Thing",
            Assembly = "Sample.Data",
            Project = "Sample.Data",
            FilePath = "ThingEntity.cs",
            SymbolId = "Table"
        };

        var client = new GraphNode
        {
            Id = "Client",
            Type = "app.client",
            Name = "DataGetClient",
            Fqdn = "Sample.App.DataGetClient",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "DataGetClient.cs",
            SymbolId = "Client"
        };

        var remoteEndpoint = new GraphNode
        {
            Id = "Remote",
            Type = "endpoint.api",
            Name = "DataGet.Api.Accounts",
            Fqdn = "DataGet.Api.Controllers.AccountsController.Get",
            Assembly = "Sample.Remote",
            Project = "Sample.Remote",
            FilePath = "AccountsController.cs",
            SymbolId = "Remote"
        };

        var request = new GraphNode
        {
            Id = "Request",
            Type = "cqrs.request",
            Name = "CreateThing",
            Fqdn = "Sample.App.CreateThing",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "CreateThing.cs",
            SymbolId = "Request"
        };

        var handler = new GraphNode
        {
            Id = "Handler",
            Type = "cqrs.handler",
            Name = "CreateThingHandler",
            Fqdn = "Sample.App.CreateThingHandler",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "CreateThingHandler.cs",
            SymbolId = "Handler",
            Span = new GraphSpan { StartLine = 50, EndLine = 80 }
        };

        var pipeline = new GraphNode
        {
            Id = "Pipeline",
            Type = "cqrs.pipeline_behavior",
            Name = "ValidationBehavior",
            Fqdn = "Sample.App.ValidationBehavior",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "ValidationBehavior.cs",
            SymbolId = "Pipeline"
        };

        var document = new GraphDocument
        {
            Version = "1",
            Nodes = new[]
            {
                controller,
                repository,
                entity,
                table,
                client,
                remoteEndpoint,
                request,
                handler,
                pipeline
            },
            Edges = new[]
            {
                new GraphEdge
                {
                    From = controller.Id,
                    To = repository.Id,
                    Kind = "calls",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = controller.FilePath, Line = 20 }
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["method"] = "SaveAsync"
                    }
                },
                new GraphEdge
                {
                    From = repository.Id,
                    To = entity.Id,
                    Kind = "writes_to",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = repository.FilePath!, Line = 30 }
                    }
                },
                new GraphEdge
                {
                    From = entity.Id,
                    To = table.Id,
                    Kind = "writes_to",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = entity.FilePath!, Line = 35 }
                    }
                },
                new GraphEdge
                {
                    From = controller.Id,
                    To = client.Id,
                    Kind = "uses_client",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = controller.FilePath!, Line = 25 }
                    }
                },
                new GraphEdge
                {
                    From = client.Id,
                    To = remoteEndpoint.Id,
                    Kind = "calls",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = controller.FilePath!, Line = 25 }
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["client_method"] = "GetAccounts",
                        ["verb"] = "GET",
                        ["route"] = "/api/v1/accounts/{id:int}",
                        ["base_url"] = "https://dataget.example.com/api/v1",
                        ["target_service"] = "DataGet.Api"
                    }
                },
                new GraphEdge
                {
                    From = controller.Id,
                    To = request.Id,
                    Kind = "sends_request",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = controller.FilePath!, Line = 30 }
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["response_type"] = "CreateThingResponse"
                    }
                },
                new GraphEdge
                {
                    From = request.Id,
                    To = handler.Id,
                    Kind = "handled_by",
                    Source = "analysis"
                },
                new GraphEdge
                {
                    From = request.Id,
                    To = pipeline.Id,
                    Kind = "processed_by",
                    Source = "analysis",
                    Props = new Dictionary<string, object>
                    {
                        ["stage"] = "Pre",
                        ["response_type"] = "CreateThingResponse"
                    }
                }
            }
        };

        var flows = FlowBuilder.BuildFlows(document, FlowFilter.BuildPredicate(new[] { "Sample.Web.Controllers.*" }));
        Assert.Contains("impact_summary", flows);
        Assert.Contains("remote_endpoints 1 (calls=1)", flows);
        Assert.Contains("GET /api/accounts/{id} -> DataGet.Api via DataGetClient", flows);
        Assert.Contains("repositories 1 (writes=1, reads=0)", flows);
        Assert.Contains("ThingRepository writes=1 reads=0", flows);
        Assert.Contains("entities 1 (writes=1, reads=0)", flows);
        Assert.Contains("clients 1", flows);
        Assert.Contains("pipeline_behaviors 1", flows);
        Assert.Contains("requests 1", flows);
        Assert.Contains("handlers 1", flows);
    }

    [Fact]
    public void BuildFlows_HeaderIncludesMultipleStatusCodes()
    {
        var controller = new GraphNode
        {
            Id = "Sample.Web.Controllers.ReportsController.Get",
            Type = "endpoint.controller",
            Name = "Get",
            Fqdn = "Sample.Web.Controllers.ReportsController.Get",
            Assembly = "Sample.Web",
            Project = "Sample.Web",
            FilePath = "Controllers/ReportsController.cs",
            SymbolId = "Sample.Web.Controllers.ReportsController.Get",
            Span = new GraphSpan { StartLine = 5, EndLine = 15 },
            Props = new Dictionary<string, object>
            {
                ["route"] = "/api/reports/{id}",
                ["http_method"] = "GET",
                ["status_codes"] = new[] { 200, 404 }
            }
        };

        var document = new GraphDocument
        {
            Version = "1",
            Nodes = new[] { controller },
            Edges = System.Array.Empty<GraphEdge>()
        };

        var flows = FlowBuilder.BuildFlows(document, FlowFilter.BuildPredicate(new[] { "Sample.Web.Controllers.*" }));

        Assert.Contains("status=200,404", flows);
    }

    [Fact]
    public void BuildFlows_ResolvesServiceContractsViaRequestsWhenNoImplementation()
    {
        var controller = new GraphNode
        {
            Id = "Sample.Web.Controllers.LedgerReportController.Get",
            Type = "endpoint.controller",
            Name = "Get",
            Fqdn = "Sample.Web.Controllers.LedgerReportController.Get",
            Assembly = "Sample.Web",
            Project = "Sample.Web",
            FilePath = "Controllers/LedgerReportController.cs",
            SymbolId = "Sample.Web.Controllers.LedgerReportController.Get",
            Span = new GraphSpan { StartLine = 10, EndLine = 40 },
            Props = new Dictionary<string, object>
            {
                ["route"] = "/api/ledger/report",
                ["http_method"] = "GET"
            }
        };

        var service = new GraphNode
        {
            Id = "Service",
            Type = "app.service_contract",
            Name = "GetTrialBalanceForDatesQuery",
            Fqdn = "Sample.App.Queries.GetTrialBalanceForDatesQuery",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = string.Empty,
            SymbolId = "Service"
        };

        var request = new GraphNode
        {
            Id = "Request",
            Type = "cqrs.request",
            Name = "GetTrialBalanceForDatesQuery",
            Fqdn = "Sample.App.Queries.GetTrialBalanceForDatesQuery",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Queries/GetTrialBalanceForDatesQuery.cs",
            SymbolId = "Request"
        };

        var handler = new GraphNode
        {
            Id = "Handler",
            Type = "cqrs.handler",
            Name = "GetTrialBalanceForDatesQueryHandler",
            Fqdn = "Sample.App.Queries.GetTrialBalanceForDatesQueryHandler",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Queries/GetTrialBalanceForDatesQuery.cs",
            SymbolId = "Handler",
            Span = new GraphSpan { StartLine = 50, EndLine = 150 }
        };

        var document = new GraphDocument
        {
            Version = "1",
            Nodes = new[] { controller, service, request, handler },
            Edges = new[]
            {
                new GraphEdge
                {
                    From = controller.Id,
                    To = service.Id,
                    Kind = "uses_service",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = controller.FilePath!, Line = 25 }
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["method"] = "Execute"
                    }
                },
                new GraphEdge
                {
                    From = request.Id,
                    To = handler.Id,
                    Kind = "handled_by",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        MethodSpan = handler.Span
                    }
                }
            }
        };

        var flows = FlowBuilder.BuildFlows(document, FlowFilter.BuildPredicate(new[] { "Sample.Web.Controllers.*" }));
        Assert.Contains("resolves_request Sample.App.Queries.GetTrialBalanceForDatesQuery.Execute", flows);
        Assert.Contains("handled_by Sample.App.Queries.GetTrialBalanceForDatesQueryHandler.Handle", flows);
        Assert.DoesNotContain("... (no implementation details available)", flows);
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
            Assembly = "Sample.Web",
            Project = "Sample.Web",
            FilePath = "Controller.cs",
            SymbolId = "Controller",
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
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Request.cs",
            SymbolId = "Request",
            Span = new GraphSpan { StartLine = 5, EndLine = 6 }
        };

        var handler = new GraphNode
        {
            Id = "Handler",
            Type = "cqrs.handler",
            Name = "CreateThingHandler",
            Fqdn = "Handler",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Handler.cs",
            SymbolId = "Handler",
            Span = new GraphSpan { StartLine = 10, EndLine = 30 }
        };

        var notification = new GraphNode
        {
            Id = "Notification",
            Type = "cqrs.notification",
            Name = "ThingCreated",
            Fqdn = "Notification",
            Assembly = "Sample.Msg",
            Project = "Sample.Msg",
            FilePath = "Notification.cs",
            SymbolId = "Notification",
            Span = new GraphSpan { StartLine = 15, EndLine = 20 }
        };

        var notificationHandler = new GraphNode
        {
            Id = "NotificationHandler",
            Type = "cqrs.notification_handler",
            Name = "ThingCreatedHandler",
            Fqdn = "NotificationHandler",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "NotificationHandler.cs",
            SymbolId = "NotificationHandler",
            Span = new GraphSpan { StartLine = 20, EndLine = 40 }
        };

        var repository = new GraphNode
        {
            Id = "Repository",
            Type = "app.repository",
            Name = "ThingRepository",
            Fqdn = "Repository",
            Assembly = "Sample.Data",
            Project = "Sample.Data",
            FilePath = "Repository.cs",
            SymbolId = "Repository",
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
                    Source = "analysis",
                    Transform = new GraphTransform { Location = new GraphLocation { File = "Controller.cs", Line = 5 } }
                },
                new GraphEdge
                {
                    From = request.Id,
                    To = handler.Id,
                    Kind = "handled_by",
                    Source = "analysis",
                    Transform = new GraphTransform { MethodSpan = handler.Span }
                },
                new GraphEdge
                {
                    From = handler.Id,
                    To = notification.Id,
                    Kind = "publishes_notification",
                    Source = "analysis",
                    Transform = new GraphTransform { Location = new GraphLocation { File = "Handler.cs", Line = 25 } }
                },
                new GraphEdge
                {
                    From = notification.Id,
                    To = notificationHandler.Id,
                    Kind = "handled_by",
                    Source = "analysis",
                    Transform = new GraphTransform { MethodSpan = notificationHandler.Span }
                },
                new GraphEdge
                {
                    From = notificationHandler.Id,
                    To = repository.Id,
                    Kind = "calls",
                    Source = "analysis",
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
    public void BuildFlows_ShowsReturnFlowFromHandlers()
    {
        var controller = new GraphNode
        {
            Id = "Controller",
            Type = "endpoint.controller",
            Name = "Update",
            Fqdn = "Controller",
            Assembly = "Sample.Web",
            Project = "Sample.Web",
            FilePath = "Controller.cs",
            SymbolId = "Controller",
            Span = new GraphSpan { StartLine = 5, EndLine = 40 },
            Props = new Dictionary<string, object>
            {
                ["route"] = "/api/reports/{id}",
                ["http_method"] = "PUT"
            }
        };

        var request = new GraphNode
        {
            Id = "Request",
            Type = "cqrs.request",
            Name = "UpdateReport",
            Fqdn = "Request",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Request.cs",
            SymbolId = "Request"
        };

        var handler = new GraphNode
        {
            Id = "Handler",
            Type = "cqrs.handler",
            Name = "UpdateReportHandler",
            Fqdn = "Handler",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Handler.cs",
            SymbolId = "Handler",
            Span = new GraphSpan { StartLine = 10, EndLine = 30 }
        };

        var repository = new GraphNode
        {
            Id = "Repository",
            Type = "app.repository",
            Name = "ReportRepository",
            Fqdn = "Repository",
            Assembly = "Sample.Data",
            Project = "Sample.Data",
            FilePath = "Repository.cs",
            SymbolId = "Repository"
        };

        var entity = new GraphNode
        {
            Id = "Entity",
            Type = "ef.entity",
            Name = "Report",
            Fqdn = "Report",
            Assembly = "Sample.Data",
            Project = "Sample.Data",
            FilePath = "Report.cs",
            SymbolId = "Report"
        };

        var table = new GraphNode
        {
            Id = "Table",
            Type = "db.table",
            Name = "Reports",
            Fqdn = "Reports",
            Assembly = "Sample.Data",
            Project = "Sample.Data",
            FilePath = "Reports.sql",
            SymbolId = "Reports"
        };

        var dto = new GraphNode
        {
            Id = "Dto",
            Type = "dto",
            Name = "ReportDto",
            Fqdn = "Dto",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Dto.cs",
            SymbolId = "Dto"
        };

        var profile = new GraphNode
        {
            Id = "Profile",
            Type = "mapping.automapper.profile",
            Name = "ReportProfile",
            Fqdn = "Profile",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Profile.cs",
            SymbolId = "Profile"
        };

        var map = new GraphNode
        {
            Id = "Map",
            Type = "mapping.automapper.map",
            Name = "Report->ReportDto",
            Fqdn = "Profile.Map",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Profile.cs",
            SymbolId = "Map",
            Span = new GraphSpan { StartLine = 20, EndLine = 20 },
            Props = new Dictionary<string, object>
            {
                ["source_type"] = "Report",
                ["destination_type"] = "ReportDto"
            }
        };

        var document = new GraphDocument
        {
            Version = "1",
            Nodes = new[] { controller, request, handler, repository, entity, table, dto, profile, map },
            Edges = new[]
            {
                new GraphEdge
                {
                    From = controller.Id,
                    To = request.Id,
                    Kind = "sends_request",
                    Source = "analysis",
                    Transform = new GraphTransform { Location = new GraphLocation { File = controller.FilePath, Line = 12 } }
                },
                new GraphEdge
                {
                    From = request.Id,
                    To = handler.Id,
                    Kind = "handled_by",
                    Source = "analysis",
                    Transform = new GraphTransform { MethodSpan = handler.Span }
                },
                new GraphEdge
                {
                    From = handler.Id,
                    To = repository.Id,
                    Kind = "calls",
                    Source = "analysis",
                    Props = new Dictionary<string, object> { ["method"] = "UpdateAsync" },
                    Transform = new GraphTransform { Location = new GraphLocation { File = handler.FilePath, Line = 18 } }
                },
                new GraphEdge
                {
                    From = repository.Id,
                    To = entity.Id,
                    Kind = "writes_to",
                    Source = "analysis",
                    Transform = new GraphTransform { Location = new GraphLocation { File = repository.FilePath, Line = 25 } }
                },
                new GraphEdge
                {
                    From = entity.Id,
                    To = table.Id,
                    Kind = "writes_to",
                    Source = "analysis",
                    Transform = new GraphTransform { Location = new GraphLocation { File = repository.FilePath, Line = 25 } }
                },
                new GraphEdge
                {
                    From = handler.Id,
                    To = dto.Id,
                    Kind = "maps_to",
                    Source = "analysis",
                    Props = new Dictionary<string, object>
                    {
                        ["source_type"] = "Report",
                        ["destination_type"] = "ReportDto"
                    },
                    Transform = new GraphTransform { Location = new GraphLocation { File = handler.FilePath, Line = 22 } }
                },
                new GraphEdge
                {
                    From = controller.Id,
                    To = dto.Id,
                    Kind = "returns",
                    Source = "analysis",
                    Props = new Dictionary<string, object>
                    {
                        ["response_type"] = "ReportDto",
                        ["variable"] = "result",
                        ["kind"] = "return"
                    },
                    Transform = new GraphTransform { Location = new GraphLocation { File = controller.FilePath, Line = 30 } }
                },
                new GraphEdge
                {
                    From = map.Id,
                    To = dto.Id,
                    Kind = "maps_to",
                    Source = "analysis",
                    Props = new Dictionary<string, object>
                    {
                        ["source_type"] = "Report",
                        ["destination_type"] = "ReportDto"
                    },
                    Transform = new GraphTransform { Location = new GraphLocation { File = map.FilePath, Line = 20 } }
                },
                new GraphEdge
                {
                    From = map.Id,
                    To = profile.Id,
                    Kind = "generated_from",
                    Source = "analysis",
                    Transform = new GraphTransform { Location = new GraphLocation { File = map.FilePath, Line = 20 } }
                }
            }
        };

        var flows = FlowBuilder.BuildFlows(document, node => true);

        Assert.Contains("returns ReportDto (var result) [L30] [return]", flows);
        Assert.Contains("handled_by Handler.Handle", flows);
        Assert.Contains("writes_to Report", flows);
        Assert.Contains("writes_to Reports", flows);
    }

    [Fact]
    public void BuildFlows_IncludesRepositoryQueries()
    {
        var controller = new GraphNode
        {
            Id = "Controller",
            Type = "endpoint.controller",
            Name = "Get",
            Fqdn = "Controller",
            Assembly = "Sample.Web",
            Project = "Sample.Web",
            FilePath = "Controller.cs",
            SymbolId = "Controller",
            Span = new GraphSpan { StartLine = 1, EndLine = 10 },
            Props = new Dictionary<string, object>
            {
                ["route"] = "/api/things",
                ["http_method"] = "GET"
            }
        };

        var request = new GraphNode
        {
            Id = "Request",
            Type = "cqrs.request",
            Name = "ListThings",
            Fqdn = "Request",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Request.cs",
            SymbolId = "Request"
        };

        var handler = new GraphNode
        {
            Id = "Handler",
            Type = "cqrs.handler",
            Name = "ListThingsHandler",
            Fqdn = "Handler",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Handler.cs",
            SymbolId = "Handler",
            Span = new GraphSpan { StartLine = 5, EndLine = 20 }
        };

        var repository = new GraphNode
        {
            Id = "Repository",
            Type = "app.repository",
            Name = "ThingRepository",
            Fqdn = "Repository",
            Assembly = "Sample.Data",
            Project = "Sample.Data",
            FilePath = "Repository.cs",
            SymbolId = "Repository"
        };

        var entity = new GraphNode
        {
            Id = "Entity",
            Type = "ef.entity",
            Name = "Thing",
            Fqdn = "Thing",
            Assembly = "Sample.Data",
            Project = "Sample.Data",
            FilePath = "Thing.cs",
            SymbolId = "Thing"
        };

        var table = new GraphNode
        {
            Id = "Table",
            Type = "db.table",
            Name = "Things",
            Fqdn = "Things",
            Assembly = "Sample.Data",
            Project = "Sample.Data",
            FilePath = "Things.sql",
            SymbolId = "Things"
        };

        var document = new GraphDocument
        {
            Version = "1",
            Nodes = new[] { controller, request, handler, repository, entity, table },
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
                        Location = new GraphLocation { File = controller.FilePath, Line = 4 }
                    }
                },
                new GraphEdge
                {
                    From = request.Id,
                    To = handler.Id,
                    Kind = "handled_by",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        MethodSpan = handler.Span
                    }
                },
                new GraphEdge
                {
                    From = handler.Id,
                    To = repository.Id,
                    Kind = "calls",
                    Source = "analysis",
                    Props = new Dictionary<string, object> { ["method"] = "ListAsync" },
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = handler.FilePath, Line = 12 }
                    }
                },
                new GraphEdge
                {
                    From = repository.Id,
                    To = entity.Id,
                    Kind = "queries",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = repository.FilePath, Line = 18 }
                    }
                },
                new GraphEdge
                {
                    From = entity.Id,
                    To = table.Id,
                    Kind = "queries",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = repository.FilePath, Line = 18 }
                    }
                }
            }
        };

        var flows = FlowBuilder.BuildFlows(document, node => true);

        Assert.Contains("queries Thing", flows);
        Assert.Contains("queries Things", flows);
    }

    [Fact]
    public void BuildFlows_ShowsProducedEvents()
    {
        var controller = new GraphNode
        {
            Id = "Controller",
            Type = "endpoint.controller",
            Name = "Post",
            Fqdn = "Controller",
            Assembly = "Sample.Web",
            Project = "Sample.Web",
            FilePath = "Controller.cs",
            SymbolId = "Controller",
            Span = new GraphSpan { StartLine = 1, EndLine = 5 },
            Props = new Dictionary<string, object>
            {
                ["route"] = "/api/things",
                ["http_method"] = "POST"
            }
        };

        var request = new GraphNode
        {
            Id = "Request",
            Type = "cqrs.request",
            Name = "CreateThing",
            Fqdn = "Request",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Request.cs",
            SymbolId = "Request"
        };

        var handler = new GraphNode
        {
            Id = "Handler",
            Type = "cqrs.handler",
            Name = "CreateThingHandler",
            Fqdn = "Handler",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Handler.cs",
            SymbolId = "Handler",
            Span = new GraphSpan { StartLine = 10, EndLine = 20 }
        };

        var publisher = new GraphNode
        {
            Id = "Publisher",
            Type = "message.publisher",
            Name = "ThingPublisher",
            Fqdn = "Publisher",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Publisher.cs",
            SymbolId = "Publisher",
            Span = new GraphSpan { StartLine = 2, EndLine = 8 },
            Props = new Dictionary<string, object>
            {
                ["queue"] = "things",
                ["subject"] = "thing.created"
            }
        };

        var contract = new GraphNode
        {
            Id = "Contract",
            Type = "message.contract",
            Name = "ThingCreated",
            Fqdn = "ThingCreated",
            Assembly = "Sample.Msg",
            Project = "Sample.Msg",
            FilePath = "ThingCreated.cs",
            SymbolId = "ThingCreated"
        };

        var document = new GraphDocument
        {
            Version = "1",
            Nodes = new[] { controller, request, handler, publisher, contract },
            Edges = new[]
            {
                new GraphEdge
                {
                    From = controller.Id,
                    To = request.Id,
                    Kind = "maps_to",
                    Source = "analysis"
                },
                new GraphEdge
                {
                    From = request.Id,
                    To = handler.Id,
                    Kind = "handled_by",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        MethodSpan = handler.Span
                    }
                },
                new GraphEdge
                {
                    From = handler.Id,
                    To = publisher.Id,
                    Kind = "publishes",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = handler.FilePath, Line = 15 }
                    }
                },
                new GraphEdge
                {
                    From = publisher.Id,
                    To = contract.Id,
                    Kind = "produces_event",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = publisher.FilePath, Line = 4 }
                    }
                }
            }
        };

        var flows = FlowBuilder.BuildFlows(document, node => true);

        Assert.Contains("publishes ThingPublisher (queue=things, subject=thing.created)", flows);
        Assert.Contains("produces_event ThingCreated", flows);
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
            Assembly = "Sample.Web",
            Project = "Sample.Web",
            FilePath = "Controller.cs",
            SymbolId = "Controller",
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
            Assembly = "Sample.Msg",
            Project = "Sample.Msg",
            FilePath = "Notification.cs",
            SymbolId = "Notification",
            Span = new GraphSpan { StartLine = 5, EndLine = 6 }
        };

        var handler = new GraphNode
        {
            Id = "NotificationHandler",
            Type = "cqrs.notification_handler",
            Name = "ThingCreatedHandler",
            Fqdn = "NotificationHandler",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "NotificationHandler.cs",
            SymbolId = "NotificationHandler",
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
                    Source = "analysis",
                    Transform = new GraphTransform { Location = new GraphLocation { File = "Controller.cs", Line = 5 } }
                },
                new GraphEdge
                {
                    From = notification.Id,
                    To = handler.Id,
                    Kind = "handled_by",
                    Source = "analysis",
                    Transform = new GraphTransform { MethodSpan = handler.Span }
                },
                new GraphEdge
                {
                    From = handler.Id,
                    To = notification.Id,
                    Kind = "publishes_notification",
                    Source = "analysis",
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
            Assembly = "Sample.Web",
            Project = "Sample.Web",
            FilePath = "Controller.cs",
            SymbolId = "Controller",
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
            Fqdn = "Microsoft.Extensions.Caching.Memory.MemoryCache",
            Assembly = "Microsoft.Extensions.Caching.Memory",
            Project = "Microsoft.Extensions.Caching.Memory",
            FilePath = "MemoryCache.cs",
            SymbolId = "Microsoft.Extensions.Caching.Memory.MemoryCache"
        };

        var options = new GraphNode
        {
            Id = "Options",
            Type = "config.options",
            Name = "ReportRetentionOptions",
            Fqdn = "Sample.App.Options.ReportRetentionOptions",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Options/ReportRetentionOptions.cs",
            SymbolId = "Sample.App.Options.ReportRetentionOptions",
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
                    Source = "analysis",
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
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = "Controller.cs", Line = 15 }
                    }
                }
            }
        };

        var flows = FlowBuilder.BuildFlows(document, node => true);

    // Cache usage is now rendered on a single line including method / operation / key
    Assert.Contains("uses_cache MemoryCache.GetOrCreateAsync [read_write] (key=report:{id}) [L12]", flows);
        Assert.Contains("uses_options ReportRetentionOptions (Retention) [L15]", flows);
    }

    [Fact]
    public void BuildFlows_FollowsTargetServiceEndpoints()
    {
        var sourceController = new GraphNode
        {
            Id = "SourceController",
            Type = "endpoint.controller",
            Name = "Post",
            Fqdn = "Source.Controller.Post",
            Assembly = "Source.App",
            Project = "Source.App",
            FilePath = "Controllers/SourceController.cs",
            SymbolId = "SourceController.Post",
            Span = new GraphSpan { StartLine = 10, EndLine = 30 },
            Props = new Dictionary<string, object>
            {
                ["route"] = "/api/source",
                ["http_method"] = "POST"
            }
        };

        var sourceRequest = new GraphNode
        {
            Id = "SourceRequest",
            Type = "cqrs.request",
            Name = "CreateSource",
            Fqdn = "Source.Requests.CreateSource",
            Assembly = "Source.App",
            Project = "Source.App",
            FilePath = "Requests/CreateSource.cs",
            SymbolId = "SourceRequest"
        };

        var sourceHandler = new GraphNode
        {
            Id = "SourceHandler",
            Type = "cqrs.handler",
            Name = "CreateSourceHandler",
            Fqdn = "Source.Handlers.CreateSourceHandler",
            Assembly = "Source.App",
            Project = "Source.App",
            FilePath = "Handlers/CreateSourceHandler.cs",
            SymbolId = "SourceHandler",
            Span = new GraphSpan { StartLine = 15, EndLine = 40 }
        };

        var httpClient = new GraphNode
        {
            Id = "HttpClient",
            Type = "http.client",
            Name = "TargetClient",
            Fqdn = "Source.Http.TargetClient",
            Assembly = "Source.Infrastructure",
            Project = "Source.Infrastructure",
            FilePath = "Http/TargetClient.cs",
            SymbolId = "TargetClient"
        };

        var targetController = new GraphNode
        {
            Id = "TargetController",
            Type = "endpoint.controller",
            Name = "Get",
            Fqdn = "Target.Service.Controllers.TargetController.Get",
            Assembly = "Target.Service",
            Project = "Target.Service",
            FilePath = "Controllers/TargetController.cs",
            SymbolId = "TargetController.Get",
            Span = new GraphSpan { StartLine = 5, EndLine = 12 },
            Props = new Dictionary<string, object>
            {
                ["route"] = "/api/target",
                ["http_method"] = "GET"
            }
        };

        var targetRequest = new GraphNode
        {
            Id = "TargetRequest",
            Type = "cqrs.request",
            Name = "GetTarget",
            Fqdn = "Target.Requests.GetTarget",
            Assembly = "Target.App",
            Project = "Target.App",
            FilePath = "Requests/GetTarget.cs",
            SymbolId = "TargetRequest"
        };

        var targetHandler = new GraphNode
        {
            Id = "TargetHandler",
            Type = "cqrs.handler",
            Name = "GetTargetHandler",
            Fqdn = "Target.Handlers.GetTargetHandler",
            Assembly = "Target.App",
            Project = "Target.App",
            FilePath = "Handlers/GetTargetHandler.cs",
            SymbolId = "TargetHandler",
            Span = new GraphSpan { StartLine = 20, EndLine = 45 }
        };

        var document = new GraphDocument
        {
            Version = "1",
            Nodes = new[]
            {
                sourceController,
                sourceRequest,
                sourceHandler,
                httpClient,
                targetController,
                targetRequest,
                targetHandler
            },
            Edges = new[]
            {
                new GraphEdge
                {
                    From = sourceController.Id,
                    To = sourceRequest.Id,
                    Kind = "maps_to",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = sourceController.FilePath, Line = 18 }
                    }
                },
                new GraphEdge
                {
                    From = sourceRequest.Id,
                    To = sourceHandler.Id,
                    Kind = "handled_by",
                    Source = "analysis",
                    Transform = new GraphTransform { MethodSpan = sourceHandler.Span }
                },
                new GraphEdge
                {
                    From = sourceHandler.Id,
                    To = httpClient.Id,
                    Kind = "uses_client",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = sourceHandler.FilePath, Line = 27 }
                    }
                },
                new GraphEdge
                {
                    From = httpClient.Id,
                    To = targetController.Id,
                    Kind = "calls",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = httpClient.FilePath, Line = 15 }
                    },
                    Props = new Dictionary<string, object>
                    {
                        ["verb"] = "GET",
                        ["route"] = "/api/target",
                        ["target_service"] = "TargetService"
                    }
                },
                new GraphEdge
                {
                    From = targetController.Id,
                    To = targetRequest.Id,
                    Kind = "maps_to",
                    Source = "analysis",
                    Transform = new GraphTransform
                    {
                        Location = new GraphLocation { File = targetController.FilePath, Line = 7 }
                    }
                },
                new GraphEdge
                {
                    From = targetRequest.Id,
                    To = targetHandler.Id,
                    Kind = "handled_by",
                    Source = "analysis",
                    Transform = new GraphTransform { MethodSpan = targetHandler.Span }
                }
            }
        };

        var workspaceIndex = new FlowWorkspaceIndex(new Dictionary<string, IEnumerable<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["TargetService"] = new[] { "Target.Service" }
        });

        var flows = FlowBuilder.BuildFlows(document, node => string.Equals(node.Id, sourceController.Id, StringComparison.Ordinal), workspaceIndex);

        Assert.Contains("target_service TargetService", flows);
        Assert.Contains("Target.Service.Controllers.TargetController.Get", flows);
        Assert.Contains("handled_by Target.Handlers.GetTargetHandler.Handle", flows);
    }

    [Fact]
    public void BuildFlows_DeduplicatesRepeatedCallLines()
    {
        var controller = new GraphNode
        {
            Id = "Sample.Web.Controllers.VatController.Get",
            Type = "endpoint.controller",
            Name = "Get",
            Fqdn = "Sample.Web.Controllers.VatController.Get",
            Assembly = "Sample.Web",
            Project = "Sample.Web",
            FilePath = "Controllers/VatController.cs",
            SymbolId = "Sample.Web.Controllers.VatController.Get",
            Span = new GraphSpan { StartLine = 10, EndLine = 40 },
            Props = new Dictionary<string, object>
            {
                ["route"] = "/api/vat",
                ["http_method"] = "GET"
            }
        };

        var client = new GraphNode
        {
            Id = "Sample.App.Services.VatClient",
            Type = "app.service",
            Name = "VatClient",
            Fqdn = "Sample.App.Services.VatClient",
            Assembly = "Sample.App",
            Project = "Sample.App",
            FilePath = "Services/VatClient.cs",
            SymbolId = "Sample.App.Services.VatClient"
        };

        GraphEdge CreateCallEdge() => new()
        {
            From = controller.Id,
            To = client.Id,
            Kind = "calls",
            Source = "analysis",
            Transform = new GraphTransform
            {
                Location = new GraphLocation { File = controller.FilePath, Line = 25 },
                IlRange = new GraphIlRange { StartOffset = 10, EndOffset = 30 }
            },
            Props = new Dictionary<string, object>
            {
                ["method"] = "GetVatObligations"
            }
        };

        var document = new GraphDocument
        {
            Version = "1",
            Nodes = new[] { controller, client },
            Edges = new[]
            {
                CreateCallEdge(),
                CreateCallEdge()
            }
        };

        var flows = FlowBuilder.BuildFlows(document, FlowFilter.BuildPredicate(new[] { "Sample.Web.Controllers.*" }));
        var occurrences = flows
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Count(line => line.Contains("calls VatClient.GetVatObligations", StringComparison.Ordinal));

        Assert.Equal(1, occurrences);
    }
}
