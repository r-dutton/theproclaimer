[web] GET /api/ui/workflow/excel/download-master  (Dataverse.Api.Controllers.UI.Workflow.ExcelImportExportController.DownloadTemplate)  [L39–L47] [auth=Authentication.UserPolicy]
  └─ sends_request CreateDownloadCommand [L44]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]

