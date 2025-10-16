[web] GET /api/ui/sync-monitor/summary  (Dataverse.Api.Controllers.UI.SyncMonitorController.GetSummary)  [L63–L87] status=200 [auth=Authentication.AdminPolicy]
  └─ calls DataverseEntityFailureLogRepository.ReadQuery [L66]
  └─ calls SyncConfigurationRepository.ReadQuery [L68]
  └─ query DataverseEntityFailureLog [L66]
    └─ reads_from DataverseEntityFailureLogs
  └─ query SyncConfiguration [L68]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method ReadQuery [L66]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L68]
      └─ ... (no implementation details available)

