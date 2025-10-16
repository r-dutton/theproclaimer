[web] PUT /api/internal/sync-configuration/{id:Guid}/settings  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.UpdateSettings)  [L103–L113] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L106]
  └─ write SyncConfiguration [L106]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfiguration writes=1 reads=0

