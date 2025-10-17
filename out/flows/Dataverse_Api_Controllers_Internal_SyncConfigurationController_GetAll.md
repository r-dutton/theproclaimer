[web] GET /api/internal/sync-configuration/  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.GetAll)  [L44–L50] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SyncConfigurationInsecureDto [L47]
    └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationInsecureDto) [L283]
  └─ calls SyncConfigurationRepository.ReadQuery [L47]
  └─ query SyncConfiguration [L47]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ SyncConfiguration writes=0 reads=1
    └─ mappings 1
      └─ SyncConfigurationInsecureDto

