[web] GET /api/ui/workflow/jobs/find  (Dataverse.Api.Controllers.UI.Workflow.JobController.FindJobs)  [L54–L70] [auth=Authentication.UserPolicy]
  └─ sends_request FindJobsQuery [L69]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.FindJobsQueryHandler.Handle [L178–L538]
      └─ calls JobRepository.ReadQuery [L269]
      └─ maps_to DeliverableDto [L249]
      └─ maps_to JobDto [L245]
        └─ converts_to JobExportDto [L311]
      └─ maps_to WorkflowTaskDto [L251]
      └─ maps_to JobExportDto [L258]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L270]
      └─ uses_service IControlledRepository<JobStatus>
        └─ method ReadQuery [L322]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L245]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L217]
      └─ uses_service UserService
        └─ method GetUserId [L270]

