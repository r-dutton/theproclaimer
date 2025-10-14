[web] GET /api/ui/notification-subscriptions  (Dataverse.Api.Controllers.UI.NotificationSubscriptionController.GetAllSubscription)  [L39–L45] [auth=Authentication.UserPolicy]
  └─ calls NotificationSubscriptionRepository.ReadQuery [L43]
  └─ queries NotificationSubscription [L43]
    └─ reads_from NotificationSubscriptions
  └─ uses_service IControlledRepository<NotificationSubscription>
    └─ method ReadQuery [L43]
  └─ uses_service UserService
    └─ method GetUserId [L42]

