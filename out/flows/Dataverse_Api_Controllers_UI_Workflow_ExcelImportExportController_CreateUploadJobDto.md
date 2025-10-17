[web] POST /api/ui/workflow/excel/create-upload  (Dataverse.Api.Controllers.UI.Workflow.ExcelImportExportController.CreateUploadJobDto)  [L84–L90] status=201 [auth=Authentication.UserPolicy]
  └─ maps_to ImportJobsDto [L89]
  └─ sends_request CreateJobImportsFromExcelCommand -> CreateJobImportsFromExcelCommandHandler [L87]
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.CreateJobImportsFromExcelCommandHandler.Handle [L31–L57]
      └─ uses_service StorageService
        └─ method GetStream [L48]
          └─ implementation Dataverse.Services.Features.Storage.StorageService.GetStream [L18-L331]
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
      └─ uses_storage StorageService.GetStream [L48]
  └─ impact_summary
    └─ requests 1
      └─ CreateJobImportsFromExcelCommand
    └─ handlers 1
      └─ CreateJobImportsFromExcelCommandHandler
    └─ caches 1
      └─ IMemoryCache
    └─ storages 1
      └─ StorageSettingsConfig
    └─ mappings 1
      └─ ImportJobsDto

