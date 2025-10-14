[web] POST /api/ui/sync-configuration/default  (Dataverse.Api.Controllers.UI.SyncConfigurationController.CreateDefault)  [L142–L156] [auth=Authentication.AdminPolicy]
  └─ maps_to SyncConfigurationLightDto (var syncConfigDto) [L154]
  └─ calls SyncConfigurationRepository.Add [L152]
  └─ calls SyncConfigurationRepository.ReadQuery [L145]
  └─ queries SyncConfiguration [L145]
    └─ reads_from SyncConfigurations
  └─ writes_to SyncConfiguration [L152]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L145]
  └─ uses_service IMapper
    └─ method Map [L154]

