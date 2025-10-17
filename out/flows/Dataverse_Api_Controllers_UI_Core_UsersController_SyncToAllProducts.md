[web] POST /api/ui/users/{userId:Guid}/sync  (Dataverse.Api.Controllers.UI.Core.UsersController.SyncToAllProducts)  [L339–L364] status=201 [auth=Authentication.UserPolicy]
  └─ calls DataverseEntityFailureLogRepository (methods: Remove,WriteQuery) [L359]
  └─ calls UserRepository.ReadQuery [L342]
  └─ delete DataverseEntityFailureLog [L359]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L357]
    └─ reads_from DataverseEntityFailureLogs
  └─ query User [L342]
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L351]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L349]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ uses_service UserRepository
    └─ method ReadQuery [L342]
      └─ implementation Dataverse.Data.Repository.Users.UserRepository.ReadQuery [L8-L51]
  └─ sends_request BulkPropagateCommand -> BulkPropagateCommandHandler [L362]
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
    └─ entities 2 (writes=2, reads=1)
      └─ DataverseEntityFailureLog writes=2 reads=0
      └─ User writes=0 reads=1
    └─ services 3
      └─ MockMessagingService
      └─ TenantService
      └─ UserRepository
    └─ requests 1
      └─ BulkPropagateCommand
    └─ handlers 1
      └─ BulkPropagateCommandHandler
    └─ caches 1
      └─ IMemoryCache

