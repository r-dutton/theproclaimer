[web] POST /api/documents/upload  (Dataverse.Api.Controllers.UI.Documents.DocumentUploadController.UploadFilePost)  [L32–L44]
  └─ sends_request UploadChunkCommand [L36]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Services.Features.FileUpload.UploadChunkCommandHandler.Handle [L37–L139]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method UploadFile [L71]

