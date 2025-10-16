[web] GET /api/external/sync-configuration/  (Dataverse.Api.Controllers.External.SyncConfigurationController.GetAll)  [L55–L70] status=200 [auth=Authentication.AdminPolicy]
  └─ maps_to SyncConfigurationInsecureDto [L58]
    └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationInsecureDto) [L283]
  └─ uses_cache IDistributedCache.SetRecordAsync [write] [L67]
  └─ calls SyncConfigurationRepository.ReadQuery [L58]
  └─ query SyncConfiguration [L58]
    └─ reads_from SyncConfigurations
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L66]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L58]
      └─ ... (no implementation details available)

