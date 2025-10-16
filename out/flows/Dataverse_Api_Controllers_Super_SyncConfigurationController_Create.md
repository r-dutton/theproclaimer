[web] POST /api/super/sync-configuration/  (Dataverse.Api.Controllers.Super.SyncConfigurationController.Create)  [L118–L126] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SyncConfigurationLightDto (var syncConfigDto) [L124]
  └─ calls SyncConfigurationRepository.Add [L122]
  └─ insert SyncConfiguration [L122]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method Add [L122]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L124]
      └─ ... (no implementation details available)

