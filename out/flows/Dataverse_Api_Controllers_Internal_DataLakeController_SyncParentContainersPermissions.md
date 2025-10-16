[web] POST /api/internal/data-lake/security/sync  (Dataverse.Api.Controllers.Internal.DataLakeController.SyncParentContainersPermissions)  [L67–L78] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IAzureDataLakeService (AddScoped)
    └─ method EnsureParentPermissionsAsync [L76]
      └─ implementation Dataverse.Services.DataLake.Implementations.AzureDataLakeService.EnsureParentPermissionsAsync [L33-L425]
        └─ uses_service TenantService
          └─ method EnsureParentPermissionsAsync [L188]
            └─ implementation Dataverse.Services.Features.Tenants.TenantService.EnsureParentPermissionsAsync [L6-L27]
              └─ uses_service TenantIdentificationService
                └─ method GetCurrentTenant [L20]
                  └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                    └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                    └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                    └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
        └─ uses_cache IMemoryCache.Set [write] [L377]
        └─ uses_cache IMemoryCache.TryGetValue [read] [L372]
  └─ uses_service IDocumentStoreService (AddScoped)
    └─ method GetReadOnlyDocumentStoresWithCredentials [L72]
      └─ implementation Dataverse.ApplicationService.Services.DocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L17-L89]
        └─ uses_service IControlledRepository<DocumentStore> (Scoped (inferred))
          └─ method GetReadOnlyDocumentStoresWithCredentials [L66]
            └─ implementation Dataverse.Data.Repository.Documents.DocumentStoreRepository.GetReadOnlyDocumentStoresWithCredentials
        └─ uses_service TenantService
          └─ method GetReadOnlyDocumentStoresWithCredentials [L57]
            └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetReadOnlyDocumentStoresWithCredentials [L6-L27]
              └─ uses_service TenantIdentificationService
                └─ method GetCurrentTenant [L20]
                  └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant (see previous expansion)
        └─ maps_to SecureDocumentStoreDto [L66]
          └─ automapper.registration DataverseMappingProfile (DocumentStore->SecureDocumentStoreDto) [L438]
        └─ uses_cache IDistributedCache.SetRecordAsync [write] [L71]
        └─ uses_cache IDistributedCache.GetRecordAsync [read] [L62]
  └─ uses_storage IDocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L72]
  └─ impact_summary
    └─ services 2
      └─ IAzureDataLakeService
      └─ IDocumentStoreService
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ storages 1
      └─ IDocumentStoreService
    └─ mappings 1
      └─ SecureDocumentStoreDto

