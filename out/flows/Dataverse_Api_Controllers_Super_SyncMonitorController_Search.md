[web] GET /api/super/sync-monitor/failures  (Dataverse.Api.Controllers.Super.SyncMonitorController.Search)  [L57–L82] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository.ReadQuery [L67]
  └─ query DataverseEntityFailureLog [L67]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method ReadQuery [L67]
      └─ ... (no implementation details available)

