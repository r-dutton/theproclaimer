[web] DELETE /api/external/sync-configuration/{id:Guid}/error  (Dataverse.Api.Controllers.External.SyncConfigurationController.DeleteErrors)  [L121–L135] [auth=Authentication.AdminPolicy]
  └─ writes_to SyncConfigurationError [L129]
    └─ reads_from SyncConfigurationErrors
  └─ uses_service IControlledRepository<SyncConfigurationError>
    └─ method WriteQuery [L129]

