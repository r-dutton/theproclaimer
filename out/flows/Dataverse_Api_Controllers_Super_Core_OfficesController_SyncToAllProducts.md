[web] POST /api/super/offices/{officeId:Guid}/sync  (Dataverse.Api.Controllers.Super.Core.OfficesController.SyncToAllProducts)  [L75–L100] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L95]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L93]
  └─ calls OfficeRepository.ReadQuery [L78]
  └─ query Office [L78]
    └─ reads_from Offices
  └─ delete DataverseEntityFailureLog [L95]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L93]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L87]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L85]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L93]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L78]
      └─ ... (no implementation details available)
  └─ sends_request BulkPropagateCommand [L98]
    └─ handled_by Dataverse.ApplicationService.Commands.DataverseEntity.BulkPropagateCommandHandler.Handle [L45–L168]
      └─ calls TenantRepository.ReadTable [L159]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L72]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
      └─ uses_service MockMessagingService
        └─ method RequestPropagation [L141]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<DataverseEntityFailureLog>
        └─ method WriteQuery [L145]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L136]
          └─ ... (no implementation details available)

