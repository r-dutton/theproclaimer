[web] GET /api/ui/sync-configuration/{id:Guid}/errors  (Dataverse.Api.Controllers.UI.SyncConfigurationController.GetErrors)  [L79–L110] status=200 [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.ReadQuery [L89]
  └─ query SyncConfiguration [L89]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ SyncConfiguration writes=0 reads=1

