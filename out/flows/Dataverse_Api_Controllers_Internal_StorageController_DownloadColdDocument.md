[web] GET /api/internal/storage/documents/{documentId:guid}/download-url  (Dataverse.Api.Controllers.Internal.StorageController.DownloadColdDocument)  [L88–L94] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetDocumentColdDownloadLink [L91]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Documents.GetDocumentColdDownloadLinkHandler.Handle [L28–L74]
      └─ uses_service DocumentServiceFactory
        └─ method GetDefaultColdStorageDocumentWithService [L58]
      └─ uses_service IControlledRepository<DocumentVersion>
        └─ method ReadQuery [L46]

