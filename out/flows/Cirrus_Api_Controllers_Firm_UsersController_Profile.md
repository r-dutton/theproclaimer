[web] GET /api/users/profile  (Cirrus.Api.Controllers.Firm.UsersController.Profile)  [L59–L76] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to ReportSettingsLightDto [L68]
    └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsLightDto) [L532]
  └─ maps_to FirmLightDto [L73]
  └─ maps_to UserProfileDto [L62]
    └─ automapper.registration DataverseMappingProfile (User->UserProfileDto) [L93]
    └─ automapper.registration CirrusMappingProfile (User->UserProfileDto) [L115]
    └─ automapper.registration WorkpapersMappingProfile (User->UserProfileDto) [L109]
  └─ calls ReportSettingsRepository.ReadQuery [L68]
  └─ calls UserRepository.ReadQuery [L62]
  └─ query ReportSettings [L68]
    └─ reads_from ReportSettings
  └─ query User [L62]
  └─ uses_service IControlledRepository<ReportSettings>
    └─ method ReadQuery [L68]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L62]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L73]
      └─ ... (no implementation details available)
  └─ uses_service IUserService (InstancePerLifetimeScope)
    └─ method GetUserId [L64]
      └─ implementation IUserService.GetUserId [L18-L18]
      └─ ... (no implementation details available)
  └─ sends_request GetFirmForCurrentRequestQuery [L73]
    └─ handled_by Cirrus.Central.Queries.GetFirmForCurrentRequestQueryHandler.Handle [L19–L55]
      └─ uses_service IHttpContextAccessor
        └─ method HttpContext [L38]
          └─ ... (no implementation details available)
      └─ uses_service ILogger<GetFirmForCurrentRequestQueryHandler>
        └─ method LogWarning [L47]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method Process [L51]
          └─ implementation IRequestProcessor.Process [L9-L9]
          └─ ... (no implementation details available)
      └─ logs ILogger<GetFirmForCurrentRequestQueryHandler> [Warning] [L47]

