[web] GET /api/ui/workflow/excel/download-master  (Dataverse.Api.Controllers.UI.Workflow.ExcelImportExportController.DownloadTemplate)  [L39–L47] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CreateDownloadCommand -> CreateDownloadCommandHandler [L44]
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]
          └─ implementation DataGet.Data.BlobStorage.StorageService.CreateDownloadUrl [L11-L116]
            └─ uses_service RequestContextProvider
              └─ method GetContainer [L89]
                └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.GetContainer
      └─ uses_storage IStorageService.CreateDownloadUrl [L60]
  └─ impact_summary
    └─ requests 1
      └─ CreateDownloadCommand
    └─ handlers 1
      └─ CreateDownloadCommandHandler

