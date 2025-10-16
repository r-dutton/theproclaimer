[web] PUT /api/internal/sync-configuration/{id:Guid}/error  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.AddOrUpdateError)  [L127–L144] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ insert SyncConfigurationError [L138]
    └─ reads_from SyncConfigurationErrors
  └─ write SyncConfigurationError [L130]
    └─ reads_from SyncConfigurationErrors
  └─ uses_service IControlledRepository<SyncConfigurationError>
    └─ method WriteQuery [L130]
      └─ ... (no implementation details available)

