[web] GET /api/ui/fyi  (Dataverse.Api.Controllers.UI.FyiController.GetDownloadUrl)  [L293–L300] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service StorageService
    └─ method CopyFileFromUri [L297]
      └─ implementation Dataverse.Services.Features.Storage.StorageService.CopyFileFromUri [L18-L331]
  └─ uses_storage StorageService.CopyFileFromUri [L297]

