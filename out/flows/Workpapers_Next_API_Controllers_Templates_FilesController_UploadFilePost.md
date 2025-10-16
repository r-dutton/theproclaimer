[web] POST /api/files/upload  (Workpapers.Next.API.Controllers.Templates.FilesController.UploadFilePost)  [L34–L48] status=201
  └─ sends_request UploadChunkCommand [L37]
    └─ handled_by Cirrus.Services.Features.FileUpload.UploadChunkCommandHandler.Handle [L37–L139]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method UploadFile [L71]
          └─ implementation IStorageService.UploadFile [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_storage IStorageService.UploadFile [L71]

