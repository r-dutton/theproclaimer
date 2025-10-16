[web] POST /api/internal/storage/upload  (Dataverse.Api.Controllers.Internal.StorageController.UploadTempFile)  [L75–L86] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request UploadChunkCommand [L78]
    └─ handled_by Cirrus.Services.Features.FileUpload.UploadChunkCommandHandler.Handle [L37–L139]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method UploadFile [L71]
          └─ implementation IStorageService.UploadFile [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_storage IStorageService.UploadFile [L71]

