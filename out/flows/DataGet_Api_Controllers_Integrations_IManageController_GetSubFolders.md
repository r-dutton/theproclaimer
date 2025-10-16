[web] GET /api/imanage/customers/{customerId:int}/libraries/{library}/folders/{folderId}/children  (DataGet.Api.Controllers.Integrations.IManageController.GetSubFolders)  [L218–L226] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageSubFoldersQuery [L225]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageSubFoldersQueryHandler.Handle [L21–L37]
      └─ uses_service IManageService
        └─ method GetSubFolders [L32]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetSubFolders [L12-L223]

