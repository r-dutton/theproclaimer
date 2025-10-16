[web] POST /api/super/users/{userId:Guid}/share-point-sync  (Dataverse.Api.Controllers.Super.Core.UsersController.ForceSyncSharePoint)  [L250–L279] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository (methods: Remove,WriteQuery) [L278]
  └─ delete DataverseEntityFailureLog [L278]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L276]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L261]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L257]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ DataverseEntityFailureLog writes=2 reads=0
    └─ services 2
      └─ MockMessagingService
      └─ TenantService
    └─ caches 1
      └─ IMemoryCache

