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
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ SyncConfiguration writes=0 reads=1
    └─ services 1
      └─ TenantService
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 1
      └─ SyncConfigurationInsecureDto

