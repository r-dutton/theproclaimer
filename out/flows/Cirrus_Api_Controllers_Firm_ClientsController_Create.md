[web] POST /api/clients/  (Cirrus.Api.Controllers.Firm.ClientsController.Create)  [L162–L174] status=201 [auth=user]
  └─ calls ClientRepository.Add [L172]
  └─ insert Client [L172]
  └─ uses_service IUserService (InstancePerLifetimeScope)
    └─ method GetUser [L166]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
        └─ uses_service RequestProcessor
          └─ method GetUserByDataverseId [L287]
            └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
            └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
            └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetUserByDataverseId
            └─ +1 additional_requests elided
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_service User>
          └─ method GetUserIdFromToken [L106]
            └─ ... (no implementation details available)
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ uses_service IFirmSettingsService (AddScoped)
    └─ method GetCurrentSettings [L165]
      └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetCurrentSettings [L15-L112]
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
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Client writes=1 reads=0
    └─ services 2
      └─ IFirmSettingsService
      └─ IUserService
    └─ caches 2
      └─ IDistributedCache
      └─ IMemoryCache

