[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/customs/{clientFieldCustomName}/workspaces  (DataGet.Api.Controllers.Integrations.IManageController.GetWorkspaces)  [L197–L206] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageWorkspacesQuery [L205]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageWorkspacesQueryHandler.Handle [L25–L43]
      └─ uses_service IManageService
        └─ method GetWorkspaces [L36]

