[web] GET /api/imanage/api-information  (DataGet.Api.Controllers.Integrations.IManageController.TestConnection)  [L138–L146] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageApiInformationQuery [L142]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageApiInformationQueryHandler.Handle [L12–L27]
      └─ uses_service IManageService
        └─ method GetApiInformation [L23]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetApiInformation [L12-L223]

