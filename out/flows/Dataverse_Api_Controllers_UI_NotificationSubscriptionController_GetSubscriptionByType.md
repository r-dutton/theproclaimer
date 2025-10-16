[web] GET /api/ui/notification-subscriptions/{productSubscriptionType}  (Dataverse.Api.Controllers.UI.NotificationSubscriptionController.GetSubscriptionByType)  [L50–L56] status=200 [auth=Authentication.UserPolicy]
  └─ calls NotificationSubscriptionRepository.ReadQuery [L54]
  └─ query NotificationSubscription [L54]
    └─ reads_from NotificationSubscriptions
  └─ uses_service IControlledRepository<NotificationSubscription>
    └─ method ReadQuery [L54]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L53]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]

