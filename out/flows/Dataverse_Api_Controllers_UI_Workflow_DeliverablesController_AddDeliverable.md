[web] POST /api/ui/workflow/deliverables/  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.AddDeliverable)  [L85–L108] status=201 [auth=Authentication.UserPolicy]
  └─ maps_to DeliverableDto [L107]
  └─ calls DeliverableRepository (methods: Add,WriteQuery) [L105]
  └─ insert Deliverable [L105]
    └─ reads_from Deliverables
  └─ write Deliverable [L91]
    └─ reads_from Deliverables
  └─ uses_service JobParamsService
    └─ method GetDeliverableParams [L101]
      └─ implementation Dataverse.ApplicationService.Services.Workflow.JobParamsService.GetDeliverableParams [L24-L215]
        └─ calls ClientRepository.ReadQuery [L140]
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
        └─ uses_service LazyParamCache<DeliverableType>
          └─ method GetDeliverableType [L123]
            └─ ... (no implementation details available)
        └─ uses_service LazyParamCache<JobType>
          └─ method GetJobType [L113]
            └─ ... (no implementation details available)
        └─ uses_service IControlledRepository<JobStatus> (Scoped (inferred))
          └─ method GetDefaultStatus [L95]
            └─ implementation Dataverse.Data.Repository.Workflow.JobStatusRepository.GetDefaultStatus
        └─ uses_service LazyParamCache<JobStatus>
          └─ method GetStatus [L89]
            └─ ... (no implementation details available)
        └─ uses_service UserService
          └─ method GetJobParams [L73]
            └─ implementation Dataverse.ApplicationService.Services.UserService.GetJobParams [L15-L185]
              └─ calls UserRepository.ReadQuery [L133]
              └─ uses_service RequestInfoService
                └─ method GetIdentityId [L160]
                  └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId [L11-L92]
                    └─ uses_service RequestInfo
                      └─ method IsValidServiceAccountRequest [L82]
                        └─ implementation Cirrus.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
                        └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
                        └─ implementation Workpapers.Next.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
                    └─ logs ILogger<IRequestInfoService> [Warning] [L89]
              └─ uses_service User
                └─ method GetUserId [L43]
                  └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
                  └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
              └─ uses_service Guid?
                └─ method GetUserId [L40]
                  └─ ... (no implementation details available)
  └─ uses_service DeliverableRepository
    └─ method WriteQuery [L91]
      └─ implementation Dataverse.Data.Repository.Workflow.DeliverableRepository.WriteQuery [L8-L45]
  └─ sends_request CanIAccessJobQuery -> CanIAccessJobQueryHandler [L97]
    └─ handled_by Dataverse.ApplicationService.Queries.WorkFlow.CanIAccessJobQueryHandler.Handle [L39–L95]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L82]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Job> (Scoped (inferred))
        └─ method ReadQuery [L78]
          └─ implementation Dataverse.Data.Repository.Workflow.JobRepository.ReadQuery
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L73]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant (see previous expansion)
      └─ uses_service UserService
        └─ method GetUserAsync [L71]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
            └─ calls UserRepository.ReadQuery [L133]
            └─ uses_service RequestInfoService
              └─ method GetIdentityId [L160]
                └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId (see previous expansion)
            └─ uses_service User
              └─ method GetUserId [L43]
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId (see previous expansion)
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId (see previous expansion)
            └─ uses_service Guid?
              └─ method GetUserId [L40]
                └─ ... (no implementation details available)
      └─ uses_service RequestInfoService
        └─ method IsValidServiceAccountRequest [L68]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest (see previous expansion)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L91]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L75]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L73]
  └─ impact_summary
    └─ entities 2 (writes=2, reads=1)
      └─ Deliverable writes=2 reads=0
      └─ PublishedReportBatch writes=0 reads=1
    └─ clients 2
      └─ ClientRepository
      └─ WorkpapersClient
    └─ services 2
      └─ DeliverableRepository
      └─ JobParamsService
    └─ requests 2
      └─ CanIAccessFileQuery
      └─ CanIAccessJobQuery
    └─ handlers 2
      └─ CanIAccessFileQueryHandler
      └─ CanIAccessJobQueryHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 1
      └─ DeliverableDto

