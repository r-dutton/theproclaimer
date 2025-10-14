[web] PUT /api/super/sync-configuration/{id:Guid}/start  (Dataverse.Api.Controllers.Super.SyncConfigurationController.StartSync)  [L178–L188] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L181]
  └─ writes_to SyncConfiguration [L181]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L181]
  └─ uses_service RequestRegionService
    └─ method GetTimeZone [L185]

