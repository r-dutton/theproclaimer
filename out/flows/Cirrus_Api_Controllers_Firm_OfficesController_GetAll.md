[web] GET /api/offices/  (Cirrus.Api.Controllers.Firm.OfficesController.GetAll)  [L58–L73] [auth=user]
  └─ maps_to OfficeLightDto [L68]
    └─ automapper.registration WorkpapersMappingProfile (Office->OfficeLightDto) [L179]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeLightDto) [L140]
    └─ automapper.registration CirrusMappingProfile (Office->OfficeLightDto) [L129]
  └─ calls OfficeRepository.ReadQuery [L68]
  └─ queries Office [L68]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L68]
  └─ uses_service IFirmSettingsService (AddScoped)
    └─ method GetCurrentSettings [L66]
  └─ uses_service IUserService (InstancePerLifetimeScope)
    └─ method IsInRole [L61]

