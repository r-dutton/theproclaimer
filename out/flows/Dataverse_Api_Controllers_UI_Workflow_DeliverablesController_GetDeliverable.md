[web] GET /api/ui/workflow/deliverables/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverablesController.GetDeliverable)  [L64–L72] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DeliverableDto [L68]
  └─ calls DeliverableRepository.ReadQuery [L68]
  └─ query Deliverable [L68]
    └─ reads_from Deliverables
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L70]
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
    └─ method GetUserId [L70]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
        └─ uses_service User
          └─ method GetUserId [L43]
            └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
            └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
        └─ uses_service Guid?
          └─ method GetUserId [L40]
            └─ ... (no implementation details available)
  └─ uses_service DeliverableRepository
    └─ method ReadQuery [L68]
      └─ implementation Dataverse.Data.Repository.Workflow.DeliverableRepository.ReadQuery [L8-L45]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Deliverable writes=0 reads=1
    └─ clients 1
      └─ WorkpapersClient
    └─ services 3
      └─ DeliverableRepository
      └─ FirmSettingsService
      └─ UserService
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache
    └─ mappings 1
      └─ DeliverableDto

