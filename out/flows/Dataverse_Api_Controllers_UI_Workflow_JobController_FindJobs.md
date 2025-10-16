[web] GET /api/ui/workflow/jobs/find  (Dataverse.Api.Controllers.UI.Workflow.JobController.FindJobs)  [L54–L70] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request FindJobsQuery -> FindJobsQueryHandler [L69]
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.FindJobsQueryHandler.Handle [L178–L538]
      └─ calls JobRepository.ReadQuery [L269]
      └─ maps_to JobDto [L245]
        └─ converts_to JobExportDto [L312]
      └─ maps_to JobExportDto [L258]
      └─ maps_to WorkflowTaskDto [L251]
      └─ maps_to DeliverableDto [L249]
      └─ uses_service IControlledRepository<JobStatus> (Scoped (inferred))
        └─ method ReadQuery [L322]
          └─ implementation Dataverse.Data.Repository.Workflow.JobStatusRepository.ReadQuery
      └─ uses_service FirmSettingsService
        └─ method GetCurrentSettingsAsync [L270]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
            └─ uses_client WorkpapersClient [L78]
            └─ uses_service IControlledRepository<FirmSettings> (Scoped (inferred))
              └─ method GetCurrentSettingsAsync [L52]
                └─ implementation Dataverse.Data.Repository.Firm.FirmSettingsRepository.GetCurrentSettingsAsync
            └─ uses_service TenantService
              └─ method GetCurrentSettingsAsync [L44]
                └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentSettingsAsync [L6-L27]
                  └─ uses_service TenantIdentificationService
                    └─ method GetCurrentTenant [L20]
                      └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                        └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                        └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                        └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
            └─ uses_cache IDistributedCache.GetRecordAsync [read] [L70]
            └─ uses_cache IDistributedCache.RemoveAsync [write] [L46]
      └─ uses_service UserService
        └─ method GetUserId [L270]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
            └─ uses_service User
              └─ method GetUserId [L43]
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
            └─ uses_service Guid?
              └─ method GetUserId [L40]
                └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L217]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ clients 1
      └─ WorkpapersClient
    └─ requests 1
      └─ FindJobsQuery
    └─ handlers 1
      └─ FindJobsQueryHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 4
      └─ DeliverableDto
      └─ JobDto
      └─ JobExportDto
      └─ WorkflowTaskDto

