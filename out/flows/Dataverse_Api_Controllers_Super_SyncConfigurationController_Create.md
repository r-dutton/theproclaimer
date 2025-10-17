[web] POST /api/super/sync-configuration/  (Dataverse.Api.Controllers.Super.SyncConfigurationController.Create)  [L118–L126] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SyncConfigurationLightDto (var syncConfigDto) [L124]
  └─ calls SyncConfigurationRepository.Add [L122]
  └─ insert SyncConfiguration [L122]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfiguration writes=1 reads=0
    └─ mappings 1
      └─ SyncConfigurationLightDto

