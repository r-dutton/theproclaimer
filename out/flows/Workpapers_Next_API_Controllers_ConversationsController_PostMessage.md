[web] POST /api/conversations/{id:guid}/message  (Workpapers.Next.API.Controllers.ConversationsController.PostMessage)  [L48–L57]
  └─ calls ConversationRepository.WriteQuery [L51]
  └─ writes_to Conversation [L51]
    └─ reads_from Conversations
  └─ uses_service IControlledRepository<Conversation>
    └─ method WriteQuery [L51]
  └─ uses_service UserService
    └─ method GetUserId [L54]

