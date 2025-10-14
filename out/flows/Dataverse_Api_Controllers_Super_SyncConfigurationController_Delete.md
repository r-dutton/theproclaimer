[web] DELETE /api/super/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.Delete)  [L190–L196] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request DeleteSyncConfigurationQuery [L193]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.SyncConfigurations.DeleteSyncConfigurationQueryHandler.Handle [L28–L68]
      └─ uses_service DataGetService
        └─ method DisconnectToken [L51]
      └─ uses_service IControlledRepository<SyncConfiguration>
        └─ method WriteQuery [L43]

