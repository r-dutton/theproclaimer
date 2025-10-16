[web] GET /api/internal/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.GetById)  [L62–L67] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to SyncConfigurationInsecureDto [L65]
    └─ automapper.registration DataverseMappingProfile (SyncConfiguration->SyncConfigurationInsecureDto) [L283]
  └─ calls SyncConfigurationRepository.ReadQuery [L65]
  └─ query SyncConfiguration [L65]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L65]
      └─ ... (no implementation details available)

