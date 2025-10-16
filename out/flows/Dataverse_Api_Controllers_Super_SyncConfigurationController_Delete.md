[web] DELETE /api/super/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.Delete)  [L190–L196] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request DeleteSyncConfigurationQuery -> DeleteSyncConfigurationQueryHandler [L193]
    └─ handled_by Dataverse.ApplicationService.Queries.SyncConfigurations.DeleteSyncConfigurationQueryHandler.Handle [L28–L68]
      └─ uses_service DataGetService
        └─ method DisconnectToken [L51]
          └─ implementation Dataverse.Services.Features.DataGet.DataGetService.DisconnectToken [L19-L182]
            └─ uses_service TenantService
              └─ method CreateCustomHeaderDictionary [L154]
                └─ implementation Dataverse.Services.Features.Tenants.TenantService.CreateCustomHeaderDictionary [L6-L27]
                  └─ uses_service TenantIdentificationService
                    └─ method GetCurrentTenant [L20]
                      └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                        └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                        └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                        └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
      └─ uses_service IControlledRepository<SyncConfiguration> (Scoped (inferred))
        └─ method WriteQuery [L43]
          └─ implementation Dataverse.Data.Repository.Sync.SyncConfigurationRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ DeleteSyncConfigurationQuery
    └─ handlers 1
      └─ DeleteSyncConfigurationQueryHandler
    └─ caches 1
      └─ IMemoryCache

