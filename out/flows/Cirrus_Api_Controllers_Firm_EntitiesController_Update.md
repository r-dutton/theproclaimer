[web] PUT /api/entities/{id:Guid}  (Cirrus.Api.Controllers.Firm.EntitiesController.Update)  [L111–L123] status=200 [auth=user]
  └─ calls ClientRepository.WriteQuery [L116]
  └─ calls EntityRepository.WriteQuery [L114]
  └─ write Client [L116]
  └─ write Entity [L114]
  └─ uses_service IFirmSettingsService (AddScoped)
    └─ method GetFirmJurisdictions [L118]
      └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetFirmJurisdictions [L15-L112]
        └─ uses_service IRequestProcessor (InstancePerDependency)
          └─ method GetCurrentSettings [L45]
            └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCurrentSettings [L7-L35]
        └─ uses_service TenantService
          └─ method GetCurrentSettings [L34]
            └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentSettings [L26-L139]
              └─ uses_service IRequestProcessor (InstancePerDependency)
                └─ method GetCatalogByDataverseId [L111]
                  └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId [L7-L35]
              └─ uses_service Tenant
                └─ method AssignCurrentTenantId [L80]
                  └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId [L20-L187]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L104]
        └─ uses_cache IDistributedCache.SetStringAsync [write] [L108]
        └─ uses_cache IDistributedCache.GetStringAsync [read] [L98]
  └─ sends_request CanIAccessEntityQuery -> CanIAccessEntityQueryHandler [L115]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.CanIAccessEntityQueryHandler.Handle [L41–L99]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L83]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Entity> (Scoped (inferred))
        └─ method ReadQuery [L81]
          └─ implementation Cirrus.Data.Repository.Firm.EntityRepository.ReadQuery
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L72]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L72]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
            └─ uses_service IRequestProcessor (InstancePerDependency)
              └─ method GetCatalogByDataverseId [L111]
                └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId (see previous expansion)
            └─ uses_service Tenant
              └─ method AssignCurrentTenantId [L80]
                └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId (see previous expansion)
      └─ uses_service IRequestInfoService (AddScoped)
        └─ method IsValidServiceAccountRequest [L67]
          └─ implementation Dataverse.Services.Features.RequestInfoService.IsValidServiceAccountRequest [L11-L92]
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L92]
      └─ uses_cache IDistributedCache.DoesRecordExistAsync [access] [L74]
      └─ uses_cache IDistributedCache.CreateAccessKey [write] [L72]
  └─ impact_summary
    └─ entities 2 (writes=2, reads=0)
      └─ Client writes=1 reads=0
      └─ Entity writes=1 reads=0
    └─ services 1
      └─ IFirmSettingsService
    └─ requests 1
      └─ CanIAccessEntityQuery
    └─ handlers 1
      └─ CanIAccessEntityQueryHandler
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache

