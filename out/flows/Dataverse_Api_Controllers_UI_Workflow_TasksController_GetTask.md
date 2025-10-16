[web] GET /api/ui/workflow/tasks/{id:guid}  (Dataverse.Api.Controllers.UI.Workflow.TasksController.GetTask)  [L88–L95] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to WorkflowTaskDto [L91]
  └─ calls TaskRepository.ReadQuery [L91]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L93]
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
    └─ method GetUserId [L93]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
        └─ uses_service User
          └─ method GetUserId [L43]
            └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
            └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
        └─ uses_service Guid?
          └─ method GetUserId [L40]
            └─ ... (no implementation details available)
  └─ uses_service TaskRepository
    └─ method ReadQuery [L91]
      └─ implementation Dataverse.Data.Repository.Workflow.TaskRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ clients 1
      └─ WorkpapersClient
    └─ services 3
      └─ FirmSettingsService
      └─ TaskRepository
      └─ UserService
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 1
      └─ WorkflowTaskDto

