[web] POST /api/internal/sync-configuration/{id:Guid}/notification  (Dataverse.Api.Controllers.Internal.SyncConfigurationController.Notification)  [L146–L150] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request NotifyUserOfFailedSyncCommand [L149]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Connections.NotifyUserOfFailedSyncCommandHandler.Handle [L33–L97]
      └─ uses_service BaseUrls
        └─ method Site [L67]
      └─ uses_service EmailSender
        └─ method SendEmailAsync [L90]
      └─ uses_service IControlledRepository<SyncConfiguration>
        └─ method WriteQuery [L57]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L63]

