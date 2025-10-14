[web] GET /api/ui/documents/documents/{id}/{fileExtension}/preview  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.PreviewDocument)  [L198–L205] [auth=Authentication.UserPolicy]
  └─ sends_request CanIAccessDocumentQuery [L201]
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
  └─ sends_request GetDocumentPreviewLink [L202]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentPreviewLinkHandler.Handle [L34–L119]
      └─ maps_to SecureDocumentStoreDto [L107]
      └─ uses_service DocumentServiceFactory
        └─ method GetDefaultColdStorageDocumentWithService [L90]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L98]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method WriteQuery [L69]
      └─ uses_service IMapper
        └─ method Map [L107]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L114]
      └─ uses_service string[]
        └─ method Contains [L67]

