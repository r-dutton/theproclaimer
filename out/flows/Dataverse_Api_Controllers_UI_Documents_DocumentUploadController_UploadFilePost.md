[web] POST /api/documents/upload  (Dataverse.Api.Controllers.UI.Documents.DocumentUploadController.UploadFilePost)  [L32–L44] status=201
  └─ sends_request UploadChunkCommand [L36]
    └─ handled_by Cirrus.Services.Features.FileUpload.UploadChunkCommandHandler.Handle [L37–L139]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method UploadFile [L71]
          └─ implementation IStorageService.UploadFile [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_storage IStorageService.UploadFile [L71]

