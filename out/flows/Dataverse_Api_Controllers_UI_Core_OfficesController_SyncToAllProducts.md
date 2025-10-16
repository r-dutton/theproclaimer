[web] POST /api/ui/offices/{officeId:Guid}/sync  (Dataverse.Api.Controllers.UI.Core.OfficesController.SyncToAllProducts)  [L244–L269] status=201 [auth=Authentication.UserPolicy]
  └─ calls DataverseEntityFailureLogRepository (methods: Remove,WriteQuery) [L264]
  └─ calls OfficeRepository.ReadQuery [L247]
  └─ delete DataverseEntityFailureLog [L264]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L262]
    └─ reads_from DataverseEntityFailureLogs
  └─ query Office [L247]
    └─ reads_from Offices
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L256]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L254]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L247]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ sends_request BulkPropagateCommand -> BulkPropagateCommandHandler [L267]
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
      └─ Office writes=0 reads=1
    └─ services 3
      └─ MockMessagingService
      └─ OfficeRepository
      └─ TenantService
    └─ requests 1
      └─ BulkPropagateCommand
    └─ handlers 1
      └─ BulkPropagateCommandHandler
    └─ caches 1
      └─ IMemoryCache

