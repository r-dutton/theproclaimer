[web] GET /api/ui/tenants/license-summary/trials/requested  (Dataverse.Api.Controllers.UI.TenantController.GetRequestedLicenseSummaries)  [L97–L118] status=200 [auth=Authentication.UserPolicy]
  └─ uses_cache IDistributedCache.GetRecordAsync [read] [L113]
  └─ calls TenantRepository.ReadTable [L101]
  └─ query Tenant [L101]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L101]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L100]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Tenant writes=0 reads=1
    └─ services 2
      └─ TenantRepository
      └─ TenantService
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache

