[web] GET /api/connection/{apiType}/{tenantId}/base-address  (DataGet.Api.Controllers.Connections.ConnectionController.GetBaseAddress)  [L181–L190] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetBaseAddress -> GetBaseAddressHandler [L185]
    └─ handled_by DataGet.Connections.App.Queries.GetBaseAddressHandler.Handle [L16–L30]
      └─ uses_service ConnectionContextProvider
        └─ method ConnectionContext [L27]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ requests 1
      └─ GetBaseAddress
    └─ handlers 1
      └─ GetBaseAddressHandler

