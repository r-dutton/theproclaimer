[web] PUT /api/ui/notifications/by-user/{id}  (Dataverse.Api.Controllers.UI.NotificationController.UpdateByUserId)  [L50–L76] status=200 [auth=Authentication.UserPolicy]
  └─ calls NotificationRepository (methods: ReadQuery,WriteQuery) [L64]
  └─ query Notification [L64]
    └─ reads_from Notifications
  └─ write Notification [L56]
    └─ reads_from Notifications
  └─ sends_request AcceptDocumentInviteCommand -> AcceptDocumentInviteCommandHandler [L71]
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.AcceptDocumentInviteCommandHandler.Handle [L31–L231]
      └─ calls TenantRepository.ReadTable [L114]
      └─ calls UserRepository.ReadQuery [L92]
      └─ uses_service MockMessagingService
        └─ method RequestSharePointSiteUpdate [L211]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ entities 1 (writes=1, reads=1)
      └─ Notification writes=1 reads=1
    └─ requests 1
      └─ AcceptDocumentInviteCommand
    └─ handlers 1
      └─ AcceptDocumentInviteCommandHandler

