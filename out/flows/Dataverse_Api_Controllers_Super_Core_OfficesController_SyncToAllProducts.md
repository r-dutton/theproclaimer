[web] POST /api/super/offices/{officeId:Guid}/sync  (Dataverse.Api.Controllers.Super.Core.OfficesController.SyncToAllProducts)  [L75–L100] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository (methods: Remove,WriteQuery) [L95]
  └─ calls OfficeRepository.ReadQuery [L78]
  └─ delete DataverseEntityFailureLog [L95]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L93]
    └─ reads_from DataverseEntityFailureLogs
  └─ query Office [L78]
    └─ reads_from Offices
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L87]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L85]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
        └─ uses_service TenantIdentificationService
          └─ method GetCurrentTenant [L20]
            └─ implementation Dataverse.Tenants.Tenants.TenantIdentificationService.GetCurrentTenant [L27-L149]
              └─ uses_cache IMemoryCache.GetOrCreateAsync [read] [L117]
              └─ uses_cache IMemoryCache.GetOrCreate [read] [L96]
              └─ logs ILogger<ITenantIdentificationService> [Warning] [L53]
  └─ uses_service OfficeRepository
    └─ method ReadQuery [L78]
      └─ implementation Dataverse.Data.Repository.Firm.OfficeRepository.ReadQuery [L8-L41]
  └─ sends_request BulkPropagateCommand -> BulkPropagateCommandHandler [L98]
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

