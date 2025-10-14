[web] GET /api/imanage  (DataGet.Api.Controllers.Integrations.IManageController.GetApiInfo)  [L290–L296] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageApiInformationQuery [L292]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageApiInformationQueryHandler.Handle [L12–L27]
      └─ uses_service IManageService
        └─ method GetApiInformation [L23]

