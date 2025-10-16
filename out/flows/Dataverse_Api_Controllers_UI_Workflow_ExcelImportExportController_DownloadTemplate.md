[web] GET /api/ui/workflow/excel/download-master  (Dataverse.Api.Controllers.UI.Workflow.ExcelImportExportController.DownloadTemplate)  [L39–L47] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CreateDownloadCommand [L44]
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]
          └─ implementation IStorageService.CreateDownloadUrl [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_storage IStorageService.CreateDownloadUrl [L60]

