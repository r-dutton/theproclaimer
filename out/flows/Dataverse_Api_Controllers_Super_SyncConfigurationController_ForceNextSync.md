[web] PUT /api/super/sync-configuration/sync/{id:Guid}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.ForceNextSync)  [L154–L164] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L157]
  └─ write SyncConfiguration [L157]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L157]
      └─ ... (no implementation details available)

