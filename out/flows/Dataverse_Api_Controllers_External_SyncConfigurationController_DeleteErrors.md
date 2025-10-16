[web] DELETE /api/external/sync-configuration/{id:Guid}/error  (Dataverse.Api.Controllers.External.SyncConfigurationController.DeleteErrors)  [L121–L135] status=200 [auth=Authentication.AdminPolicy]
  └─ write SyncConfigurationError [L129]
    └─ reads_from SyncConfigurationErrors
  └─ uses_service IControlledRepository<SyncConfigurationError>
    └─ method WriteQuery [L129]
      └─ ... (no implementation details available)

