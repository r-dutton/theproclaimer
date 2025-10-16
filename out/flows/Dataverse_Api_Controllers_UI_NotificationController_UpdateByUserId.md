[web] PUT /api/ui/notifications/by-user/{id}  (Dataverse.Api.Controllers.UI.NotificationController.UpdateByUserId)  [L50–L76] status=200 [auth=Authentication.UserPolicy]
  └─ calls NotificationRepository.ReadQuery [L64]
  └─ calls NotificationRepository.WriteQuery [L56]
  └─ query Notification [L64]
    └─ reads_from Notifications
  └─ write Notification [L56]
    └─ reads_from Notifications
  └─ uses_service IControlledRepository<Notification>
    └─ method WriteQuery [L56]
      └─ ... (no implementation details available)
  └─ sends_request AcceptDocumentInviteCommand [L71]
    └─ handled_by Dataverse.ApplicationService.Commands.Documents.AcceptDocumentInviteCommandHandler.Handle [L31–L231]
      └─ calls TenantRepository.ReadTable [L114]
      └─ calls TenantRepository.ReadTable [L108]
      └─ calls TenantRepository.ReadTable [L104]
      └─ uses_service MockMessagingService
        └─ method RequestSharePointSiteUpdate [L211]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L92]
          └─ ... (no implementation details available)

