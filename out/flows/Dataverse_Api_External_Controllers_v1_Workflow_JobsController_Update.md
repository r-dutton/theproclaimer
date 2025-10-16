[web] PUT /workflow/v1/jobs/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.Update)  [L150–L157] status=200 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PutSerialisedModel [L155]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PutSerialisedModel [L12-L74]

