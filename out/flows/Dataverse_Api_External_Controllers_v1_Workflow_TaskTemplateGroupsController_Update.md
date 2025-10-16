[web] PUT /workflow/v1/task-template-groups/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplateGroupsController.Update)  [L117–L124] status=200 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PutSerialisedModel [L122]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PutSerialisedModel [L12-L74]

