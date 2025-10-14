[web] POST /api/ui/offices/{officeId:Guid}/sync  (Dataverse.Api.Controllers.UI.Core.OfficesController.SyncToAllProducts)  [L244–L269] [auth=Authentication.UserPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L264]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L262]
  └─ calls OfficeRepository.ReadQuery [L247]
  └─ queries Office [L247]
    └─ reads_from Offices
  └─ writes_to DataverseEntityFailureLog [L264]
    └─ reads_from DataverseEntityFailureLogs
  └─ writes_to DataverseEntityFailureLog [L262]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L262]
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L247]
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L256]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L254]
  └─ sends_request BulkPropagateCommand [L267]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.DataverseEntity.BulkPropagateCommandHandler.Handle [L45–L168]
      └─ calls TenantRepository.ReadTable [L159]
      └─ uses_service IControlledRepository<DataverseEntityFailureLog>
        └─ method WriteQuery [L145]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L136]
      └─ uses_service MockMessagingService
        └─ method RequestPropagation [L141]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L72]

