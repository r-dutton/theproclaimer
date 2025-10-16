[web] POST /api/ui/workflow/excel/create-upload  (Dataverse.Api.Controllers.UI.Workflow.ExcelImportExportController.CreateUploadJobDto)  [L84–L90] status=201 [auth=Authentication.UserPolicy]
  └─ maps_to ImportJobsDto [L89]
  └─ uses_service IMapper
    └─ method Map [L89]
      └─ ... (no implementation details available)
  └─ sends_request CreateJobImportsFromExcelCommand [L87]
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.CreateJobImportsFromExcelCommandHandler.Handle [L31–L57]
      └─ uses_service StorageService
        └─ method GetStream [L48]
          └─ implementation Dataverse.Services.Features.Storage.StorageService.GetStream [L18-L331]
      └─ uses_storage StorageService.GetStream [L48]

