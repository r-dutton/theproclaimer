[web] GET /api/users/profile  (Cirrus.Api.Controllers.Firm.UsersController.Profile)  [L59–L76] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to FirmLightDto [L73]
  └─ maps_to ReportSettingsLightDto [L68]
    └─ automapper.registration CirrusMappingProfile (ReportSettings->ReportSettingsLightDto) [L532]
  └─ maps_to UserProfileDto [L62]
    └─ automapper.registration CirrusMappingProfile (User->UserProfileDto) [L115]
  └─ calls ReportSettingsRepository.ReadQuery [L68]
  └─ calls UserRepository.ReadQuery [L62]
  └─ query ReportSettings [L68]
    └─ reads_from ReportSettings
  └─ query User [L62]
  └─ uses_service IUserService (InstancePerLifetimeScope)
    └─ method GetUserId [L64]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request GetFirmForCurrentRequestQuery -> GetFirmForCurrentRequestQueryHandler [L73]
    └─ handled_by Cirrus.Central.Queries.GetFirmForCurrentRequestQueryHandler.Handle [L19–L55]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method Process [L51]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.Process [L7-L35]
      └─ logs ILogger<GetFirmForCurrentRequestQueryHandler> [Warning] [L47]
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ ReportSettings writes=0 reads=1
      └─ User writes=0 reads=1
    └─ services 1
      └─ IUserService
    └─ requests 1
      └─ GetFirmForCurrentRequestQuery
    └─ handlers 1
      └─ GetFirmForCurrentRequestQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 3
      └─ FirmLightDto
      └─ ReportSettingsLightDto
      └─ UserProfileDto

