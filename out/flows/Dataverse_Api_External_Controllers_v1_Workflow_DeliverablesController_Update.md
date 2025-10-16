[web] PUT /workflow/v1/deliverables/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.Update)  [L149–L156] status=200 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PutSerialisedModel [L154]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PutSerialisedModel [L12-L74]

