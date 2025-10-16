[web] POST /workflow/v1/task-template-groups/  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplateGroupsController.Create)  [L100–L107] status=201 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method PostSerialisedModel [L105]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.PostSerialisedModel [L12-L74]

