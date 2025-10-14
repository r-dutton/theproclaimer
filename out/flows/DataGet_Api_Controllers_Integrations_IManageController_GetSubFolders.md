[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/folders/{folderId}/children  (DataGet.Api.Controllers.Integrations.IManageController.GetSubFolders)  [L218–L226] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageSubFoldersQuery [L225]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageSubFoldersQueryHandler.Handle [L21–L37]
      └─ uses_service IManageService
        └─ method GetSubFolders [L32]

