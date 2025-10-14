[web] GET /api/dataverse/firms/{dataverseId}/settings  (Workpapers.Next.API.Controllers.DataverseController.GetFirmSettings)  [L215–L227] [auth=AuthorizationPolicies.M2M]
  └─ maps_to WorkpaperSettingsDto [L219]
  └─ uses_service FirmFeatureFlagService
    └─ method GetAvailableFeaturesForFirm [L224]
  └─ uses_service UnitOfWork
    └─ method Table [L219]

