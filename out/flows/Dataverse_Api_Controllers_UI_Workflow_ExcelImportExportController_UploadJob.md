[web] POST /api/ui/workflow/excel/upload-job  (Dataverse.Api.Controllers.UI.Workflow.ExcelImportExportController.UploadJob)  [L92–L100] status=201 [auth=Authentication.UserPolicy]
  └─ sends_request ImportJobsFromDtoCommand [L95]
    └─ handled_by Dataverse.ApplicationService.Commands.Workflow.ImportJobsFromDtoCommandHandler.Handle [L31–L263]
      └─ uses_service IControlledRepository<Client>
        └─ method ReadQuery [L184]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DeliverableType>
        └─ method ReadQuery [L200]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<JobStatus>
        └─ method ReadQuery [L231]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<JobType>
        └─ method ReadQuery [L253]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L216]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L56]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

