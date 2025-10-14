[web] GET /documents/v1/documents/{id:Guid}/download  (Dataverse.Api.External.Controllers.v1.Documents.DocumentsController.DownloadDocument)  [L70–L76]
  └─ sends_request GetDocumentDownloadLink [L73]
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

