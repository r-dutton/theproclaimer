[web] GET /api/offices/  (Cirrus.Api.Controllers.Firm.OfficesController.GetAll)  [L58–L73] status=200 [auth=user]
  └─ maps_to OfficeLightDto [L68]
    └─ automapper.registration WorkpapersMappingProfile (Office->OfficeLightDto) [L179]
    └─ automapper.registration DataverseMappingProfile (Office->OfficeLightDto) [L141]
    └─ automapper.registration CirrusMappingProfile (Office->OfficeLightDto) [L129]
  └─ calls OfficeRepository.ReadQuery [L68]
  └─ query Office [L68]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L68]
      └─ ... (no implementation details available)
  └─ uses_service IFirmSettingsService (AddScoped)
    └─ method GetCurrentSettings [L66]
      └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetCurrentSettings [L15-L112]
  └─ uses_service IUserService (InstancePerLifetimeScope)
    └─ method IsInRole [L61]
      └─ implementation IUserService.IsInRole [L18-L18]
      └─ ... (no implementation details available)

