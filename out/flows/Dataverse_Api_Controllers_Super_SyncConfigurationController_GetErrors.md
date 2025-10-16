[web] GET /api/super/sync-configuration/{id:Guid}/errors  (Dataverse.Api.Controllers.Super.SyncConfigurationController.GetErrors)  [L85–L116] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.ReadQuery [L95]
  └─ query SyncConfiguration [L95]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method ReadQuery [L95]
      └─ ... (no implementation details available)

