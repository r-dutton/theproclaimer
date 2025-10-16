[web] POST /api/documents/upload  (Dataverse.Api.Controllers.UI.Documents.DocumentUploadController.UploadFilePost)  [L32–L44] status=201
  └─ sends_request UploadChunkCommand -> UploadChunkCommandHandler [L36]
    └─ handled_by Cirrus.Services.Features.FileUpload.UploadChunkCommandHandler.Handle [L37–L139]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method UploadFile [L71]
          └─ implementation DataGet.Data.BlobStorage.StorageService.UploadFile [L11-L116]
            └─ uses_service RequestContextProvider
              └─ method GetContainer [L89]
                └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.GetContainer
      └─ uses_storage IStorageService.UploadFile [L71]
  └─ impact_summary
    └─ requests 1
      └─ UploadChunkCommand
    └─ handlers 1
      └─ UploadChunkCommandHandler

