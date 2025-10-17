[web] POST /api/ui/sync-configuration/default  (Dataverse.Api.Controllers.UI.SyncConfigurationController.CreateDefault)  [L142–L156] status=201 [auth=Authentication.AdminPolicy]
  └─ maps_to SyncConfigurationLightDto (var syncConfigDto) [L154]
  └─ calls SyncConfigurationRepository (methods: Add,ReadQuery) [L152]
  └─ insert SyncConfiguration [L152]
    └─ reads_from SyncConfigurations
  └─ query SyncConfiguration [L145]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=1, reads=1)
      └─ SyncConfiguration writes=1 reads=1
    └─ mappings 1
      └─ SyncConfigurationLightDto

