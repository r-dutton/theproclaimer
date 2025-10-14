[web] POST /api/external/sync-configuration/{id:Guid}/complete  (Dataverse.Api.Controllers.External.SyncConfigurationController.Complete)  [L81–L94] [auth=Authentication.AdminPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L88]
  └─ writes_to SyncConfiguration [L88]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L88]

