[web] POST /api/ui/documents/documents/download-and-validate  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.DownloadAndValidateDocuments)  [L255–L283] status=201 [auth=Authentication.UserPolicy]
  └─ maps_to IntegrationSettingsDto [L263]
    └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
  └─ calls IntegrationSettingsRepository.ReadQuery [L263]
  └─ query IntegrationSettings [L263]
    └─ reads_from IntegrationSettingss
  └─ uses_service IControlledRepository<IntegrationSettings>
    └─ method ReadQuery [L263]
      └─ ... (no implementation details available)
  └─ uses_service IDatagetImanageService (AddTransient)
    └─ method GetDocumentDownloadUrlAsync [L270]
      └─ implementation Dataverse.Connections.DataGet.Services.DataGetImanageService.GetDocumentDownloadUrlAsync [L19-L225]
  └─ sends_request IManageDocumentDownloadAndUploadCommand [L273]
    └─ handled_by Dataverse.ApplicationService.Commands.IManage.IManageDocumentDownloadAndUploadCommandHandler.Handle [L25–L108]
      └─ uses_service StorageService
        └─ method UploadFile [L58]
          └─ implementation Dataverse.Services.Features.Storage.StorageService.UploadFile [L18-L331]
      └─ uses_storage StorageService.UploadFile [L58]
  └─ sends_request FileDownloadAndUploadFromUriCommand [L277]
    └─ handled_by Dataverse.Services.Features.FileUpload.FileDownloadAndUploadFromUriCommandHandler.Handle [L25–L124]
      └─ uses_client HttpClient [L40]
      └─ uses_service StorageService
        └─ method UploadFile [L60]
          └─ implementation Dataverse.Services.Features.Storage.StorageService.UploadFile [L18-L331]
      └─ uses_service HttpClient (SingleInstance)
        └─ method GetAsync [L40]
          └─ ... (no implementation details available)
      └─ uses_storage StorageService.UploadFile [L60]

