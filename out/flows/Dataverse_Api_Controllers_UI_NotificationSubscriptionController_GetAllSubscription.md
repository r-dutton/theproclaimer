[web] GET /api/ui/notification-subscriptions  (Dataverse.Api.Controllers.UI.NotificationSubscriptionController.GetAllSubscription)  [L39–L45] status=200 [auth=Authentication.UserPolicy]
  └─ calls NotificationSubscriptionRepository.ReadQuery [L43]
  └─ query NotificationSubscription [L43]
    └─ reads_from NotificationSubscriptions
  └─ uses_service IControlledRepository<NotificationSubscription>
    └─ method ReadQuery [L43]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L42]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]

