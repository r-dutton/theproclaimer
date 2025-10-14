[web] GET /api/internal/sync-configuration/  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.GetAll)  [L44–L50] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SyncConfigurationInsecureDto [L47]
    └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationInsecureDto) [L282]
  └─ calls SyncConfigurationRepository.ReadQuery [L47]
  └─ queries SyncConfiguration [L47]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L47]

