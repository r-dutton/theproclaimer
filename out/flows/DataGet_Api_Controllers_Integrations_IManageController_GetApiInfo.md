[web] GET /api/imanage  (DataGet.Api.Controllers.Integrations.IManageController.GetApiInfo)  [L290–L296] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageApiInformationQuery [L292]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageApiInformationQueryHandler.Handle [L12–L27]
      └─ uses_service IManageService
        └─ method GetApiInformation [L23]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetApiInformation [L12-L223]

