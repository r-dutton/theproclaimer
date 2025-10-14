[web] GET /api/internal/users/profile  (Dataverse.Api.Controllers.Internal.Core.UsersController.Profile)  [L100–L109] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request GetUserProfileQuery [L103]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.GetUserProfileQueryHandler.Handle [L34–L129]
      └─ calls TenantRepository.ReadTable [L79]
      └─ maps_to FirmSettingsDto [L91]
        └─ automapper.registration DataverseMappingProfile (FirmSettings->FirmSettingsDto) [L120]
      └─ maps_to TenantLightDto [L118]
      └─ maps_to IntegrationSettingsDto [L104]
        └─ automapper.registration DataverseMappingProfile (IntegrationSettings->IntegrationSettingsDto) [L293]
      └─ maps_to TenantDto [L112]
      └─ maps_to NotificationDto [L120]
        └─ automapper.registration DataverseMappingProfile (Notification->NotificationDto) [L115]
      └─ maps_to UserProfileDto [L74]
        └─ automapper.registration DataverseMappingProfile (User->UserProfileDto) [L92]
        └─ automapper.registration CirrusMappingProfile (User->UserProfileDto) [L115]
        └─ automapper.registration WorkpapersMappingProfile (User->UserProfileDto) [L109]
      └─ uses_service FirmFeatureFlagService
        └─ method GetAvailableFeaturesForFirm [L96]
      └─ uses_service FirmSettingsService
        └─ method GetCurrentWorkpapersSettingsAsync [L101]
      └─ uses_service IControlledRepository<FirmSettings>
        └─ method ReadQuery [L91]
      └─ uses_service IControlledRepository<IntegrationSettings>
        └─ method ReadQuery [L104]
      └─ uses_service IControlledRepository<Notification>
        └─ method ReadQuery [L120]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L74]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L93]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L77]
      └─ uses_service UserService
        └─ method GetUserId [L73]

