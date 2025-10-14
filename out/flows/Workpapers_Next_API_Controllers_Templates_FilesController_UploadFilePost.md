[web] POST /api/files/upload  (Workpapers.Next.API.Controllers.Templates.FilesController.UploadFilePost)  [L34–L48]
  └─ sends_request UploadChunkCommand [L37]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Services.Features.FileUpload.UploadChunkCommandHandler.Handle [L37–L139]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method UploadFile [L71]

