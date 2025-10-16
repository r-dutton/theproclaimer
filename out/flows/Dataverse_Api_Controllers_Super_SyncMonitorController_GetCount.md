[web] GET /api/super/sync-monitor/count  (Dataverse.Api.Controllers.Super.SyncMonitorController.GetCount)  [L84–L91] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository.ReadQuery [L90]
  └─ calls SyncConfigurationRepository.ReadQuery [L87]
  └─ query DataverseEntityFailureLog [L90]
    └─ reads_from DataverseEntityFailureLogs
  └─ query SyncConfiguration [L87]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method ReadQuery [L90]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L87]
      └─ ... (no implementation details available)

