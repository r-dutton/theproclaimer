[web] GET /api/public/sync-services/  (Dataverse.Api.Controllers.Public.SyncServicesController.GetAll)  [L37–L43] [auth=Authentication.AdminPolicy]
  └─ maps_to SyncServiceDto [L40]
    └─ automapper.registration DataverseMappingProfile (SyncService->SyncServiceDto) [L259]
  └─ calls SyncServiceRepository.ReadQuery [L40]
  └─ queries SyncService [L40]
    └─ reads_from SyncServices
  └─ uses_service IControlledRepository<SyncService>
    └─ method ReadQuery [L40]

