[web] POST /api/internal/storage/upload  (Dataverse.Api.Controllers.Internal.StorageController.UploadTempFile)  [L75–L86] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request UploadChunkCommand -> UploadChunkCommandHandler [L78]
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

