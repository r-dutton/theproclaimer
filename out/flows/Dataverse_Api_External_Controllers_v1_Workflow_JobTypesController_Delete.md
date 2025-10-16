[web] DELETE /workflow/v1/job-types/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobTypesController.Delete)  [L164–L168] status=200 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method Delete [L167]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.Delete [L12-L74]

