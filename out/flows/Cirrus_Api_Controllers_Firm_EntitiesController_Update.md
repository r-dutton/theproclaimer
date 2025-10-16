[web] PUT /api/entities/{id:Guid}  (Cirrus.Api.Controllers.Firm.EntitiesController.Update)  [L111–L123] status=200 [auth=user]
  └─ calls EntityRepository.WriteQuery [L114]
  └─ calls ClientRepository.WriteQuery [L116]
  └─ write Client [L116]
  └─ write Entity [L114]
  └─ uses_service IControlledRepository<Client>
    └─ method WriteQuery [L116]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<Entity>
    └─ method WriteQuery [L114]
      └─ ... (no implementation details available)
  └─ uses_service IFirmSettingsService (AddScoped)
    └─ method GetFirmJurisdictions [L118]
      └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetFirmJurisdictions [L15-L112]
  └─ sends_request CanIAccessEntityQuery [L115]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessEntityQueryHandler.Handle [L41–L99]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L72]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
      └─ uses_service IControlledRepository<Entity>
        └─ method ReadQuery [L81]
          └─ ... (no implementation details available)
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L67]
          └─ implementation IRequestInfoService.IsValidServiceAccountRequest [L20-L20]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L83]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L72]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L92]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L74]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L72]

