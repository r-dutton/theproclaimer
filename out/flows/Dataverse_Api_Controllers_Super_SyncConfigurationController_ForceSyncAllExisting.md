[web] PUT /api/super/sync-configuration/sync-all-existing/{id:Guid}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.ForceSyncAllExisting)  [L166–L176] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L169]
  └─ writes_to SyncConfiguration [L169]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L169]
  └─ uses_service RequestRegionService
    └─ method GetTimeZone [L173]

