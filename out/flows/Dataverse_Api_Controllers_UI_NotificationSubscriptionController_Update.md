[web] PUT /api/ui/notification-subscriptions/{productSubscriptionType}  (Dataverse.Api.Controllers.UI.NotificationSubscriptionController.Update)  [L61–L66] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CreateOrUpdateNotificationSubscriptionCommand [L65]
    └─ handled_by Dataverse.ApplicationService.Commands.NotificationSubscriptions.CreateOrUpdateNotificationSubscriptionCommandHandler.Handle [L24–L55]
      └─ uses_service UserService
        └─ method GetUserId [L36]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
      └─ uses_service IControlledRepository<NotificationSubscription>
        └─ method WriteQuery [L38]
          └─ ... (no implementation details available)

