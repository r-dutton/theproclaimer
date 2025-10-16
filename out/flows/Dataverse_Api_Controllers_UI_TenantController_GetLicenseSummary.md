[web] GET /api/ui/tenants/license-summary  (Dataverse.Api.Controllers.UI.TenantController.GetLicenseSummary)  [L70–L95] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to LicenseSummaryDto [L74]
  └─ calls UserRepository.ReadQuery [L81]
  └─ calls TenantRepository.ReadTable [L74]
  └─ query User [L81]
  └─ query Tenant [L74]
    └─ reads_from Tenants
  └─ uses_service UserRepository
    └─ method ReadQuery [L81]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ uses_service TenantRepository
    └─ method ReadTable [L74]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L73]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ Tenant writes=0 reads=1
      └─ User writes=0 reads=1
    └─ services 3
      └─ TenantRepository
      └─ TenantService
      └─ UserRepository
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ LicenseSummaryDto

