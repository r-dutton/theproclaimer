[web] GET /api/connection/{apiType}  (DataGet.Api.Controllers.Connections.ConnectionController.GetConnection)  [L40–L44] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetConnectionQuery -> GetConnectionQueryHandler [L43]
    └─ handled_by DataGet.Connections.App.Queries.GetConnectionQueryHandler.Handle [L20–L48]
      └─ calls ConnectionRepository.ReadQuery [L40]
      └─ maps_to ConnectionDto [L40]
      └─ uses_service RequestContextProvider
        └─ method CurrentContext [L38]
          └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.CurrentContext
  └─ impact_summary
    └─ requests 1
      └─ GetConnectionQuery
    └─ handlers 1
      └─ GetConnectionQueryHandler
    └─ mappings 1
      └─ ConnectionDto

