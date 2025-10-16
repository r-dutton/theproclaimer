[web] GET /api/documents/upload  (Dataverse.Api.Controllers.UI.Documents.DocumentUploadController.UploadFileGet)  [L22–L30] status=200
  └─ sends_request UploadChunkExistsQuery -> UploadChunkExistsQueryHandler [L25]
    └─ handled_by Cirrus.Services.Features.FileUpload.UploadChunkExistsQueryHandler.Handle [L24–L62]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method BlockExists [L50]
          └─ implementation DataGet.Data.BlobStorage.StorageService.BlockExists [L11-L116]
            └─ uses_service RequestContextProvider
              └─ method GetContainer [L89]
                └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.GetContainer
      └─ uses_storage IStorageService.BlockExists [L50]
  └─ impact_summary
    └─ requests 1
      └─ UploadChunkExistsQuery
    └─ handlers 1
      └─ UploadChunkExistsQueryHandler

