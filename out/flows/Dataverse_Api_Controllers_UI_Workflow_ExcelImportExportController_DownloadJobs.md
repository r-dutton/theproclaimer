[web] GET /api/ui/workflow/excel/download  (Dataverse.Api.Controllers.UI.Workflow.ExcelImportExportController.DownloadJobs)  [L49–L82] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CreateDownloadCommand [L79]
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]
          └─ implementation IStorageService.CreateDownloadUrl [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_storage IStorageService.CreateDownloadUrl [L60]
  └─ sends_request ExportJobsFromExcelCommand [L78]
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.ExportJobsFromExcelCommandHandler.Handle [L27–L45]
      └─ uses_service WorkflowExcelWriter
        └─ method WriteAsync [L41]
          └─ ... (no implementation details available)
  └─ sends_request FindExcelJobsQuery [L65]

