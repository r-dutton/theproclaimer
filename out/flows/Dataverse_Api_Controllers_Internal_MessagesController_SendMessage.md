[web] POST /api/internal/messages/send-message  (Dataverse.Api.Controllers.Internal.MessagesController.SendMessage)  [L71–L77] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request SendMessageCommand [L74]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Commands.Messaging.SendMessageCommandHandler.Handle [L55–L167]
      └─ maps_to UserUltraLightDto [L85]
      └─ uses_service IControlledRepository<Message>
        └─ method Add [L113]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L83]
      └─ uses_service IMapper
        └─ method Map [L85]
      └─ uses_service MockMessagingService
        └─ method RequestNotificationMessage [L125]
      └─ uses_service TenantService
        └─ method GetCurrentTenantAsync [L86]
      └─ uses_service UserService
        └─ method GetUserAsync [L84]

