[web] GET /documents/v1/documents/{id:Guid}/download  (Dataverse.Api.External.Controllers.v1.Documents.DocumentsController.DownloadDocument)  [L70–L76] status=200
  └─ sends_request GetDocumentDownloadLink -> GetDocumentDownloadLinkHandler [L73]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentDownloadLinkHandler.Handle [L30–L77]
      └─ maps_to SecureDocumentStoreDto [L65]
      └─ uses_service UserService
        └─ method GetUserId [L73]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
            └─ uses_service User
              └─ method GetUserId [L43]
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
            └─ uses_service Guid?
              └─ method GetUserId [L40]
                └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DocumentAuditLog> (Scoped (inferred))
        └─ method Add [L68]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentAuditLogRepository.Add
      └─ uses_service DocumentServiceFactory
        └─ method GetForStore [L65]
          └─ implementation DocumentServiceFactory.GetForStore
      └─ uses_service IControlledRepository<Document> (Scoped (inferred))
        └─ method ReadQuery [L56]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetDocumentDownloadLink
    └─ handlers 1
      └─ GetDocumentDownloadLinkHandler
    └─ mappings 1
      └─ SecureDocumentStoreDto

