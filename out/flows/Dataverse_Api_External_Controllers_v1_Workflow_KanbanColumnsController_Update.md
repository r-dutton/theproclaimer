[web] PUT /workflow/v1/kanban-columns/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.KanbanColumnsController.Update)  [L115–L122] status=200 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PutSerialisedModel [L120]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PutSerialisedModel [L12-L74]

