[web] POST /api/external/sync-configuration/{id:Guid}/complete  (Dataverse.Api.Controllers.External.SyncConfigurationController.Complete)  [L81–L94] status=201 [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L88]
  └─ write SyncConfiguration [L88]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfiguration writes=1 reads=0

