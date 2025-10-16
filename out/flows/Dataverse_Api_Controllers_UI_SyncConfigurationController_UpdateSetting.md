[web] PUT /api/ui/sync-configuration/{id:Guid}/setting  (Dataverse.Api.Controllers.UI.SyncConfigurationController.UpdateSetting)  [L130–L140] status=200 [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L133]
  └─ write SyncConfiguration [L133]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L133]
      └─ ... (no implementation details available)

