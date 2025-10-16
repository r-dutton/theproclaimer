[web] POST /api/internal/documents/{documentId:guid}/versions/add-document-version-M2M  (Dataverse.Api.Controllers.Internal.Documents.DocumentVersionsController.AddDocumentVersionViaM2M)  [L36–L45] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service StorageService
    └─ method MoveFile [L42]
      └─ implementation Dataverse.Services.Features.Storage.StorageService.MoveFile [L18-L331]
        └─ uses_service TenantService
          └─ method GetPrivateContainer [L305]
            └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetPrivateContainer [L6-L27]
              └─ uses_service TenantIdentificationService
                └─ method GetCurrentTenant [L20]
                  └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                    └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                    └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                    └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
        └─ uses_service StorageSettingsConfig
          └─ method GetBlobServiceClient [L31]
            └─ implementation StorageSettingsConfig.GetBlobServiceClient
        └─ uses_storage StorageSettingsConfig.GetBlobServiceClient [L31]
  └─ uses_storage StorageService.MoveFile [L42]
  └─ impact_summary
    └─ services 1
      └─ StorageService
    └─ caches 1
      └─ IMemoryCache
    └─ storages 2
      └─ StorageService
      └─ StorageSettingsConfig

