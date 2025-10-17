[web] GET /api/internal/tenants/  (Dataverse.Api.Controllers.Internal.TenantController.Search)  [L42–L51] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request FindTenantsQuery -> FindTenantsQueryHandler [L50]
    └─ handled_by Cirrus.Central.Dataverse.Queries.FindTenantsQueryHandler.Handle [L32–L52]
      └─ uses_service DataverseService
        └─ method Get [L50]
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
  └─ impact_summary
    └─ requests 1
      └─ FindTenantsQuery
    └─ handlers 1
      └─ FindTenantsQueryHandler
    └─ caches 1
      └─ IMemoryCache

