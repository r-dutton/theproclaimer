[web] PUT /api/ui/fyi-elite/{id}/authorise-with-existing-info  (Dataverse.Api.Controllers.UI.FyiEliteController.AuthenticateWithExistingInfo)  [L90–L114] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L102]
  └─ write SyncConfiguration [L102]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L102]
      └─ ... (no implementation details available)
  └─ uses_service IDatagetFyiEliteService (AddTransient)
    └─ method Authenticate [L98]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiEliteService.Authenticate [L13-L53]
  └─ uses_service IDatagetFyiService (AddTransient)
    └─ method GetAccessInfo [L95]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetFyiService.GetAccessInfo [L19-L291]
  └─ uses_service UserService
    └─ method GetUserAsync [L94]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]

