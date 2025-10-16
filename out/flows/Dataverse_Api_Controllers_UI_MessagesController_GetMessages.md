[web] GET /api/ui/messages/  (Dataverse.Api.Controllers.UI.MessagesController.GetMessages)  [L29–L35] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetMessagesQuery -> GetMessagesQueryHandler [L32]
    └─ handled_by Dataverse.ApplicationService.Queries.Messaging.GetMessagesQueryHandler.Handle [L39–L102]
      └─ calls UserRepository.ReadQuery [L81]
      └─ maps_to UserLightDto [L99]
      └─ uses_service IControlledRepository<Message> (Scoped (inferred))
        └─ method ReadQuery [L54]
          └─ implementation Dataverse.Data.Repository.Users.MessageRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetMessagesQuery
    └─ handlers 1
      └─ GetMessagesQueryHandler
    └─ mappings 1
      └─ UserLightDto

