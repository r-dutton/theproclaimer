[web] DELETE /api/entities/{id:Guid}  (Cirrus.Api.Controllers.Firm.EntitiesController.Delete)  [L128–L135] status=200 [auth=user]
  └─ calls EntityRepository.Remove [L133]
  └─ calls EntityRepository.WriteQuery [L131]
  └─ delete Entity [L133]
  └─ write Entity [L131]
  └─ uses_service IControlledRepository<Entity>
    └─ method WriteQuery [L131]
      └─ ... (no implementation details available)
  └─ sends_request CanIAccessEntityQuery [L132]
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

