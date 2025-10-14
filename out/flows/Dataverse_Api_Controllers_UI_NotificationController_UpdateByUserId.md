[web] PUT /api/ui/notifications/by-user/{id}  (Dataverse.Api.Controllers.UI.NotificationController.UpdateByUserId)  [L50–L76] [auth=Authentication.UserPolicy]
  └─ calls NotificationRepository.ReadQuery [L64]
  └─ calls NotificationRepository.WriteQuery [L56]
  └─ queries Notification [L64]
    └─ reads_from Notifications
  └─ writes_to Notification [L56]
    └─ reads_from Notifications
  └─ uses_service IControlledRepository<Notification>
    └─ method WriteQuery [L56]
  └─ sends_request AcceptDocumentInviteCommand [L71]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.AcceptDocumentInviteCommandHandler.Handle [L31–L231]
      └─ calls TenantRepository.ReadTable [L114]
      └─ calls TenantRepository.ReadTable [L108]
      └─ calls TenantRepository.ReadTable [L104]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L92]
      └─ uses_service MockMessagingService
        └─ method RequestSharePointSiteUpdate [L211]

