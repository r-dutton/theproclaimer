[web] PUT /api/external/sync-configuration/{id:Guid}/error  (Dataverse.Api.Controllers.External.SyncConfigurationController.AddOrUpdateError)  [L96–L113] status=200 [auth=Authentication.AdminPolicy]
  └─ insert SyncConfigurationError [L107]
    └─ reads_from SyncConfigurationErrors
  └─ write SyncConfigurationError [L99]
    └─ reads_from SyncConfigurationErrors
  └─ uses_service IControlledRepository<SyncConfigurationError>
    └─ method WriteQuery [L99]
      └─ ... (no implementation details available)

