[web] GET /api/documents/upload  (Dataverse.Api.Controllers.UI.Documents.DocumentUploadController.UploadFileGet)  [L22–L30]
  └─ sends_request UploadChunkExistsQuery [L25]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Services.Features.FileUpload.UploadChunkExistsQueryHandler.Handle [L24–L62]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method BlockExists [L50]

