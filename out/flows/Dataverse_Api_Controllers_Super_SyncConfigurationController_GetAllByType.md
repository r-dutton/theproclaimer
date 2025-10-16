[web] GET /api/super/sync-configuration/type/{type}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.GetAllByType)  [L71–L83] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SyncConfigurationUltraLightDto [L76]
    └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationUltraLightDto) [L265]
  └─ calls SyncConfigurationRepository.ReadQuery [L76]
  └─ query SyncConfiguration [L76]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L76]
      └─ ... (no implementation details available)

