[web] DELETE /api/internal/sync-configuration/{id:Guid}/error  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.DeleteErrors)  [L152–L166] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ write SyncConfigurationError [L160]
    └─ reads_from SyncConfigurationErrors
  └─ uses_service IControlledRepository<SyncConfigurationError>
    └─ method WriteQuery [L160]
      └─ ... (no implementation details available)

