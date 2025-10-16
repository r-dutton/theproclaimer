[web] PUT /api/dataverse/offices  (Cirrus.Api.Controllers.Dataverse.DataverseController.UpdateOfficeChanges)  [L48–L53] status=200 [auth=Authentication.RequireTenantIdPolicy]
  └─ uses_service ITenantService (AddScoped)
    └─ method GetCurrentTenant [L51]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenant [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ impact_summary
    └─ services 1
      └─ ITenantService
    └─ caches 1
      └─ IMemoryCache

