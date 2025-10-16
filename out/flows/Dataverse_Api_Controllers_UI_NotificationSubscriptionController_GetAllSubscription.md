[web] GET /api/ui/notification-subscriptions  (Dataverse.Api.Controllers.UI.NotificationSubscriptionController.GetAllSubscription)  [L39–L45] status=200 [auth=Authentication.UserPolicy]
  └─ calls NotificationSubscriptionRepository.ReadQuery [L43]
  └─ query NotificationSubscription [L43]
    └─ reads_from NotificationSubscriptions
  └─ uses_service UserService
    └─ method GetUserId [L42]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
        └─ uses_service User
          └─ method GetUserId [L43]
            └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
            └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
        └─ uses_service Guid?
          └─ method GetUserId [L40]
            └─ ... (no implementation details available)
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ NotificationSubscription writes=0 reads=1
    └─ services 1
      └─ UserService

