[web] GET /api/connection/{apiType}/{tenantId}/base-address  (DataGet.Api.Controllers.Connections.ConnectionController.GetBaseAddress)  [L181–L190] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetBaseAddress [L185]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Connections.App.Queries.GetBaseAddressHandler.Handle [L16–L30]
      └─ uses_service ConnectionContextProvider
        └─ method ConnectionContext [L27]

