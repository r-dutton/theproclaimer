[web] POST /api/ui/clients/{clientId:Guid}/sync  (Dataverse.Api.Controllers.UI.Core.ClientsController.SyncToAllProducts)  [L313–L338] [auth=Authentication.UserPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L333]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L331]
  └─ calls ClientRepository.ReadQuery [L316]
  └─ writes_to DataverseEntityFailureLog [L333]
    └─ reads_from DataverseEntityFailureLogs
  └─ writes_to DataverseEntityFailureLog [L331]
    └─ reads_from DataverseEntityFailureLogs
  └─ queries Client [L316]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L316]
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L331]
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L325]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L323]
  └─ sends_request BulkPropagateCommand [L336]
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

