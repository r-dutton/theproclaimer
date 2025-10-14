[web] GET /api/public/sync-services/{id}  (Dataverse.Api.Controllers.Public.SyncServicesController.Get)  [L45–L50] [auth=Authentication.AdminPolicy]
  └─ maps_to SyncServiceDto [L48]
    └─ automapper.registration DataverseMappingProfile (SyncService->SyncServiceDto) [L259]
  └─ calls SyncServiceRepository.ReadQuery [L48]
  └─ queries SyncService [L48]
    └─ reads_from SyncServices
  └─ uses_service IControlledRepository<SyncService>
    └─ method ReadQuery [L48]

