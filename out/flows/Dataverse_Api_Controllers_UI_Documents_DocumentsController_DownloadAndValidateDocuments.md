[web] POST /api/ui/documents/documents/download-and-validate  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.DownloadAndValidateDocuments)  [L255–L283] [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L263]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L293]
  └─ calls IntegrationSettingsRepository.ReadQuery [L263]
  └─ queries IntegrationSettings [L263]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L263]
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetDocumentDownloadUrlAsync [L270]
  └─ sends_request IManageDocumentDownloadAndUploadCommand [L273]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.IManage.IManageDocumentDownloadAndUploadCommandHandler.Handle [L25–L108]
      └─ uses_service StorageService
        └─ method UploadFile [L58]
  └─ sends_request FileDownloadAndUploadFromUriCommand [L277]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.Services.Features.FileUpload.FileDownloadAndUploadFromUriCommandHandler.Handle [L25–L124]
      └─ uses_client HttpClient [L40]
      └─ uses_service HttpClient (SingleInstance)
        └─ method GetAsync [L40]
      └─ uses_service StorageService
        └─ method UploadFile [L60]

