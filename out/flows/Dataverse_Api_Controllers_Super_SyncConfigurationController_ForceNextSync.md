[web] PUT /api/super/sync-configuration/sync/{id:Guid}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.ForceNextSync)  [L154–L164] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L157]
  └─ writes_to SyncConfiguration [L157]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L157]

