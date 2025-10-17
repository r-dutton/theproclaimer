[web] GET /api/super/sync-configuration/{id:Guid}/errors  (Dataverse.Api.Controllers.Super.SyncConfigurationController.GetErrors)  [L85–L116] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.ReadQuery [L95]
  └─ query SyncConfiguration [L95]
    └─ reads_from SyncConfigurations
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ SyncConfiguration writes=0 reads=1

