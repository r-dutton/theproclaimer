[web] PUT /api/internal/sync-configuration/sync/{id:Guid}  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.ForceNextSync)  [L78–L86] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L81]
  └─ write SyncConfiguration [L81]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfiguration writes=1 reads=0

