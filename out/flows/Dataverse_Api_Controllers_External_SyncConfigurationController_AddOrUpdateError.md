[web] PUT /api/external/sync-configuration/{id:Guid}/error  (Dataverse.Api.Controllers.External.SyncConfigurationController.AddOrUpdateError)  [L96–L113] status=200 [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationErrorsRepository (methods: Add,WriteQuery) [L107]
  └─ insert SyncConfigurationError [L107]
    └─ reads_from SyncConfigurationErrors
  └─ write SyncConfigurationError [L99]
    └─ reads_from SyncConfigurationErrors
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ SyncConfigurationError writes=2 reads=0

