[web] PUT /workflow/v1/job-statuses/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.JobStatusController.Update)  [L148–L155] status=200 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PutSerialisedModel [L153]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PutSerialisedModel [L12-L74]

