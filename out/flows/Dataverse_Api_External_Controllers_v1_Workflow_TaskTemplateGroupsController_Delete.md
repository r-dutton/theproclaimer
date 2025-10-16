[web] DELETE /workflow/v1/task-template-groups/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplateGroupsController.Delete)  [L133–L137] status=200 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method Delete [L136]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.Delete [L12-L74]

