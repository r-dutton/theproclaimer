[web] POST /api/internal/documents/create-document-m2m  (Dataverse.Api.Controllers.Internal.Documents.DocumentsController.CreateDocumentViaM2M)  [L227–L236] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service StorageService
    └─ method MoveFile [L232]
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
  └─ uses_storage StorageService.MoveFile [L232]
  └─ sends_request CreateDocumentCommand -> CreateDocumentCommandHandler [L234]
    └─ handled_by DataGet.Integrations.Fyi.Api.Commands.CreateDocumentCommandHandler.Handle [L18–L33]
      └─ uses_service FyiService
        └─ method CreateDocument [L29]
          └─ implementation DataGet.Integrations.Fyi.Api.FyiService.CreateDocument [L30-L166]
            └─ uses_client FyiHttpClient [L50]
            └─ uses_service FyiHttpClient
              └─ method GetCabinets [L50]
                └─ implementation DataGet.Integrations.Fyi.Api.FyiHttpClient.GetCabinets [L24-L194]
  └─ returns FyiDocumentDto [L234]
  └─ impact_summary
    └─ clients 1
      └─ FyiHttpClient
    └─ services 1
      └─ StorageService
    └─ requests 1
      └─ CreateDocumentCommand
    └─ handlers 1
      └─ CreateDocumentCommandHandler
    └─ caches 1
      └─ IMemoryCache
    └─ storages 2
      └─ StorageService
      └─ StorageSettingsConfig
    └─ mappings 1
      └─ FyiDocumentDto

