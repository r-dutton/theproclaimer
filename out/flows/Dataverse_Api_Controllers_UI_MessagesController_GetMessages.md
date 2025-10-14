[web] GET /api/ui/messages/  (Dataverse.Api.Controllers.UI.MessagesController.GetMessages)  [L29–L35] [auth=Authentication.UserPolicy]
  └─ sends_request GetMessagesQuery [L32]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Messaging.GetMessagesQueryHandler.Handle [L39–L102]
      └─ maps_to UserLightDto [L99]
      └─ maps_to UserLightDto [L81]
        └─ automapper.registration CirrusMappingProfile (User->UserLightDto) [L104]
        └─ automapper.registration DataverseMappingProfile (User->UserLightDto) [L83]
        └─ automapper.registration WorkpapersMappingProfile (User->UserLightDto) [L97]
      └─ uses_service IControlledRepository<Message>
        └─ method ReadQuery [L54]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L81]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L83]

