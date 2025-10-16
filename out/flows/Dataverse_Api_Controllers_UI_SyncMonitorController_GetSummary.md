[web] GET /api/ui/sync-monitor/summary  (Dataverse.Api.Controllers.UI.SyncMonitorController.GetSummary)  [L63–L87] status=200 [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.ReadQuery [L68]
  └─ calls DataverseEntityFailureLogRepository.ReadQuery [L66]
  └─ query SyncConfiguration [L68]
    └─ reads_from SyncConfigurations
  └─ query DataverseEntityFailureLog [L66]
    └─ reads_from DataverseEntityFailureLogs
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ DataverseEntityFailureLog writes=0 reads=1
      └─ SyncConfiguration writes=0 reads=1

