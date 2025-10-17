[web] DELETE /api/external/sync-configuration/{id:Guid}/error  (Dataverse.Api.Controllers.External.SyncConfigurationController.DeleteErrors)  [L121–L135] status=200 [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationErrorsRepository.WriteQuery [L129]
  └─ write SyncConfigurationError [L129]
    └─ reads_from SyncConfigurationErrors
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfigurationError writes=1 reads=0

