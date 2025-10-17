[web] DELETE /api/public/sync-services/{id}  (Dataverse.Api.Controllers.Public.SyncServicesController.Delete)  [L66–L71] status=200 [auth=Authentication.AdminPolicy]
  └─ calls SyncServiceRepository (methods: Remove,WriteQuery) [L70]
  └─ delete SyncService [L70]
    └─ reads_from SyncServices
  └─ write SyncService [L69]
    └─ reads_from SyncServices
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ SyncService writes=2 reads=0

