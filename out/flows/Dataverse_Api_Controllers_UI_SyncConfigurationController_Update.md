[web] PUT /api/ui/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.UI.SyncConfigurationController.Update)  [L112–L128] status=200 [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L115]
  └─ write SyncConfiguration [L115]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfiguration writes=1 reads=0

