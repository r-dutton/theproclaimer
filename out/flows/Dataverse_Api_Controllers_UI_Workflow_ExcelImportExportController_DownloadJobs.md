[web] GET /api/ui/workflow/excel/download  (Dataverse.Api.Controllers.UI.Workflow.ExcelImportExportController.DownloadJobs)  [L49–L82] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CreateDownloadCommand -> CreateDownloadCommandHandler [L79]
    └─ handled_by Cirrus.ApplicationService.Services.Commands.CreateDownloadCommandHandler.Handle [L48–L73]
      └─ uses_service IStorageService (InstancePerLifetimeScope)
        └─ method CreateDownloadUrl [L60]
          └─ implementation DataGet.Data.BlobStorage.StorageService.CreateDownloadUrl [L11-L116]
            └─ uses_service RequestContextProvider
              └─ method GetContainer [L89]
                └─ resolves_request DataGet.ApplicationService.Services.RequestContextProvider.GetContainer
      └─ uses_storage IStorageService.CreateDownloadUrl [L60]
  └─ sends_request ExportJobsFromExcelCommand -> ExportJobsFromExcelCommandHandler [L78]
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.ExportJobsFromExcelCommandHandler.Handle [L27–L45]
      └─ uses_service WorkflowExcelWriter
        └─ method WriteAsync [L41]
          └─ ... (no implementation details available)
  └─ sends_request FindExcelJobsQuery [L65]
  └─ impact_summary
    └─ requests 3
      └─ CreateDownloadCommand
      └─ ExportJobsFromExcelCommand
      └─ FindExcelJobsQuery
    └─ handlers 2
      └─ CreateDownloadCommandHandler
      └─ ExportJobsFromExcelCommandHandler

