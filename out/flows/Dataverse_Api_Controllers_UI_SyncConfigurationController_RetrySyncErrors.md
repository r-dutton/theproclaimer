[web] PUT /api/ui/sync-configuration/{id:Guid}/retry  (Dataverse.Api.Controllers.UI.SyncConfigurationController.RetrySyncErrors)  [L179–L189] status=200 [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L182]
  └─ write SyncConfiguration [L182]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L182]
      └─ ... (no implementation details available)

