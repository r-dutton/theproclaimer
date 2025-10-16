[web] PUT /workflow/v1/task-templates/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplatesController.Update)  [L120–L127] status=200 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PutSerialisedModel [L125]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PutSerialisedModel [L12-L74]

