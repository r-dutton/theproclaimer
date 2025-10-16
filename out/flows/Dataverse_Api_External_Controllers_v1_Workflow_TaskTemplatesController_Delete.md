[web] DELETE /workflow/v1/task-templates/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplatesController.Delete)  [L136–L140] status=200 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method Delete [L139]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.Delete [L12-L74]

