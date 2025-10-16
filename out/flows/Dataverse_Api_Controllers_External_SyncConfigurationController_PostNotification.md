[web] POST /api/external/sync-configuration/{id:Guid}/notification  (Dataverse.Api.Controllers.External.SyncConfigurationController.PostNotification)  [L115–L119] status=201 [auth=Authentication.AdminPolicy]
  └─ sends_request NotifyUserOfFailedSyncCommand -> NotifyUserOfFailedSyncCommandHandler [L118]
    └─ handled_by Dataverse.ApplicationService.Commands.Connections.NotifyUserOfFailedSyncCommandHandler.Handle [L33–L97]
      └─ calls UserRepository.ReadQuery [L63]
      └─ uses_service EmailSender
        └─ method SendEmailAsync [L90]
          └─ ... (no implementation details available)
      └─ uses_service BaseUrls
        └─ method Site [L67]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SyncConfiguration> (Scoped (inferred))
        └─ method WriteQuery [L57]
          └─ implementation Dataverse.Data.Repository.Sync.SyncConfigurationRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ NotifyUserOfFailedSyncCommand
    └─ handlers 1
      └─ NotifyUserOfFailedSyncCommandHandler

