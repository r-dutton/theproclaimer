[web] GET /api/ui/fyi-elite/{id}/test-connection  (Dataverse.Api.Controllers.UI.FyiEliteController.TestConnection)  [L64–L88] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L69]
  └─ write SyncConfiguration [L69]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L69]
      └─ ... (no implementation details available)
  └─ uses_service IDatagetFyiEliteService (AddTransient)
    └─ method TestConnection [L68]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiEliteService.TestConnection [L13-L53]
  └─ uses_service UserService
    └─ method GetUserAsync [L71]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]

