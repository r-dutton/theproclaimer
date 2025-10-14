[web] POST /api/super/users/{userId:Guid}/sync  (Dataverse.Api.Controllers.Super.Core.UsersController.SyncToAllProducts)  [L215–L241] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls UserRepository.ReadQuery [L219]
  └─ calls DataverseEntityFailureLogRepository.Remove [L236]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L234]
  └─ writes_to DataverseEntityFailureLog [L236]
    └─ reads_from DataverseEntityFailureLogs
  └─ writes_to DataverseEntityFailureLog [L234]
    └─ reads_from DataverseEntityFailureLogs
  └─ queries User [L219]
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L234]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L219]
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L228]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L226]
  └─ sends_request BulkPropagateCommand [L239]
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

