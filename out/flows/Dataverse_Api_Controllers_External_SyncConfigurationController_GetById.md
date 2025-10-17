[web] GET /api/external/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.External.SyncConfigurationController.GetById)  [L72–L79] status=200 [auth=Authentication.AdminPolicy]
  └─ maps_to SyncConfigurationInsecureDto [L75]
    └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationInsecureDto) [L283]
  └─ calls SyncConfigurationRepository.ReadQuery [L75]
  └─ query SyncConfiguration [L75]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ SyncConfiguration writes=0 reads=1
    └─ mappings 1
      └─ SyncConfigurationInsecureDto

