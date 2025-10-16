[web] GET /api/imanage  (DataGet.Api.Controllers.Integrations.IManageController.GetApiInfo)  [L290–L296] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageApiInformationQuery -> GetIManageApiInformationQueryHandler [L292]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageApiInformationQueryHandler.Handle [L12–L27]
      └─ uses_service IManageService
        └─ method GetApiInformation [L23]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetApiInformation [L12-L223]
            └─ uses_client IManageApiClient [L33]
            └─ uses_service IManageApiClient
              └─ method GetApiInformation [L33]
                └─ implementation DataGet.Integrations.iManage.Api.Services.IManageApiClient.GetApiInformation [L17-L95]
  └─ impact_summary
    └─ clients 1
      └─ IManageApiClient
    └─ requests 1
      └─ GetIManageApiInformationQuery
    └─ handlers 1
      └─ GetIManageApiInformationQueryHandler

