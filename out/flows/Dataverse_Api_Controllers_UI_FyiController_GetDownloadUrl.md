[web] GET /api/ui/fyi  (Dataverse.Api.Controllers.UI.FyiController.GetDownloadUrl)  [L293–L300] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service StorageService
    └─ method CopyFileFromUri [L297]
      └─ implementation Dataverse.Services.Features.Storage.StorageService.CopyFileFromUri [L18-L331]
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
  └─ uses_storage StorageService.CopyFileFromUri [L297]
  └─ impact_summary
    └─ services 1
      └─ StorageService
    └─ caches 1
      └─ IMemoryCache
    └─ storages 2
      └─ StorageService
      └─ StorageSettingsConfig

