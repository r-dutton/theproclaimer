[web] GET /core/v1/offices/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.OfficesController.Get)  [L52–L57] status=200
  └─ maps_to OfficeDto [L55]
    └─ automapper.registration ExternalApiMappingProfile (Office->OfficeDto) [L54]
    └─ automapper.registration WorkpapersMappingProfile (Office->OfficeDto) [L181]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeDto) [L138]
    └─ automapper.registration CirrusMappingProfile (Office->OfficeDto) [L128]
  └─ calls OfficeRepository.ReadQuery [L55]
  └─ query Office [L55]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L55]
      └─ ... (no implementation details available)

