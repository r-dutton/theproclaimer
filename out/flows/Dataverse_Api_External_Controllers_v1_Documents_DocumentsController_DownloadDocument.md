[web] GET /documents/v1/documents/{id:Guid}/download  (Dataverse.Api.External.Controllers.v1.Documents.DocumentsController.DownloadDocument)  [L70–L76] status=200
  └─ sends_request GetDocumentDownloadLink [L73]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentDownloadLinkHandler.Handle [L30–L77]
      └─ maps_to SecureDocumentStoreDto [L65]
      └─ uses_service UserService
        └─ method GetUserId [L73]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service DocumentServiceFactory
        └─ method GetForStore [L65]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Document>
        └─ method ReadQuery [L56]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentAuditLog>
        └─ method Add [L68]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L65]
          └─ ... (no implementation details available)

