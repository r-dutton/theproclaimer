[web] POST /api/ui/users/{userId:Guid}/sync  (Dataverse.Api.Controllers.UI.Core.UsersController.SyncToAllProducts)  [L339–L364] status=201 [auth=Authentication.UserPolicy]
  └─ calls UserRepository.ReadQuery [L342]
  └─ calls DataverseEntityFailureLogRepository.Remove [L359]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L357]
  └─ delete DataverseEntityFailureLog [L359]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L357]
    └─ reads_from DataverseEntityFailureLogs
  └─ query User [L342]
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L351]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L349]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L357]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L342]
      └─ ... (no implementation details available)
  └─ sends_request BulkPropagateCommand [L362]
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

