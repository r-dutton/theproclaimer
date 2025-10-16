[web] DELETE /workflow/v1/tasks/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TasksController.Delete)  [L140–L144] status=200 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method Delete [L143]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.Delete [L12-L74]

