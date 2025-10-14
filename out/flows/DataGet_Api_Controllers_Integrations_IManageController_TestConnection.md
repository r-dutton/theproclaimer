[web] GET /api/imanage/api-information  (DataGet.Api.Controllers.Integrations.IManageController.TestConnection)  [L138–L146] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetIManageApiInformationQuery [L142]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetIManageApiInformationQueryHandler.Handle [L12–L27]
      └─ uses_service IManageService
        └─ method GetApiInformation [L23]

