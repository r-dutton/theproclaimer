[web] PUT /api/super/sync-configuration/sync-all-existing/{id:Guid}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.ForceSyncAllExisting)  [L166–L176] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L169]
  └─ write SyncConfiguration [L169]
    └─ reads_from SyncConfigurations
  └─ uses_service RequestRegionService
    └─ method GetTimeZone [L173]
      └─ implementation Dataverse.Services.Features.Localisation.RequestRegionService.GetTimeZone [L10-L74]
        └─ uses_service Dictionary<string, string>
          └─ method GetTimeZone [L39]
            └─ ... (no implementation details available)
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfiguration writes=1 reads=0
    └─ services 1
      └─ RequestRegionService

