[web] DELETE /api/public/sync-services/{id}  (Dataverse.Api.Controllers.Public.SyncServicesController.Delete)  [L66–L71] status=200 [auth=Authentication.AdminPolicy]
  └─ calls SyncServiceRepository.Remove [L70]
  └─ calls SyncServiceRepository.WriteQuery [L69]
  └─ delete SyncService [L70]
    └─ reads_from SyncServices
  └─ write SyncService [L69]
    └─ reads_from SyncServices
  └─ uses_service IControlledRepository<SyncService>
    └─ method WriteQuery [L69]
      └─ ... (no implementation details available)

