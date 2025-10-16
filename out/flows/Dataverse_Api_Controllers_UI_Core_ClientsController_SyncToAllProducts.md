[web] POST /api/ui/clients/{clientId:Guid}/sync  (Dataverse.Api.Controllers.UI.Core.ClientsController.SyncToAllProducts)  [L313–L338] status=201 [auth=Authentication.UserPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L333]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L331]
  └─ calls ClientRepository.ReadQuery [L316]
  └─ delete DataverseEntityFailureLog [L333]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L331]
    └─ reads_from DataverseEntityFailureLogs
  └─ query Client [L316]
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L325]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L323]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L316]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L331]
      └─ ... (no implementation details available)
  └─ sends_request BulkPropagateCommand [L336]
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

