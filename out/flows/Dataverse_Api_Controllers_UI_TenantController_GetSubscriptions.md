[web] GET /api/ui/tenants/subscriptions  (Dataverse.Api.Controllers.UI.TenantController.GetSubscriptions)  [L58–L68] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to SubscriptionLightDto [L62]
  └─ calls TenantRepository.ReadTable [L62]
  └─ query Tenant [L62]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method ReadTable [L62]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.ReadTable [L15-L180]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L61]
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
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ SubscriptionLightDto

