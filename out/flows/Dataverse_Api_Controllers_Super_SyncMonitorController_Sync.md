[web] POST /api/super/sync-monitor/{id}/sync  (Dataverse.Api.Controllers.Super.SyncMonitorController.Sync)  [L119–L159] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository.Remove [L155]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L152]
  └─ calls DataverseEntityFailureLogRepository.ReadQuery [L124]
  └─ queries DataverseEntityFailureLog [L124]
    └─ reads_from DataverseEntityFailureLogs
  └─ writes_to DataverseEntityFailureLog [L155]
    └─ reads_from DataverseEntityFailureLogs
  └─ writes_to DataverseEntityFailureLog [L152]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method ReadQuery [L124]
  └─ uses_service MockMessagingService
    └─ method RequestSharePointSiteUpdate [L136]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L122]
  └─ sends_request BulkPropagateCommand [L158]
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

