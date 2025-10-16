[web] PUT /api/internal/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.Update)  [L69–L76] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L72]
  └─ write SyncConfiguration [L72]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfiguration writes=1 reads=0

