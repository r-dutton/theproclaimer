[web] PUT /workflow/v1/deliverable-types/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverableTypeController.Update)  [L148–L155] status=200 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PutSerialisedModel [L153]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PutSerialisedModel [L12-L74]

