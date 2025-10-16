[web] POST /api/ui/offices/{officeId:Guid}/sync  (Dataverse.Api.Controllers.UI.Core.OfficesController.SyncToAllProducts)  [L244–L269] status=201 [auth=Authentication.UserPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L264]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L262]
  └─ calls OfficeRepository.ReadQuery [L247]
  └─ query Office [L247]
    └─ reads_from Offices
  └─ delete DataverseEntityFailureLog [L264]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L262]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L256]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L254]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L262]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L247]
      └─ ... (no implementation details available)
  └─ sends_request BulkPropagateCommand [L267]
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

