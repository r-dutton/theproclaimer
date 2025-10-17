[web] GET /api/internal/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.GetById)  [L62–L67] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SyncConfigurationInsecureDto [L65]
    └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationInsecureDto) [L283]
  └─ calls SyncConfigurationRepository.ReadQuery [L65]
  └─ query SyncConfiguration [L65]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ SyncConfiguration writes=0 reads=1
    └─ mappings 1
      └─ SyncConfigurationInsecureDto

