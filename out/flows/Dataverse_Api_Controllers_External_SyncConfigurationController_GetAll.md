[web] GET /api/external/sync-configuration/  (Dataverse.Api.Controllers.External.SyncConfigurationController.GetAll)  [L55–L70] [auth=Authentication.AdminPolicy]
  └─ maps_to SyncConfigurationInsecureDto [L58]
    └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationInsecureDto) [L282]
  └─ uses_cache IDistributedCache [L67]
    └─ method SetRecordAsync [write] [L67]
  └─ calls SyncConfigurationRepository.ReadQuery [L58]
  └─ queries SyncConfiguration [L58]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L58]
  └─ uses_service TenantService
    └─ method GetCurrentTenantAsync [L66]

