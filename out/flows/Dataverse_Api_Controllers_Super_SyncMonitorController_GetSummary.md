[web] GET /api/super/sync-monitor/summary  (Dataverse.Api.Controllers.Super.SyncMonitorController.GetSummary)  [L93–L117] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository.ReadQuery [L96]
  └─ calls SyncConfigurationRepository.ReadQuery [L98]
  └─ query DataverseEntityFailureLog [L96]
    └─ reads_from DataverseEntityFailureLogs
  └─ query SyncConfiguration [L98]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method ReadQuery [L96]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L98]
      └─ ... (no implementation details available)

