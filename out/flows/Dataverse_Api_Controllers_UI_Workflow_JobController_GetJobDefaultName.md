[web] GET /api/ui/workflow/jobs/default-name  (Dataverse.Api.Controllers.UI.Workflow.JobController.GetJobDefaultName)  [L95–L100] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request DefaultJobNameQuery -> DefaultJobNameQueryHandler [L98]
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.DefaultJobNameQueryHandler.Handle [L32–L73]
      └─ maps_to JobTypeDto [L51]
      └─ uses_service JobParamsService
        └─ method GetClient [L50]
          └─ implementation Dataverse.ApplicationService.Services.Workflow.JobParamsService.GetClient [L24-L215]
            └─ uses_client ClientRepository [L137]
            └─ uses_service FirmSettingsService
              └─ method GetClient [L142]
                └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetClient [L18-L97]
                  └─ uses_client WorkpapersClient [L78]
                  └─ uses_service WorkpapersClient
                    └─ method GetCurrentWorkpapersSettingsAsync [L78]
                      └─ ... (no implementation details available)
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
            └─ uses_service RequestInfoService
              └─ method GetClient [L136]
                └─ implementation Dataverse.Services.Features.RequestInfoService.GetClient [L11-L92]
                  └─ uses_service RequestInfo
                    └─ method IsValidServiceAccountRequest [L82]
                      └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                      └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
                        └─ uses_service RequestInfo
                          └─ method IsValidServiceAccountRequest [L82]
                            └─ ... (service recursion detected)
                        └─ logs ILogger<IRequestInfoService> [Warning] [L89]
                      └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L84]
                        └─ uses_service RequestInfo
                          └─ method IsValidServiceAccountRequest [L71]
                            └─ ... (service recursion detected)
                        └─ logs ILogger<IRequestInfoService> [Warning] [L81]
                  └─ logs ILogger<IRequestInfoService> [Warning] [L89]
            └─ uses_service List<ClientDto>
              └─ method GetClient [L133]
                └─ implementation Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.List.GetClient [L60-L77]
                  └─ calls PublishedReportBatchRepository.ReadQuery [L66]
                  └─ uses_service IRequestProcessor (InstancePerDependency)
                    └─ method ProcessAsync [L70]
                      └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
                        └─ constructs RequestProcessorWrapper<CanIAccessFileQuery,Unit>
                        └─ resolves IPipelineBehavior<CanIAccessFileQuery,Unit> chain
                        └─ invokes IAsyncRequestHandler<CanIAccessFileQuery,Unit>.Handle
                        └─ dispatches CanIAccessFileQuery [L70]
                  └─ uses_service IControlledRepository<PublishedReportBatch> (Scoped (inferred))
                    └─ method ReadQuery [L66]
                      └─ implementation Cirrus.Data.Repository.Accounting.Report.PublishedReportBatchRepository.ReadQuery
                  └─ query PublishedReportBatch [L66]
                  └─ dispatches CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70]
                  └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70] ... (reused)
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ PublishedReportBatch writes=0 reads=1
    └─ clients 2
      └─ ClientRepository
      └─ WorkpapersClient
    └─ requests 2
      └─ CanIAccessFileQuery
      └─ DefaultJobNameQuery
    └─ handlers 2
      └─ CanIAccessFileQueryHandler
      └─ DefaultJobNameQueryHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 1
      └─ JobTypeDto

