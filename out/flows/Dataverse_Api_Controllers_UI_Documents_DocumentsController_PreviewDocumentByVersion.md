[web] GET /api/ui/documents/documents/{id}/{versionId}/preview-by-version  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.PreviewDocumentByVersion)  [L207–L214] [auth=Authentication.UserPolicy]
  └─ sends_request CanIAccessDocumentQuery [L210]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.CanIAccessDocumentQueryHandler.Handle [L37–L82]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L66]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L68]
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L58]
      └─ uses_service UserService
        └─ method GetUserAsync [L61]
  └─ sends_request GetDocumentPreviewLinkByVersionQuery [L211]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentPreviewLinkByVersionQueryHandler.Handle [L35–L135]
      └─ maps_to SecureDocumentStoreDto [L86]
      └─ maps_to DocumentVersionLightDto [L125]
        └─ automapper.registration DataverseMappingProfile (DocumentVersion->DocumentVersionLightDto) [L420]
      └─ uses_service DocumentServiceFactory
        └─ method GetForStore [L86]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L77]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L64]
      └─ uses_service IMapper
        └─ method Map [L86]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L131]
      └─ uses_service string[]
        └─ method Contains [L72]

