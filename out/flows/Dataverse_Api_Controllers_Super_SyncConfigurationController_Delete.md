[web] DELETE /api/super/sync-configuration/{id:Guid}  (Dataverse.Api.Controllers.Super.SyncConfigurationController.Delete)  [L190–L196] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request DeleteSyncConfigurationQuery [L193]
    └─ handled_by Dataverse.ApplicationService.Queries.SyncConfigurations.DeleteSyncConfigurationQueryHandler.Handle [L28–L68]
      └─ uses_service DataGetService
        └─ method DisconnectToken [L51]
          └─ implementation Dataverse.Services.Features.DataGet.DataGetService.DisconnectToken [L19-L182]
      └─ uses_service IControlledRepository<SyncConfiguration>
        └─ method WriteQuery [L43]
          └─ ... (no implementation details available)

