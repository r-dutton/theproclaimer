[web] PUT /api/super/sync-configuration/sync/{id:Guid}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.ForceNextSync)  [L154–L164] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L157]
  └─ write SyncConfiguration [L157]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfiguration writes=1 reads=0

