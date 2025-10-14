[web] PUT /api/ui/notifications/{id}  (Dataverse.Api.Controllers.UI.NotificationController.Update)  [L33–L48] [auth=Authentication.UserPolicy]
  └─ calls NotificationRepository.WriteQuery [L38]
  └─ writes_to Notification [L38]
    └─ reads_from Notifications
  └─ uses_service IControlledRepository<Notification>
    └─ method WriteQuery [L38]

