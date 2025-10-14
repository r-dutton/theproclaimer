[web] PUT /api/entities/{id:Guid}  (Cirrus.Api.Controllers.Firm.EntitiesController.Update)  [L111–L123] [auth=user]
  └─ calls EntityRepository.WriteQuery [L114]
  └─ calls ClientRepository.WriteQuery [L116]
  └─ writes_to Client [L116]
  └─ writes_to Entity [L114]
  └─ uses_service IControlledRepository<Client>
    └─ method WriteQuery [L116]
  └─ uses_service IControlledRepository<Entity>
    └─ method WriteQuery [L114]
  └─ uses_service IFirmSettingsService (AddScoped)
    └─ method GetFirmJurisdictions [L118]
  └─ sends_request CanIAccessEntityQuery [L115]
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

