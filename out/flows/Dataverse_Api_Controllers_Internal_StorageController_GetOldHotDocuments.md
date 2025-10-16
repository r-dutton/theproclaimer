[web] GET /api/internal/storage/old-hot-documents/{jobId:Guid}  (Dataverse.Api.Controllers.Internal.StorageController.GetOldHotDocuments)  [L168–L207] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_cache IDistributedCache.GetRecordAsync [read] [L204]
  └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L171]
  └─ uses_service RequiredService
    └─ method AssignActiveTenant [L182]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L176]
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
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache

