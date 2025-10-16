[web] POST /api/ui/storage/start-zip  (Dataverse.Api.Controllers.UI.StorageController.StartCreateZipFile)  [L55–L101] status=201 [auth=Authentication.UserPolicy]
  └─ uses_service RequiredService
    └─ method AssignActiveTenant [L67]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L58]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ impact_summary
    └─ services 2
      └─ RequiredService
      └─ TenantService
    └─ caches 1
      └─ IMemoryCache

