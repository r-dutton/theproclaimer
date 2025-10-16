[web] GET /api/internal/users/profile  (Dataverse.Api.Controllers.Internal.Core.UsersController.Profile)  [L100–L109] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetUserProfileQuery [L103]
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.GetUserProfileQueryHandler.Handle [L34–L129]
      └─ calls TenantRepository.ReadTable [L79]
      └─ maps_to FirmSettingsDto [L91]
        └─ automapper.registration DataverseMappingProfile (FirmSettings->FirmSettingsDto) [L121]
      └─ maps_to TenantLightDto [L118]
      └─ maps_to IntegrationSettingsDto [L104]
        └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L294]
      └─ maps_to TenantDto [L112]
      └─ maps_to NotificationDto [L120]
        └─ automapper.registration DataverseMappingProfile (Notification->NotificationDto) [L116]
      └─ maps_to UserProfileDto [L74]
        └─ automapper.registration DataverseMappingProfile (User->UserProfileDto) [L93]
        └─ automapper.registration CirrusMappingProfile (User->UserProfileDto) [L115]
        └─ automapper.registration WorkpapersMappingProfile (User->UserProfileDto) [L109]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentWorkpapersSettingsAsync [L101]
          └─ implementation Dataverse.ApplicationService.Services.FirmSettingsService.GetCurrentWorkpapersSettingsAsync [L18-L97]
      └─ uses_service UserService
        └─ method GetUserId [L73]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L77]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
      └─ uses_service FirmFeatureFlagService
        └─ method GetAvailableFeaturesForFirm [L96]
          └─ implementation Dataverse.ApplicationService.Features.FeatureFlags.FirmFeatureFlagService.GetAvailableFeaturesForFirm [L14-L91]
      └─ uses_service IControlledRepository<FirmSettings>
        └─ method ReadQuery [L91]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<IntegrationSettings>
        └─ method ReadQuery [L104]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Notification>
        └─ method ReadQuery [L120]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L74]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L93]
          └─ ... (no implementation details available)

