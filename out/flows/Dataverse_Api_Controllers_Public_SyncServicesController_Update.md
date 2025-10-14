[web] PUT /api/public/sync-services/{id}  (Dataverse.Api.Controllers.Public.SyncServicesController.Update)  [L59–L64] [auth=Authentication.AdminPolicy]
  └─ calls SyncServiceRepository.WriteQuery [L62]
  └─ writes_to SyncService [L62]
    └─ reads_from SyncServices
  └─ uses_service IControlledRepository<SyncService>
    └─ method WriteQuery [L62]

