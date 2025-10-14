[web] POST /api/super/sync-configuration/  (Dataverse.Api.Controllers.Super.SyncConfigurationController.Create)  [L118–L126] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SyncConfigurationLightDto (var syncConfigDto) [L124]
  └─ calls SyncConfigurationRepository.Add [L122]
  └─ writes_to SyncConfiguration [L122]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method Add [L122]
  └─ uses_service IMapper
    └─ method Map [L124]

