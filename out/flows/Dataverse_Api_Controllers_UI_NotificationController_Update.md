[web] PUT /api/ui/notifications/{id}  (Dataverse.Api.Controllers.UI.NotificationController.Update)  [L33–L48] status=200 [auth=Authentication.UserPolicy]
  └─ calls NotificationRepository.WriteQuery [L38]
  └─ write Notification [L38]
    └─ reads_from Notifications
  └─ uses_service IControlledRepository<Notification>
    └─ method WriteQuery [L38]
      └─ ... (no implementation details available)

