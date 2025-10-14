[web] GET /api/super/offices/  (Dataverse.Api.Controllers.Super.Core.OfficesController.GetAll)  [L56–L63] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to OfficeLightDto [L59]
    └─ automapper.registration WorkpapersMappingProfile (Office->OfficeLightDto) [L179]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeLightDto) [L140]
    └─ automapper.registration CirrusMappingProfile (Office->OfficeLightDto) [L129]
  └─ calls OfficeRepository.ReadQuery [L59]
  └─ queries Office [L59]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L59]

