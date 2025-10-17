[web] GET /api/ui/workflow/tasks/for-job/{jobId:guid}  (Dataverse.Api.Controllers.UI.Workflow.TasksController.GetJobTasks)  [L59–L72] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to WorkflowTaskLightDto [L65]
  └─ calls TaskRepository.ReadQuery [L65]
  └─ uses_service TaskRepository
    └─ method ReadQuery [L65]
      └─ implementation Dataverse.Data.Repository.Workflow.TaskRepository.ReadQuery [L8-L40]
  └─ sends_request CanIAccessJobQuery -> CanIAccessJobQueryHandler [L63]
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
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
      └─ uses_service UserService
        └─ method GetUserAsync [L71]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
            └─ calls UserRepository.ReadQuery [L133]
            └─ uses_service RequestInfoService
              └─ method GetIdentityId [L160]
                └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId [L11-L92]
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
            └─ uses_service User
              └─ method GetUserId [L43]
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
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
    └─ services 1
      └─ TaskRepository
    └─ requests 1
      └─ CanIAccessJobQuery
    └─ handlers 1
      └─ CanIAccessJobQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ WorkflowTaskLightDto

