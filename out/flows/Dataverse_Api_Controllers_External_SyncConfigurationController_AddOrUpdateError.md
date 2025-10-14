[web] PUT /api/external/sync-configuration/{id:Guid}/error  (Dataverse.Api.Controllers.External.SyncConfigurationController.AddOrUpdateError)  [L96–L113] [auth=Authentication.AdminPolicy]
  └─ writes_to SyncConfigurationError [L107]
    └─ reads_from SyncConfigurationErrors
  └─ writes_to SyncConfigurationError [L99]
    └─ reads_from SyncConfigurationErrors
  └─ uses_service IControlledRepository<SyncConfigurationError>
    └─ method WriteQuery [L99]

