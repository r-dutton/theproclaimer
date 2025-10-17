[web] POST /api/ui/clients/{clientId:Guid}/share-point-sync  (Dataverse.Api.Controllers.UI.Core.ClientsController.ForceSyncSharePoint)  [L343–L357] status=201 [auth=Authentication.UserPolicy]
  └─ calls DataverseEntityFailureLogRepository (methods: Remove,WriteQuery) [L356]
  └─ delete DataverseEntityFailureLog [L356]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L354]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L348]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L346]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ DataverseEntityFailureLog writes=2 reads=0
    └─ services 2
      └─ MockMessagingService
      └─ TenantService
    └─ caches 1
      └─ IMemoryCache

