[web] GET /api/ui/offices/  (Dataverse.Api.Controllers.UI.Core.OfficesController.GetAll)  [L159–L179] [auth=Authentication.UserPolicy]
  └─ maps_to OfficeLightDto [L173]
    └─ automapper.registration WorkpapersMappingProfile (Office->OfficeLightDto) [L179]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeLightDto) [L140]
    └─ automapper.registration CirrusMappingProfile (Office->OfficeLightDto) [L129]
  └─ calls OfficeRepository.ReadQuery [L173]
  └─ queries Office [L173]
    └─ reads_from Offices
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L167]
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L173]
  └─ uses_service UserService
    └─ method IsInRole [L162]

