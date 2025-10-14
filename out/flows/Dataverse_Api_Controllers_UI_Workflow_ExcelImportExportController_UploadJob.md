[web] POST /api/ui/workflow/excel/upload-job  (Dataverse.Api.Controllers.UI.Workflow.ExcelImportExportController.UploadJob)  [L92–L100] [auth=Authentication.UserPolicy]
  └─ sends_request ImportJobsFromDtoCommand [L95]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.ImportJobsFromDtoCommandHandler.Handle [L31–L263]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L184]
      └─ uses_service IControlledRepository<DeliverableType>
        └─ method ReadQuery [L200]
      └─ uses_service IControlledRepository<JobStatus>
        └─ method ReadQuery [L231]
      └─ uses_service IControlledRepository<JobType>
        └─ method ReadQuery [L253]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L216]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L56]

