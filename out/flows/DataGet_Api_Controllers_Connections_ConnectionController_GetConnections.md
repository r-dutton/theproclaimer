[web] GET /api/connection/  (DataGet.Api.Controllers.Connections.ConnectionController.GetConnections)  [L34–L38] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetConnectionsQuery [L37]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Connections.App.Queries.GetConnectionsQueryHandler.Handle [L14–L78]
      └─ uses_service IControlledRepository<FileToken>
        └─ method ReadQuery [L40]
      └─ uses_service IControlledRepository<UserToken>
        └─ method ReadQuery [L32]
      └─ uses_service RequestContextProvider
        └─ method CurrentIdentification [L29]

