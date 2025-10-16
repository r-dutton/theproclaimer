[web] DELETE /api/internal/sync-configuration/{id:Guid}/error  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.DeleteErrors)  [L152–L166] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationErrorsRepository.WriteQuery [L160]
  └─ write SyncConfigurationError [L160]
    └─ reads_from SyncConfigurationErrors
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfigurationError writes=1 reads=0

