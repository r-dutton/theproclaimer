[web] GET /api/imanage/customers/{customerId:int}/folders  (DataGet.Api.Controllers.Integrations.IManageController.GetFolders)  [L228–L235] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageFolderQuery [L234]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageFolderQueryHandler.Handle [L19–L32]
      └─ uses_service IManageService
        └─ method GetFolders [L28]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetFolders [L12-L223]

