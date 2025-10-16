[web] POST /api/internal/sync-configuration/{id:Guid}/complete  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.Complete)  [L88–L101] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L95]
  └─ write SyncConfiguration [L95]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfiguration writes=1 reads=0

