[web] GET /api/super/sync-monitor/count  (Dataverse.Api.Controllers.Super.SyncMonitorController.GetCount)  [L84–L91] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository.ReadQuery [L90]
  └─ calls SyncConfigurationRepository.ReadQuery [L87]
  └─ query DataverseEntityFailureLog [L90]
    └─ reads_from DataverseEntityFailureLogs
  └─ query SyncConfiguration [L87]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ DataverseEntityFailureLog writes=0 reads=1
      └─ SyncConfiguration writes=0 reads=1

