[web] DELETE /workflow/v1/kanban-columns/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.KanbanColumnsController.Delete)  [L131–L135] status=200 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method Delete [L134]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.Delete [L12-L74]

