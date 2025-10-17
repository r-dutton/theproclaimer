[web] GET /api/ui/sync-monitor/failures  (Dataverse.Api.Controllers.UI.SyncMonitorController.Search)  [L36–L61] status=200 [auth=Authentication.AdminPolicy]
  └─ calls DataverseEntityFailureLogRepository.ReadQuery [L46]
  └─ query DataverseEntityFailureLog [L46]
    └─ reads_from DataverseEntityFailureLogs
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DataverseEntityFailureLog writes=0 reads=1

