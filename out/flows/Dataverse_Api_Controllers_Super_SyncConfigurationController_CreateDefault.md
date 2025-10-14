[web] POST /api/super/sync-configuration/default  (Dataverse.Api.Controllers.Super.SyncConfigurationController.CreateDefault)  [L128–L140] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SyncConfigurationLightDto (var syncConfigDto) [L138]
  └─ calls SyncConfigurationRepository.Add [L137]
  └─ writes_to SyncConfiguration [L137]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method Add [L137]
  └─ uses_service IMapper
    └─ method Map [L138]

