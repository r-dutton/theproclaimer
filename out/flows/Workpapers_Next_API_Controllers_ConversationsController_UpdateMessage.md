[web] PUT /api/conversations/{id:guid}/message/{messageId:guid}  (Workpapers.Next.API.Controllers.ConversationsController.UpdateMessage)  [L59–L68] status=200
  └─ calls ConversationRepository.WriteQuery [L62]
  └─ write Conversation [L62]
    └─ reads_from Conversations
  └─ uses_service IControlledRepository<Conversation>
    └─ method WriteQuery [L62]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L65]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]

