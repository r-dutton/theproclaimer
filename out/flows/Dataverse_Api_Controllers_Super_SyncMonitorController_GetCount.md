[web] GET /api/super/sync-monitor/count  (Dataverse.Api.Controllers.Super.SyncMonitorController.GetCount)  [L84–L91] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository.ReadQuery [L90]
  └─ calls SyncConfigurationRepository.ReadQuery [L87]
  └─ queries DataverseEntityFailureLog [L90]
    └─ reads_from DataverseEntityFailureLogs
  └─ queries SyncConfiguration [L87]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method ReadQuery [L90]
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L87]

