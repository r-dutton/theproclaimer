[web] GET /api/message-notifications/  (Workpapers.Next.API.Controllers.Workpapers.MessageNotificationController.GetAll)  [L26–L32] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetMessageNotificationsQuery [L29]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.MessageNotification.GetMessageNotificationsQueryHandler.Handle [L40–L129]
      └─ maps_to UserUltraLightDto [L116]
      └─ uses_service UserService
        └─ method GetUser [L66]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]
      └─ uses_service TenantService
        └─ method GetCurrentTenant [L65]
          └─ implementation Workpapers.Next.Services.Features.Tenants.TenantService.GetCurrentTenant [L5-L22]
            └─ uses_service TenantIdentificationService
              └─ method GetCurrentTenant [L20]
                └─ implementation Workpapers.Next.ApplicationService.Services.TenantIdentificationService.GetCurrentTenant [L15-L131]
      └─ uses_service DataverseService
        └─ method Get [L70]
          └─ implementation Workpapers.Next.Dataverse.Service.DataverseService.Get [L17-L127]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L110]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L116]
          └─ ... (no implementation details available)

