[web] GET /api/users/profile  (Workpapers.Next.API.Controllers.UsersController.GetMyProfile)  [L131–L147] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to UserProfileDto [L135]
    └─ automapper.registration DataverseMappingProfile (User->UserProfileDto) [L93]
    └─ automapper.registration CirrusMappingProfile (User->UserProfileDto) [L115]
    └─ automapper.registration WorkpapersMappingProfile (User->UserProfileDto) [L109]
  └─ calls UserRepository.ReadQuery [L135]
  └─ query User [L135]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettings [L137]
      └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
      └─ implementation Workpapers.Next.API.Features.FirmSettings.FirmSettingsService.GetCurrentSettings [L23-L154]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L135]
      └─ ... (no implementation details available)
  └─ uses_service FirmFeatureFlagService
    └─ method GetAvailableFeaturesForFirm [L144]
      └─ implementation Workpapers.Next.ApplicationService.Features.FeatureFlags.FirmFeatureFlagService.GetAvailableFeaturesForFirm [L14-L110]
  └─ uses_service UserService
    └─ method GetUserId [L135]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
  └─ sends_request GetDataverseProfileQuery [L139]
    └─ handled_by Cirrus.Central.Dataverse.Queries.GetDataverseProfileQueryHandler.Handle [L25–L66]
      └─ uses_service FirmSettingsService
        └─ method StoreSettings [L57]
          └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.StoreSettings [L15-L112]
      └─ uses_service UserService
        └─ method GetUser [L46]
          └─ implementation Cirrus.ApplicationService.Services.UserService.GetUser [L14-L122]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L43]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
          └─ implementation Cirrus.Central.Tenants.TenantService.GetCurrentTenant [L26-L139]
      └─ uses_service DataverseService
        └─ method Get [L56]
          └─ implementation Cirrus.Central.Features.MachineToMachine.DataverseService.Get [L14-L66]
  └─ returns DataverseProfileDto [L139]

