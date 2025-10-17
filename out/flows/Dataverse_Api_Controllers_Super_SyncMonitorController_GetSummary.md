[web] GET /api/super/sync-monitor/summary  (Dataverse.Api.Controllers.Super.SyncMonitorController.GetSummary)  [L93–L117] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.ReadQuery [L98]
  └─ calls DataverseEntityFailureLogRepository.ReadQuery [L96]
  └─ query SyncConfiguration [L98]
    └─ reads_from SyncConfigurations
  └─ query DataverseEntityFailureLog [L96]
    └─ reads_from DataverseEntityFailureLogs
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ DataverseEntityFailureLog writes=0 reads=1
      └─ SyncConfiguration writes=0 reads=1

