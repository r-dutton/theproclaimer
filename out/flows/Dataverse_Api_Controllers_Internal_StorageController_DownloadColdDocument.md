[web] GET /api/internal/storage/documents/{documentId:guid}/download-url  (Dataverse.Api.Controllers.Internal.StorageController.DownloadColdDocument)  [L88–L94] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetDocumentColdDownloadLink -> GetDocumentColdDownloadLinkHandler [L91]
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentColdDownloadLinkHandler.Handle [L28–L74]
      └─ uses_service DocumentServiceFactory
        └─ method GetDefaultColdStorageDocumentWithService [L58]
          └─ implementation DocumentServiceFactory.GetDefaultColdStorageDocumentWithService
      └─ uses_service IControlledRepository<DocumentVersion> (Scoped (inferred))
        └─ method ReadQuery [L46]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentVersionRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetDocumentColdDownloadLink
    └─ handlers 1
      └─ GetDocumentColdDownloadLinkHandler

