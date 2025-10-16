[web] GET /api/connection/  (DataGet.Api.Controllers.Connections.ConnectionController.GetConnections)  [L34–L38] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetConnectionsQuery [L37]
    └─ handled_by DataGet.Connections.App.Queries.GetConnectionsQueryHandler.Handle [L14–L78]
      └─ uses_service IControlledRepository<FileToken>
        └─ method ReadQuery [L40]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<UserToken>
        └─ method ReadQuery [L32]
          └─ ... (no implementation details available)
      └─ uses_service RequestContextProvider
        └─ method CurrentIdentification [L29]
          └─ implementation DataGet.ApplicationService.Services.RequestContextProvider.CurrentIdentification [L11-L76]

