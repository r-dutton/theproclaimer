[web] GET /api/imanage/customers/{customerId:int}/folders  (DataGet.Api.Controllers.Integrations.IManageController.GetFolders)  [L228–L235] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageFolderQuery [L234]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageFolderQueryHandler.Handle [L19–L32]
      └─ uses_service IManageService
        └─ method GetFolders [L28]

