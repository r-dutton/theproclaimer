[web] POST /api/super/offices/{officeId:Guid}/sync  (Dataverse.Api.Controllers.Super.Core.OfficesController.SyncToAllProducts)  [L75–L100] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L95]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L93]
  └─ calls OfficeRepository.ReadQuery [L78]
  └─ queries Office [L78]
    └─ reads_from Offices
  └─ writes_to DataverseEntityFailureLog [L95]
    └─ reads_from DataverseEntityFailureLogs
  └─ writes_to DataverseEntityFailureLog [L93]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L93]
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L78]
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L87]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L85]
  └─ sends_request BulkPropagateCommand [L98]
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

