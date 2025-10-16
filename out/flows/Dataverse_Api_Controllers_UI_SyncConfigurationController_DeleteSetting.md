[web] DELETE /api/ui/sync-configuration/{id:Guid}/setting  (Dataverse.Api.Controllers.UI.SyncConfigurationController.DeleteSetting)  [L158–L165] status=200 [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L161]
  └─ write SyncConfiguration [L161]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L161]
      └─ ... (no implementation details available)

