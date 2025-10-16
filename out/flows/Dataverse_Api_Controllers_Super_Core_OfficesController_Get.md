[web] GET /api/super/offices/{id:Guid}  (Dataverse.Api.Controllers.Super.Core.OfficesController.Get)  [L65–L70] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to OfficeDto [L68]
    └─ automapper.registration ExternalApiMappingProfile (Office->OfficeDto) [L54]
    └─ automapper.registration WorkpapersMappingProfile (Office->OfficeDto) [L181]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeDto) [L138]
    └─ automapper.registration CirrusMappingProfile (Office->OfficeDto) [L128]
  └─ calls OfficeRepository.ReadQuery [L68]
  └─ query Office [L68]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L68]
      └─ ... (no implementation details available)

