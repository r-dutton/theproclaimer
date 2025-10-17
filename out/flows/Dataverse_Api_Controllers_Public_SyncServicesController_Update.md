[web] PUT /api/public/sync-services/{id}  (Dataverse.Api.Controllers.Public.SyncServicesController.Update)  [L59–L64] status=200 [auth=Authentication.AdminPolicy]
  └─ calls SyncServiceRepository.WriteQuery [L62]
  └─ write SyncService [L62]
    └─ reads_from SyncServices
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncService writes=1 reads=0

