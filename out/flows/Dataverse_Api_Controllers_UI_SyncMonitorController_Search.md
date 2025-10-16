[web] GET /api/ui/sync-monitor/failures  (Dataverse.Api.Controllers.UI.SyncMonitorController.Search)  [L36–L61] status=200 [auth=Authentication.AdminPolicy]
  └─ calls DataverseEntityFailureLogRepository.ReadQuery [L46]
  └─ query DataverseEntityFailureLog [L46]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method ReadQuery [L46]
      └─ ... (no implementation details available)

