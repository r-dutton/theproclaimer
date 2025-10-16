[web] GET /api/ui/karbon/{id}/test-and-update-connection-settings  (Dataverse.Api.Controllers.UI.KarbonController.TestAndUpdateConnectionSettings)  [L50–L74] status=200 [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L57]
  └─ write SyncConfiguration [L57]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L57]
      └─ ... (no implementation details available)
  └─ uses_service IDatagetKarbonService (AddTransient)
    └─ method TestConnection [L53]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetKarbonService.TestConnection [L13-L53]
  └─ uses_service UserService
    └─ method GetUserAsync [L59]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]

