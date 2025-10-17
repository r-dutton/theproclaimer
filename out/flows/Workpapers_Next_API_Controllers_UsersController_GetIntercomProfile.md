[web] GET /api/users/intercom-profile  (Workpapers.Next.API.Controllers.UsersController.GetIntercomProfile)  [L149–L155] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetIntercomProfileQuery -> GetIntercomProfileQueryHandler [L153]
    └─ handled_by Cirrus.Central.Dataverse.Queries.GetIntercomProfileQueryHandler.Handle [L24–L61]
      └─ uses_service DataverseService
        └─ method Get [L52]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Get [L14-L66]
            └─ uses_service TenantService
              └─ method GetStandardQueryParams [L62]
                └─ implementation Cirrus.Central.Tenants.TenantService.GetStandardQueryParams [L26-L139]
                  └─ uses_service IRequestProcessor (InstancePerDependency)
                    └─ method GetCatalogByDataverseId [L111]
                      └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId [L7-L35]
                  └─ uses_service Tenant
                    └─ method AssignCurrentTenantId [L80]
                      └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId [L20-L187]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L104]
      └─ uses_service UserService
        └─ method GetUser [L42]
          └─ implementation Cirrus.ApplicationService.Services.UserService.GetUser [L14-L122]
            └─ uses_service IRequestInfoService (AddScoped)
              └─ method GetIdentityId [L103]
                └─ implementation Dataverse.Services.Features.RequestInfoService.GetIdentityId [L11-L92]
            └─ uses_service IControlledRepository<User> (Scoped (inferred))
              └─ method GetUserId [L50]
                └─ implementation Cirrus.Data.Repository.Firm.UserRepository.GetUserId
            └─ uses_service User
              └─ method GetUserId [L37]
                └─ implementation Cirrus.DomainModel.Model.Firm.User.GetUserId [L18-L345]
            └─ uses_service Guid?
              └─ method GetUserId [L34]
                └─ ... (no implementation details available)
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L39]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
            └─ uses_service IRequestProcessor (InstancePerDependency)
              └─ method GetCatalogByDataverseId [L111]
                └─ implementation DataGet.Services.Features.Requests.RequestProcessor.GetCatalogByDataverseId (see previous expansion)
            └─ uses_service Tenant
              └─ method AssignCurrentTenantId [L80]
                └─ implementation Dataverse.Tenants.Model.Tenant.AssignCurrentTenantId (see previous expansion)
  └─ returns IntercomProfileDto [L153]
  └─ impact_summary
    └─ requests 1
      └─ GetIntercomProfileQuery
    └─ handlers 1
      └─ GetIntercomProfileQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ IntercomProfileDto

