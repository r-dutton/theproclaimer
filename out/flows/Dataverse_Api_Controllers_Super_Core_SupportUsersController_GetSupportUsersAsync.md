[web] GET /api/super/support-users  (Dataverse.Api.Controllers.Super.Core.SupportUsersController.GetSupportUsersAsync)  [L26–L34] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetSupportUsersQuery -> GetSupportUsersQueryHandler [L32]
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.GetSupportUsersQueryHandler.Handle [L29–L61]
      └─ calls TenantRepository.ReadTable [L49]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L47]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ impact_summary
    └─ requests 1
      └─ GetSupportUsersQuery
    └─ handlers 1
      └─ GetSupportUsersQueryHandler
    └─ caches 1
      └─ IMemoryCache

