[web] GET /api/ui/fyi-elite/{id}/test-connection  (Dataverse.Api.Controllers.UI.FyiEliteController.TestConnection)  [L64–L88] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L69]
  └─ writes_to SyncConfiguration [L69]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L69]
  └─ uses_service IDatagetFyiEliteService (AddTransient)
    └─ method TestConnection [L68]
  └─ uses_service UserService
    └─ method GetUserAsync [L71]

