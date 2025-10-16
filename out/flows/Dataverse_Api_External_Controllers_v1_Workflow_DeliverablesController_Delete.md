[web] DELETE /workflow/v1/deliverables/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Workflow.DeliverablesController.Delete)  [L165–L169] status=200 [auth=Authentication.WorkflowWrite]
  └─ uses_service IDataverseProxyService (AddScoped)
    └─ method Delete [L168]
      └─ implementation Dataverse.Api.External.Features.DataverseProxy.DataverseProxyService.Delete [L12-L74]

