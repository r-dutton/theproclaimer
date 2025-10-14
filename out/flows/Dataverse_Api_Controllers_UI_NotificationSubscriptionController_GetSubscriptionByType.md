[web] GET /api/ui/notification-subscriptions/{productSubscriptionType}  (Dataverse.Api.Controllers.UI.NotificationSubscriptionController.GetSubscriptionByType)  [L50–L56] [auth=Authentication.UserPolicy]
  └─ calls NotificationSubscriptionRepository.ReadQuery [L54]
  └─ queries NotificationSubscription [L54]
    └─ reads_from NotificationSubscriptions
  └─ uses_service IControlledRepository<NotificationSubscription>
    └─ method ReadQuery [L54]
  └─ uses_service UserService
    └─ method GetUserId [L53]

