[web] GET /api/ui/sync-monitor/summary  (Dataverse.Api.Controllers.UI.SyncMonitorController.GetSummary)  [L63–L87] [auth=Authentication.AdminPolicy]
  └─ calls DataverseEntityFailureLogRepository.ReadQuery [L66]
  └─ calls SyncConfigurationRepository.ReadQuery [L68]
  └─ queries DataverseEntityFailureLog [L66]
    └─ reads_from DataverseEntityFailureLogs
  └─ queries SyncConfiguration [L68]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method ReadQuery [L66]
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L68]

