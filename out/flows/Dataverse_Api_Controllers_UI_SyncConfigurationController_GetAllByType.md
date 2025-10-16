[web] GET /api/ui/sync-configuration/type/{type}  (Dataverse.Api.Controllers.UI.SyncConfigurationController.GetAllByType)  [L65–L77] status=200 [auth=Authentication.AdminPolicy]
  └─ maps_to SyncConfigurationUltraLightDto [L70]
    └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationUltraLightDto) [L265]
  └─ calls SyncConfigurationRepository.ReadQuery [L70]
  └─ query SyncConfiguration [L70]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L70]
      └─ ... (no implementation details available)

