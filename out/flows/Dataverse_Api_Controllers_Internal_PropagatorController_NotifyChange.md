[web] PUT /api/internal/Propagator/notify-change  (Dataverse.Api.Controllers.Internal.PropagatorController.NotifyChange)  [L105–L111] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request BulkPropagateCommand -> BulkPropagateCommandHandler [L110]
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
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
                  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
                  └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
                  └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ impact_summary
    └─ requests 1
      └─ BulkPropagateCommand
    └─ handlers 1
      └─ BulkPropagateCommandHandler
    └─ caches 1
      └─ IMemoryCache

