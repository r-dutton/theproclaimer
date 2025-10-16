[web] GET /api/ui/offices/{id}  (Dataverse.Api.Controllers.UI.Core.OfficesController.Get)  [L77–L83] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to OfficeDto [L80]
    └─ automapper.registration ExternalApiMappingProfile (Office->OfficeDto) [L54]
    └─ automapper.registration WorkpapersMappingProfile (Office->OfficeDto) [L181]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeDto) [L138]
    └─ automapper.registration CirrusMappingProfile (Office->OfficeDto) [L128]
  └─ calls OfficeRepository.ReadQuery [L80]
  └─ query Office [L80]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L80]
      └─ ... (no implementation details available)

