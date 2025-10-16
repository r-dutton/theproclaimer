[web] PUT /api/internal/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.Update)  [L69–L76] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls SyncConfigurationRepository.WriteQuery [L72]
  └─ write SyncConfiguration [L72]
    └─ reads_from SyncConfigurations
  └─ uses_service IControlledRepository<SyncConfiguration>
    └─ method WriteQuery [L72]
      └─ ... (no implementation details available)

