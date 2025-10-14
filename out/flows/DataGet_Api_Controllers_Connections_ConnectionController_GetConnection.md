[web] GET /api/connection/{apiType}  (DataGet.Api.Controllers.Connections.ConnectionController.GetConnection)  [L40–L44] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetConnectionQuery [L43]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Connections.App.Queries.GetConnectionQueryHandler.Handle [L20–L48]
      └─ calls ConnectionRepository.ReadQuery [L40]
      └─ maps_to ConnectionDto [L40]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L43]
      └─ uses_service RequestContextProvider
        └─ method CurrentContext [L38]

