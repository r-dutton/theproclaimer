[web] GET /api/ui/sync-configuration/{id:Guid}/errors  (Dataverse.Api.Controllers.UI.SyncConfigurationController.GetErrors)  [L79–L110] [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.ReadQuery [L89]
  └─ queries SyncConfiguration [L89]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L89]

