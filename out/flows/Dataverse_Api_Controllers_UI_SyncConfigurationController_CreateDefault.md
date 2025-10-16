[web] POST /api/ui/sync-configuration/default  (Dataverse.Api.Controllers.UI.SyncConfigurationController.CreateDefault)  [L142–L156] status=201 [auth=Authentication.AdminPolicy]
  └─ maps_to SyncConfigurationLightDto (var syncConfigDto) [L154]
  └─ calls SyncConfigurationRepository.Add [L152]
  └─ calls SyncConfigurationRepository.ReadQuery [L145]
  └─ insert SyncConfiguration [L152]
    └─ reads_from SyncConfigurations
  └─ query SyncConfiguration [L145]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L145]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L154]
      └─ ... (no implementation details available)

