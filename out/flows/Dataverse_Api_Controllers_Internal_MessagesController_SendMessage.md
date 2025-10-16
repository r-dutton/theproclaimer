[web] POST /api/internal/messages/send-message  (Dataverse.Api.Controllers.Internal.MessagesController.SendMessage)  [L71–L77] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request SendMessageCommand [L74]
    └─ handled_by Dataverse.ApplicationService.Commands.Messaging.SendMessageCommandHandler.Handle [L55–L167]
      └─ maps_to UserUltraLightDto [L85]
      └─ uses_service UserService
        └─ method GetUserAsync [L84]
          └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L86]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
          └─ implementation Dataverse.Services.Features.Tenants.TenantService.GetCurrentTenantAsync [L6-L27]
      └─ uses_service MockMessagingService
        └─ method RequestNotificationMessage [L125]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<Message>
        └─ method Add [L113]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L83]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L85]
          └─ ... (no implementation details available)

