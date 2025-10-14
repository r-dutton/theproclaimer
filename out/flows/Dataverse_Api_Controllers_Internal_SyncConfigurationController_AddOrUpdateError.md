[web] PUT /api/internal/sync-configuration/{id:Guid}/error  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.AddOrUpdateError)  [L127–L144] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ writes_to SyncConfigurationError [L138]
    └─ reads_from SyncConfigurationErrors
  └─ writes_to SyncConfigurationError [L130]
    └─ reads_from SyncConfigurationErrors
  └─ uses_service IControlledRepository<SyncConfigurationError>
    └─ method WriteQuery [L130]

