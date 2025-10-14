[web] POST /api/ui/workflow/excel/create-upload  (Dataverse.Api.Controllers.UI.Workflow.ExcelImportExportController.CreateUploadJobDto)  [L84–L90] [auth=Authentication.UserPolicy]
  └─ maps_to ImportJobsDto [L89]
  └─ uses_service IMapper
    └─ method Map [L89]
  └─ sends_request CreateJobImportsFromExcelCommand [L87]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.CreateJobImportsFromExcelCommandHandler.Handle [L31–L57]
      └─ uses_service StorageService
        └─ method GetStream [L48]

