[web] POST /api/internal/sync-configuration/{id:Guid}/complete  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.Complete)  [L88–L101] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L95]
  └─ writes_to SyncConfiguration [L95]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L95]

