[web] GET /api/super/sync-configuration/type/{type}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.GetAllByType)  [L71–L83] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SyncConfigurationUltraLightDto [L76]
    └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationUltraLightDto) [L264]
  └─ calls SyncConfigurationRepository.ReadQuery [L76]
  └─ queries SyncConfiguration [L76]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L76]

