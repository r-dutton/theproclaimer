[web] POST /api/external/sync-configuration/{id:Guid}/notification  (Dataverse.Api.Controllers.External.SyncConfigurationController.PostNotification)  [L115–L119] status=201 [auth=Authentication.AdminPolicy]
  └─ sends_request NotifyUserOfFailedSyncCommand [L118]
    └─ handled_by Dataverse.ApplicationService.Commands.Connections.NotifyUserOfFailedSyncCommandHandler.Handle [L33–L97]
      └─ uses_service BaseUrls
        └─ method Site [L67]
          └─ ... (no implementation details available)
      └─ uses_service EmailSender
        └─ method SendEmailAsync [L90]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SyncConfiguration>
        └─ method WriteQuery [L57]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L63]
          └─ ... (no implementation details available)

