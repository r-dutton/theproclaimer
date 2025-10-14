[web] GET /api/ui/karbon/{id}/test-and-update-connection-settings  (Dataverse.Api.Controllers.UI.KarbonController.TestAndUpdateConnectionSettings)  [L50–L74] [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L57]
  └─ writes_to SyncConfiguration [L57]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L57]
  └─ uses_service IDatagetKarbonService (AddTransient)
    └─ method TestConnection [L53]
  └─ uses_service UserService
    └─ method GetUserAsync [L59]

