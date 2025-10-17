[web] DELETE /api/ui/sync-configuration/{id:Guid}/setting  (Dataverse.Api.Controllers.UI.SyncConfigurationController.DeleteSetting)  [L158–L165] status=200 [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L161]
  └─ write SyncConfiguration [L161]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ SyncConfiguration writes=1 reads=0

