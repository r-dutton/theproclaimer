[web] GET /api/ui/workflow/excel/download  (Dataverse.Api.Controllers.UI.Workflow.ExcelImportExportController.DownloadJobs)  [L49–L82] [auth=Authentication.UserPolicy]
  └─ sends_request CreateDownloadCommand [L79]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]
  └─ sends_request ExportJobsFromExcelCommand [L78]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.ExportJobsFromExcelCommandHandler.Handle [L27–L45]
      └─ uses_service WorkflowExcelWriter
        └─ method WriteAsync [L41]
  └─ sends_request FindExcelJobsQuery [L65]

