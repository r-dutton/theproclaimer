[web] GET /api/super/workpapers/{tenantId:Guid}/tenant-creation-info  (Dataverse.Api.Controllers.Super.Workpapers.WorkpapersController.GetTenantCreationInfo)  [L53–L58] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetTenantCreateInfoQuery -> GetTenantCreateInfoQueryHandler [L57]
    └─ handled_by Dataverse.ApplicationService.Queries.Workpapers.GetTenantCreateInfoQueryHandler.Handle [L38–L81]
      └─ calls TenantRepository.ReadTable [L60]
      └─ uses_client WorkpapersClient [L73]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L79]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
      └─ uses_service WorkpapersClient
        └─ method Get [L73]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ clients 1
      └─ WorkpapersClient
    └─ requests 1
      └─ GetTenantCreateInfoQuery
    └─ handlers 1
      └─ GetTenantCreateInfoQueryHandler
    └─ caches 1
      └─ IMemoryCache

