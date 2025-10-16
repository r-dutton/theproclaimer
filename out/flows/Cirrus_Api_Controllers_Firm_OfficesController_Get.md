[web] GET /api/offices/{id}  (Cirrus.Api.Controllers.Firm.OfficesController.Get)  [L43–L49] status=200 [auth=user]
  └─ maps_to OfficeDto [L46]
    └─ automapper.registration ExternalApiMappingProfile (Office->OfficeDto) [L54]
    └─ automapper.registration WorkpapersMappingProfile (Office->OfficeDto) [L181]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeDto) [L138]
    └─ automapper.registration CirrusMappingProfile (Office->OfficeDto) [L128]
  └─ calls OfficeRepository.ReadQuery [L46]
  └─ query Office [L46]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L46]
      └─ ... (no implementation details available)

