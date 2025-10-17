[web] POST /api/ui/clients/{clientId:Guid}/sync  (Dataverse.Api.Controllers.UI.Core.ClientsController.SyncToAllProducts)  [L313–L338] status=201 [auth=Authentication.UserPolicy]
  └─ uses_client ClientRepository [L316]
  └─ calls DataverseEntityFailureLogRepository (methods: Remove,WriteQuery) [L333]
  └─ delete DataverseEntityFailureLog [L333]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L331]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L325]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L323]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ sends_request BulkPropagateCommand -> BulkPropagateCommandHandler [L336]
    └─ handled_by Dataverse.ApplicationService.Commands.DataverseEntity.BulkPropagateCommandHandler.Handle [L45–L168]
      └─ calls TenantRepository.ReadTable [L159]
      └─ uses_service IControlledRepository<DataverseEntityFailureLog> (Scoped (inferred))
        └─ method WriteQuery [L145]
          └─ implementation Dataverse.Data.Repository.Base.DataverseEntityFailureLogRepository.WriteQuery
      └─ uses_service MockMessagingService
        └─ method RequestPropagation [L141]
          └─ ... (no implementation details available)
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L72]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync (see previous expansion)
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ DataverseEntityFailureLog writes=2 reads=0
    └─ clients 1
      └─ ClientRepository
    └─ services 2
      └─ MockMessagingService
      └─ TenantService
    └─ requests 1
      └─ BulkPropagateCommand
    └─ handlers 1
      └─ BulkPropagateCommandHandler
    └─ caches 1
      └─ IMemoryCache

