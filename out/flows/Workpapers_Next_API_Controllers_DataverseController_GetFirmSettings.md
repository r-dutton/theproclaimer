[web] GET /api/dataverse/firms/{dataverseId}/settings  (Workpapers.Next.API.Controllers.DataverseController.GetFirmSettings)  [L216–L228] status=200 [auth=AuthorizationPolicies.M2M]
  └─ maps_to WorkpaperSettingsDto [L220]
  └─ uses_service UnitOfWork
    └─ method Table [L220]
      └─ ... (no implementation details available)
  └─ uses_service FirmFeatureFlagService
    └─ method GetAvailableFeaturesForFirm [L225]
      └─ implementation Workpapers.Next.ApplicationService.Features.FeatureFlags.FirmFeatureFlagService.GetAvailableFeaturesForFirm [L14-L110]

