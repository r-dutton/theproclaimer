[web] PUT /api/internal/sync-configuration/{id:Guid}/settings  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.UpdateSettings)  [L103–L113] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L106]
  └─ writes_to SyncConfiguration [L106]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L106]

