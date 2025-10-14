[web] PUT /api/ui/fyi-elite/{id}/authorise-with-existing-info  (Dataverse.Api.Controllers.UI.FyiEliteController.AuthenticateWithExistingInfo)  [L90–L114] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L102]
  └─ writes_to SyncConfiguration [L102]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L102]
  └─ uses_service IDatagetFyiEliteService (AddTransient)
    └─ method Authenticate [L98]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetAccessInfo [L95]
  └─ uses_service UserService
    └─ method GetUserAsync [L94]

