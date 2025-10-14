[web] DELETE /api/public/sync-services/{id}  (Dataverse.Api.Controllers.Public.SyncServicesController.Delete)  [L66–L71] [auth=Authentication.AdminPolicy]
  └─ calls SyncServiceRepository.Remove [L70]
  └─ calls SyncServiceRepository.WriteQuery [L69]
  └─ writes_to SyncService [L70]
    └─ reads_from SyncServices
  └─ writes_to SyncService [L69]
    └─ reads_from SyncServices
  └─ uses_service IControlledRepository<SyncService>
    └─ method WriteQuery [L69]

