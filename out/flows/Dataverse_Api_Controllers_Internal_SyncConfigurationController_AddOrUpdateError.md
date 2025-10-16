[web] PUT /api/internal/sync-configuration/{id:Guid}/error  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.AddOrUpdateError)  [L127–L144] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationErrorsRepository (methods: Add,WriteQuery) [L138]
  └─ insert SyncConfigurationError [L138]
    └─ reads_from SyncConfigurationErrors
  └─ write SyncConfigurationError [L130]
    └─ reads_from SyncConfigurationErrors
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ SyncConfigurationError writes=2 reads=0

