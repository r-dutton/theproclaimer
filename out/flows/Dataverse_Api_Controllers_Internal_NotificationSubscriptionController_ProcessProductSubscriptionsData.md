[web] POST /api/internal/notification-subscriptions/{productSubscriptionType}/{emailNotificationFrequency}  (Dataverse.Api.Controllers.Internal.NotificationSubscriptionController.ProcessProductSubscriptionsData)  [L30–L39] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request ProcessWorkpapersNotificationSubscriptionCommand [L35]
    └─ handled_by Dataverse.ApplicationService.Commands.NotificationSubscriptions.ProcessWorkpapersNotificationSubscriptionCommandHandler.Handle [L40–L348]
      └─ calls TenantRepository.ReadTable [L287]
      └─ calls TenantRepository.ReadTable [L186]
      └─ uses_client WorkpapersClient [L65]
      └─ uses_service EmailSender
        └─ method SendEmailAsync [L165]
          └─ ... (no implementation details available)
      └─ uses_service WorkpapersClient
        └─ method Get [L65]
          └─ ... (no implementation details available)

