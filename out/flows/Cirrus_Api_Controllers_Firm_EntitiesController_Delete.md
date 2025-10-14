[web] DELETE /api/entities/{id:Guid}  (Cirrus.Api.Controllers.Firm.EntitiesController.Delete)  [L128–L135] [auth=user]
  └─ calls EntityRepository.Remove [L133]
  └─ calls EntityRepository.WriteQuery [L131]
  └─ writes_to Entity [L133]
  └─ writes_to Entity [L131]
  └─ uses_service IControlledRepository<Entity>
    └─ method WriteQuery [L131]
  └─ sends_request CanIAccessEntityQuery [L132]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessEntityQueryHandler.Handle [L41–L99]
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L81]
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L67]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L83]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L72]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L72]
      └─ uses_cache IDistributedCache [L92]
        └─ method SetRecordAsync [write] [L92]
      └─ uses_cache IDistributedCache [L74]
        └─ method DoesRecordExistAsync [access] [L74]
      └─ uses_cache IDistributedCache [L72]
        └─ method CreateAccessKey [write] [L72]

