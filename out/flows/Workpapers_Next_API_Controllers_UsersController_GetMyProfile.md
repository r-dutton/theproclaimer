[web] GET /api/users/profile  (Workpapers.Next.API.Controllers.UsersController.GetMyProfile)  [L131–L147] [auth=AuthorizationPolicies.User]
  └─ maps_to UserProfileDto [L135]
    └─ automapper.registration DataverseMappingProfile (User->UserProfileDto) [L92]
    └─ automapper.registration CirrusMappingProfile (User->UserProfileDto) [L115]
    └─ automapper.registration WorkpapersMappingProfile (User->UserProfileDto) [L109]
  └─ calls UserRepository.ReadQuery [L135]
  └─ queries User [L135]
  └─ uses_service FirmFeatureFlagService
    └─ method GetAvailableFeaturesForFirm [L144]
  └─ uses_service FirmSettingsService
    └─ method GetCurrentSettings [L137]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L135]
  └─ uses_service UserService
    └─ method GetUserId [L135]
  └─ sends_request GetDataverseProfileQuery [L139]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.Central.Dataverse.Queries.GetDataverseProfileQueryHandler.Handle [L25–L66]
      └─ uses_service DataverseService
        └─ method Get [L56]
      └─ uses_service FirmSettingsService
        └─ method StoreSettings [L57]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L43]
      └─ uses_service UserService
        └─ method GetUser [L46]
  └─ returns DataverseProfileDto [L139]

