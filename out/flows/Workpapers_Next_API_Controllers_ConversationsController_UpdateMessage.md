[web] PUT /api/conversations/{id:guid}/message/{messageId:guid}  (Workpapers.Next.API.Controllers.ConversationsController.UpdateMessage)  [L59–L68] status=200
  └─ calls ConversationRepository.WriteQuery [L62]
  └─ write Conversation [L62]
    └─ reads_from Conversations
  └─ uses_service UserService
    └─ method GetUserId [L65]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Conversation writes=1 reads=0
    └─ services 1
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

