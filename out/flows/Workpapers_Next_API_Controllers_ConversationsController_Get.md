[web] GET /api/conversations/{id:guid}  (Workpapers.Next.API.Controllers.ConversationsController.Get)  [L34–L46]
  └─ maps_to ConversationDto [L37]
    └─ automapper.registration WorkpapersMappingProfile (Conversation->ConversationDto) [L394]
  └─ calls ConversationRepository.ReadQuery [L37]
  └─ queries Conversation [L37]
    └─ reads_from Conversations
  └─ uses_service IControlledRepository<Conversation>
    └─ method ReadQuery [L37]
  └─ uses_service UserService
    └─ method GetUserId [L42]

