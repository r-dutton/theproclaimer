[web] POST /api/internal/sync-configuration/{id:Guid}/delete-settings  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.DeleteSettings)  [L115–L125] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L118]
  └─ write SyncConfiguration [L118]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L118]
      └─ ... (no implementation details available)

