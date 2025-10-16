[web] POST /api/super/sync-configuration/default  (Dataverse.Api.Controllers.Super.SyncConfigurationController.CreateDefault)  [L128–L140] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SyncConfigurationLightDto (var syncConfigDto) [L138]
  └─ calls SyncConfigurationRepository.Add [L137]
  └─ insert SyncConfiguration [L137]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method Add [L137]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L138]
      └─ ... (no implementation details available)

