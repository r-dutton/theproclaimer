[web] POST /workflow/v1/jobs/  (Dataverse.Api.External.Controllers.v1.Workflow.JobsController.Create)  [L131–L140] status=201 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PostSerialisedModel [L137]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PostSerialisedModel [L12-L74]

