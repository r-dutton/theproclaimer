[web] PUT /workflow/v1/tasks/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TasksController.Update)  [L124–L131] status=200 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PutSerialisedModel [L129]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PutSerialisedModel [L12-L74]

