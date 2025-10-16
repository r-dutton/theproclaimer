[web] GET /api/ui/clients/{id:guid}/has-dependencies  (Dataverse.Api.Controllers.UI.Core.ClientsController.CheckForExternalDependencies)  [L139–L145] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CheckClientExternalDependenciesQuery -> CheckClientExternalDependenciesQueryHandler [L142]
    └─ handled_by Dataverse.ApplicationService.Queries.Clients.CheckClientExternalDependenciesQueryHandler.Handle [L35–L158]
      └─ uses_client CirrusClient [L115]
      └─ uses_client WorkpapersClient [L100]
      └─ uses_service TenantLicenseService
        └─ method GetFirmSubscriptions [L153]
          └─ implementation Dataverse.ApplicationService.Services.TenantLicenseService.GetFirmSubscriptions [L22-L185]
            └─ uses_service TenantService
              └─ method GetFirmSubscriptions [L74]
                └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetFirmSubscriptions [L6-L27]
                  └─ uses_service TenantIdentificationService
                    └─ method GetCurrentTenant [L20]
                      └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                        └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                        └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                        └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
            └─ maps_to SubscriptionDto [L76]
      └─ uses_service IControlledRepository<Document> (Scoped (inferred))
        └─ method ReadQuery [L130]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentRepository.ReadQuery
      └─ uses_service CirrusClient
        └─ method Get [L115]
          └─ ... (no implementation details available)
      └─ uses_service WorkpapersClient
        └─ method Get [L100]
          └─ ... (no implementation details available)
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L62]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant (see previous expansion)
      └─ uses_cache IDistributedCache.SetRecordAsync [write] [L155]
      └─ uses_cache IDistributedCache.GetRecordAsync [read] [L146]
  └─ impact_summary
    └─ clients 2
      └─ CirrusClient
      └─ WorkpapersClient
    └─ requests 1
      └─ CheckClientExternalDependenciesQuery
    └─ handlers 1
      └─ CheckClientExternalDependenciesQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ SubscriptionDto

