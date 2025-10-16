[web] POST /api/users/{userId}/resendInvite  (Cirrus.Api.Controllers.Firm.UsersController.ResendInvite)  [L174–L183] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls UserRepository.ReadQuery [L177]
  └─ query User [L177]
  └─ uses_service ITenantService (AddScoped)
    └─ method GetCurrentTenant [L181]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ ITenantService
    └─ caches 1
      └─ IMemoryCache

