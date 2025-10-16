[web] GET /api/ui/offices/  (Dataverse.Api.Controllers.UI.Core.OfficesController.GetAll)  [L159–L179] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to OfficeLightDto [L173]
    └─ automapper.registration WorkpapersMappingProfile (Office->OfficeLightDto) [L179]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeLightDto) [L141]
    └─ automapper.registration CirrusMappingProfile (Office->OfficeLightDto) [L129]
  └─ calls OfficeRepository.ReadQuery [L173]
  └─ query Office [L173]
    └─ reads_from Offices
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettingsAsync [L167]
      └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentSettingsAsync [L18-L97]
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L173]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsInRole [L162]
      └─ implementation Dataverse.ApplicationService.Services.UserService.IsInRole [L15-L185]

