[web] POST /api/ui/users/{userId:Guid}/sync  (Dataverse.Api.Controllers.UI.Core.UsersController.SyncToAllProducts)  [L339–L364] [auth=Authentication.UserPolicy]
  └─ calls UserRepository.ReadQuery [L342]
  └─ calls DataverseEntityFailureLogRepository.Remove [L359]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L357]
  └─ writes_to DataverseEntityFailureLog [L359]
    └─ reads_from DataverseEntityFailureLogs
  └─ writes_to DataverseEntityFailureLog [L357]
    └─ reads_from DataverseEntityFailureLogs
  └─ queries User [L342]
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L357]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L342]
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L351]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L349]
  └─ sends_request BulkPropagateCommand [L362]
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

