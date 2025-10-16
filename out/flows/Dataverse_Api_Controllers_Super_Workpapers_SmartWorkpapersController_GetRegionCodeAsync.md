[web] GET /api/super/smart-workpapers  (Dataverse.Api.Controllers.Super.Workpapers.SmartWorkpapersController.GetRegionCodeAsync)  [L143–L146] status=200 [auth=Authentication.MasterPolicy]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L145]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ impact_summary
    └─ services 1
      └─ TenantService
    └─ caches 1
      └─ IMemoryCache

