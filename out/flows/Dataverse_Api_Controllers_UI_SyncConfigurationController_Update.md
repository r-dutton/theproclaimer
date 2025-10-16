[web] PUT /api/ui/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.UI.SyncConfigurationController.Update)  [L112–L128] status=200 [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L115]
  └─ write SyncConfiguration [L115]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L115]
      └─ ... (no implementation details available)

