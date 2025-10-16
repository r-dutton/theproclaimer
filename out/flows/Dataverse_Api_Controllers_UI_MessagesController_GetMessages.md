[web] GET /api/ui/messages/  (Dataverse.Api.Controllers.UI.MessagesController.GetMessages)  [L29–L35] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetMessagesQuery [L32]
    └─ handled_by Dataverse.ApplicationService.Queries.Messaging.GetMessagesQueryHandler.Handle [L39–L102]
      └─ maps_to UserLightDto [L99]
      └─ maps_to UserLightDto [L81]
        └─ automapper.registration CirrusMappingProfile (User->UserLightDto) [L104]
        └─ automapper.registration DataverseMappingProfile (User->UserLightDto) [L84]
        └─ automapper.registration WorkpapersMappingProfile (User->UserLightDto) [L97]
      └─ uses_service IControlledRepository<Message>
        └─ method ReadQuery [L54]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L81]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L83]
          └─ ... (no implementation details available)

