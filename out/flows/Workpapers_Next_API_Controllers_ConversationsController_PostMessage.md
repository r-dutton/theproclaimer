[web] POST /api/conversations/{id:guid}/message  (Workpapers.Next.API.Controllers.ConversationsController.PostMessage)  [L48–L57] status=201
  └─ calls ConversationRepository.WriteQuery [L51]
  └─ write Conversation [L51]
    └─ reads_from Conversations
  └─ uses_service IControlledRepository<Conversation>
    └─ method WriteQuery [L51]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L54]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]

