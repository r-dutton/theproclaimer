[web] POST /api/super/users/{userId:Guid}/sync  (Dataverse.Api.Controllers.Super.Core.UsersController.SyncToAllProducts)  [L215–L241] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls UserRepository.ReadQuery [L219]
  └─ calls DataverseEntityFailureLogRepository.Remove [L236]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L234]
  └─ delete DataverseEntityFailureLog [L236]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L234]
    └─ reads_from DataverseEntityFailureLogs
  └─ query User [L219]
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L228]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L226]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L234]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L219]
      └─ ... (no implementation details available)
  └─ sends_request BulkPropagateCommand [L239]
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

