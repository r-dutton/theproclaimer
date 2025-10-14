[web] GET /api/ui/sync-configuration/type/{type}  (Dataverse.Api.Controllers.UI.SyncConfigurationController.GetAllByType)  [L65–L77] [auth=Authentication.AdminPolicy]
  └─ maps_to SyncConfigurationUltraLightDto [L70]
    └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationUltraLightDto) [L264]
  └─ calls SyncConfigurationRepository.ReadQuery [L70]
  └─ queries SyncConfiguration [L70]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L70]

