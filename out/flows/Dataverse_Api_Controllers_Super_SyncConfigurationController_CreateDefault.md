[web] POST /api/super/sync-configuration/default  (Dataverse.Api.Controllers.Super.SyncConfigurationController.CreateDefault)  [L128–L140] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SyncConfigurationLightDto (var syncConfigDto) [L138]
  └─ calls SyncConfigurationRepository.Add [L137]
  └─ insert SyncConfiguration [L137]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfiguration writes=1 reads=0
    └─ mappings 1
      └─ SyncConfigurationLightDto

