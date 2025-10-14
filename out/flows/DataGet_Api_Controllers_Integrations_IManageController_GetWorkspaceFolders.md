[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/workspaces/{workspaceId}/children  (DataGet.Api.Controllers.Integrations.IManageController.GetWorkspaceFolders)  [L208–L216] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageWorkspaceChildrenQuery [L215]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageWorkspaceChildrenQueryHandler.Handle [L22–L38]
      └─ uses_service IManageService
        └─ method GetWorkspaceFolders [L33]

