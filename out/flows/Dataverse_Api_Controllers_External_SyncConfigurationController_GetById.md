[web] GET /api/external/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.External.SyncConfigurationController.GetById)  [L72–L79] [auth=Authentication.AdminPolicy]
  └─ maps_to SyncConfigurationInsecureDto [L75]
    └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationInsecureDto) [L282]
  └─ calls SyncConfigurationRepository.ReadQuery [L75]
  └─ queries SyncConfiguration [L75]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L75]

