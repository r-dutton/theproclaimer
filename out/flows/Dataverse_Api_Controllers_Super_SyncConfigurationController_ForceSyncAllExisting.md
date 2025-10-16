[web] PUT /api/super/sync-configuration/sync-all-existing/{id:Guid}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.ForceSyncAllExisting)  [L166–L176] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L169]
  └─ write SyncConfiguration [L169]
    └─ reads_from SyncConfigurations
  └─ uses_service RequestRegionService
    └─ method GetTimeZone [L173]
      └─ implementation Dataverse.Services.Features.Localisation.RequestRegionService.GetTimeZone [L10-L74]
      └─ implementation Dataverse.Services.Features.Localisation.RequestRegionService.GetTimeZone [L10-L74]
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L169]
      └─ ... (no implementation details available)

