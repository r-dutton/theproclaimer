[web] GET /api/public/sync-services/{id}  (Dataverse.Api.Controllers.Public.SyncServicesController.Get)  [L45–L50] status=200 [auth=Authentication.AdminPolicy]
  └─ maps_to SyncServiceDto [L48]
    └─ automapper.registration DataverseMappingProfile (SyncService->SyncServiceDto) [L260]
  └─ calls SyncServiceRepository.ReadQuery [L48]
  └─ query SyncService [L48]
    └─ reads_from SyncServices
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ SyncService writes=0 reads=1
    └─ mappings 1
      └─ SyncServiceDto

