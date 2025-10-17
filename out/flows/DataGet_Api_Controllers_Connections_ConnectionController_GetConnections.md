[web] GET /api/connection/  (DataGet.Api.Controllers.Connections.ConnectionController.GetConnections)  [L34–L38] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetConnectionsQuery -> GetConnectionsQueryHandler [L37]
    └─ handled_by DataGet.Connections.App.Queries.GetConnectionsQueryHandler.Handle [L14–L78]
      └─ calls FileTokenRepository.ReadQuery [L40]
      └─ calls UserTokenRepository.ReadQuery [L32]
      └─ uses_service RequestContextProvider
        └─ method CurrentIdentification [L29]
          └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.CurrentIdentification
  └─ impact_summary
    └─ requests 1
      └─ GetConnectionsQuery
    └─ handlers 1
      └─ GetConnectionsQueryHandler

