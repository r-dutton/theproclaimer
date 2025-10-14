[web] GET /api/users/profile  (Cirrus.Api.Controllers.Firm.UsersController.Profile)  [L59–L76] [auth=Authentication.UserPolicy]
  └─ maps_to ReportSettingsLightDto [L68]
    └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsLightDto) [L532]
  └─ maps_to FirmLightDto [L73]
  └─ maps_to UserProfileDto [L62]
    └─ automapper.registration DataverseMappingProfile (User->UserProfileDto) [L92]
    └─ automapper.registration CirrusMappingProfile (User->UserProfileDto) [L115]
    └─ automapper.registration WorkpapersMappingProfile (User->UserProfileDto) [L109]
  └─ calls ReportSettingsRepository.ReadQuery [L68]
  └─ calls UserRepository.ReadQuery [L62]
  └─ queries ReportSettings [L68]
    └─ reads_from ReportSettings
  └─ queries User [L62]
  └─ uses_service IControlledRepository<ReportSettings>
    └─ method ReadQuery [L68]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L62]
  └─ uses_service IMapper
    └─ method Map [L73]
  └─ uses_service IUserService (InstancePerLifetimeScope)
    └─ method GetUserId [L64]
  └─ sends_request GetFirmForCurrentRequestQuery [L73]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Central.Queries.GetFirmForCurrentRequestQueryHandler.Handle [L19–L55]
      └─ uses_service IHttpContextAccessor
        └─ method HttpContext [L38]
      └─ uses_service ILogger<GetFirmForCurrentRequestQueryHandler>
        └─ method LogWarning [L47]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method Process [L51]

