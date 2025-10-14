[web] PUT /api/internal/Propagator/notify-change  (Dataverse.Api.Controllers.Internal.PropagatorController.NotifyChange)  [L105–L111] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request BulkPropagateCommand [L110]
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

