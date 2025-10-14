[web] PUT /api/ui/notification-subscriptions/{productSubscriptionType}  (Dataverse.Api.Controllers.UI.NotificationSubscriptionController.Update)  [L61–L66] [auth=Authentication.UserPolicy]
  └─ sends_request CreateOrUpdateNotificationSubscriptionCommand [L65]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.NotificationSubscriptions.CreateOrUpdateNotificationSubscriptionCommandHandler.Handle [L24–L55]
      └─ uses_service IControlledRepository<NotificationSubscription>
        └─ method WriteQuery [L38]
      └─ uses_service UserService
        └─ method GetUserId [L36]

