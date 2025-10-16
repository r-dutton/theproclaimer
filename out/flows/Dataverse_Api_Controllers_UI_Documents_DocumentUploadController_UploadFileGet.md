[web] GET /api/documents/upload  (Dataverse.Api.Controllers.UI.Documents.DocumentUploadController.UploadFileGet)  [L22–L30] status=200
  └─ sends_request UploadChunkExistsQuery [L25]
    └─ handled_by Cirrus.Services.Features.FileUpload.UploadChunkExistsQueryHandler.Handle [L24–L62]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method BlockExists [L50]
          └─ implementation IStorageService.BlockExists [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_storage IStorageService.BlockExists [L50]

