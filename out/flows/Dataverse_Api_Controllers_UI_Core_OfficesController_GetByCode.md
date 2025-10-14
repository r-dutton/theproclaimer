[web] GET /api/ui/offices/code/{code}  (Dataverse.Api.Controllers.UI.Core.OfficesController.GetByCode)  [L92–L100] [auth=Authentication.UserPolicy]
  └─ maps_to OfficeDto [L95]
    └─ automapper.registration ExternalApiMappingProfile (Office->OfficeDto) [L54]
    └─ automapper.registration WorkpapersMappingProfile (Office->OfficeDto) [L181]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeDto) [L137]
    └─ automapper.registration CirrusMappingProfile (Office->OfficeDto) [L128]
  └─ calls OfficeRepository.ReadQuery [L95]
  └─ queries Office [L95]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L95]

