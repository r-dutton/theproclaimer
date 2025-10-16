[web] PUT /api/ui/notification-subscriptions/{productSubscriptionType}  (Dataverse.Api.Controllers.UI.NotificationSubscriptionController.Update)  [L61–L66] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request CreateOrUpdateNotificationSubscriptionCommand -> CreateOrUpdateNotificationSubscriptionCommandHandler [L65]
    └─ handled_by Dataverse.ApplicationService.Commands.NotificationSubscriptions.CreateOrUpdateNotificationSubscriptionCommandHandler.Handle [L24–L55]
      └─ uses_service IControlledRepository<NotificationSubscription> (Scoped (inferred))
        └─ method WriteQuery [L38]
          └─ implementation Dataverse.Data.Repository.NotificationSubscriptions.NotificationSubscriptionRepository.WriteQuery
      └─ uses_service UserService
        └─ method GetUserId [L36]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserId [L15-L185]
            └─ uses_service User
              └─ method GetUserId [L43]
                └─ implementation Dataverse.DomainModel.Model.Users.User.GetUserId [L28-L619]
                └─ implementation Dataverse.Dtos.IManage.User.GetUserId [L21-L27]
            └─ uses_service Guid?
              └─ method GetUserId [L40]
                └─ ... (no implementation details available)
  └─ impact_summary
    └─ requests 1
      └─ CreateOrUpdateNotificationSubscriptionCommand
    └─ handlers 1
      └─ CreateOrUpdateNotificationSubscriptionCommandHandler

