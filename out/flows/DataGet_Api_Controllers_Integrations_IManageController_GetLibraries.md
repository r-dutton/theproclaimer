[web] GET /api/imanage/customers/{customerId:int}/libraries  (DataGet.Api.Controllers.Integrations.IManageController.GetLibraries)  [L179–L184] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IIManageService (AddScoped)
    └─ method GetLibraries [L183]
      └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetLibraries [L12-L223]

