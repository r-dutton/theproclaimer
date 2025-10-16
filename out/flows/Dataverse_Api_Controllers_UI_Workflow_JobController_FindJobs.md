[web] GET /api/ui/workflow/jobs/find  (Dataverse.Api.Controllers.UI.Workflow.JobController.FindJobs)  [L54–L70] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request FindJobsQuery [L69]
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.FindJobsQueryHandler.Handle [L178–L538]
      └─ calls JobRepository.ReadQuery [L269]
      └─ maps_to DeliverableDto [L249]
      └─ maps_to JobDto [L245]
        └─ converts_to JobExportDto [L312]
      └─ maps_to WorkflowTaskDto [L251]
      └─ maps_to JobExportDto [L258]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L270]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
      └─ uses_service UserService
        └─ method GetUserId [L270]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service IControlledRepository<JobStatus>
        └─ method ReadQuery [L322]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L245]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L217]
          └─ implementation Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync [L8-L45]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

