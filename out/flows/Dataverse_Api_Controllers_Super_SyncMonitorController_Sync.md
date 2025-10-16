[web] POST /api/super/sync-monitor/{id}/sync  (Dataverse.Api.Controllers.Super.SyncMonitorController.Sync)  [L119–L159] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L155]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L152]
  └─ calls DataverseEntityFailureLogRepository.ReadQuery [L124]
  └─ delete DataverseEntityFailureLog [L155]
    └─ reads_from DataverseEntityFailureLogs
  └─ query DataverseEntityFailureLog [L124]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L152]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L136]
      └─ ... (no implementation details available)
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L122]
      └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method ReadQuery [L124]
      └─ ... (no implementation details available)
  └─ sends_request BulkPropagateCommand [L158]
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

