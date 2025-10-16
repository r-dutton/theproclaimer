[web] PUT /api/super/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.Update)  [L142–L152] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L145]
  └─ write SyncConfiguration [L145]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfiguration writes=1 reads=0

