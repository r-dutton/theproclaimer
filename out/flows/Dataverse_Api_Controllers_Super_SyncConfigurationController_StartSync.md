[web] PUT /api/super/sync-configuration/{id:Guid}/start  (Dataverse.Api.Controllers.Super.SyncConfigurationController.StartSync)  [L178–L188] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L181]
  └─ write SyncConfiguration [L181]
    └─ reads_from SyncConfigurations
  └─ uses_service RequestRegionService
    └─ method GetTimeZone [L185]
      └─ implementation Dataverse.Services.Features.Localisation.RequestRegionService.GetTimeZone [L10-L74]
        └─ uses_service Dictionary<string, string>
          └─ method GetTimeZone [L39]
            └─ ... (no implementation details available)
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfiguration writes=1 reads=0
    └─ services 1
      └─ RequestRegionService

