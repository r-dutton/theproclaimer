[web] GET /api/ui/documents/documents/{id}/download  (Dataverse.Api.Controllers.UI.Documents.DocumentsController.DownloadDocument)  [L189–L196] [auth=Authentication.UserPolicy]
  └─ sends_request CanIAccessDocumentQuery [L192]
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
  └─ sends_request GetDocumentDownloadLink [L193]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentDownloadLinkHandler.Handle [L30–L77]
      └─ maps_to SecureDocumentStoreDto [L65]
      └─ uses_service DocumentServiceFactory
        └─ method GetForStore [L65]
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L56]
      └─ uses_service IControlledRepository<DocumentAuditLog>
        └─ method Add [L68]
      └─ uses_service IMapper
        └─ method Map [L65]
      └─ uses_service UserService
        └─ method GetUserId [L73]

