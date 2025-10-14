[web] PUT /api/internal/sync-configuration/sync/{id:Guid}  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.ForceNextSync)  [L78–L86] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L81]
  └─ writes_to SyncConfiguration [L81]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L81]

