[web] DELETE /workflow/v1/jobs/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.Delete)  [L166–L170] status=200 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method Delete [L169]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.Delete [L12-L74]

