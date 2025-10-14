[web] GET /api/entities/{id}  (Cirrus.Api.Controllers.Firm.EntitiesController.Get)  [L70–L88] [auth=user]
  └─ maps_to EntityDto [L85]
    └─ automapper.registration CirrusMappingProfile (Entity->EntityDto) [L165]
  └─ maps_to EntityDto [L75]
    └─ automapper.registration CirrusMappingProfile (Entity->EntityDto) [L165]
  └─ calls EntityRepository.ReadQuery [L75]
  └─ queries Entity [L75]
  └─ uses_service IControlledRepository<Entity>
    └─ method ReadQuery [L75]
  └─ sends_request CanIAccessEntityQuery [L84]
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
  └─ sends_request CanIAccessEntityQuery [L80]
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

