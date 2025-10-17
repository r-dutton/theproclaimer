[web] POST /api/ui/documents/documents/download-and-validate  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.DownloadAndValidateDocuments)  [L255–L283] status=201 [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L263]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L263]
  └─ query IntegrationSettings [L263]
    └─ reads_from IntegrationSettingss
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetDocumentDownloadUrlAsync [L270]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetDocumentDownloadUrlAsync [L19-L225]
  └─ sends_request FileDownloadAndUploadFromUriCommand -> FileDownloadAndUploadFromUriCommandHandler [L277]
    └─ handled_by Dataverse.Services.Features.FileUpload.FileDownloadAndUploadFromUriCommandHandler.Handle [L25–L124]
      └─ uses_client HttpClient [L40]
      └─ uses_service StorageService
        └─ method UploadFile [L60]
          └─ implementation Dataverse.Services.Features.Storage.StorageService.UploadFile [L18-L331]
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
      └─ uses_service HttpClient (SingleInstance)
        └─ method GetAsync [L40]
          └─ ... (no implementation details available)
      └─ uses_storage StorageService.UploadFile [L60]
  └─ sends_request IManageDocumentDownloadAndUploadCommand -> IManageDocumentDownloadAndUploadCommandHandler [L273]
    └─ handled_by Dataverse.ApplicationService.Commands.IManage.IManageDocumentDownloadAndUploadCommandHandler.Handle [L25–L108]
      └─ uses_service StorageService
        └─ method UploadFile [L58]
          └─ implementation Dataverse.Services.Features.Storage.StorageService.UploadFile (see previous expansion)
      └─ uses_storage StorageService.UploadFile [L58]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ IntegrationSettings writes=0 reads=1
    └─ clients 1
      └─ HttpClient
    └─ services 1
      └─ IDatagetImanageService
    └─ requests 2
      └─ FileDownloadAndUploadFromUriCommand
      └─ IManageDocumentDownloadAndUploadCommand
    └─ handlers 2
      └─ FileDownloadAndUploadFromUriCommandHandler
      └─ IManageDocumentDownloadAndUploadCommandHandler
    └─ caches 1
      └─ IMemoryCache
    └─ storages 1
      └─ StorageSettingsConfig
    └─ mappings 1
      └─ IntegrationSettingsDto

