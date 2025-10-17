[web] POST /api/conversations/{id:guid}/message  (Workpapers.Next.API.Controllers.ConversationsController.PostMessage)  [L48–L57] status=201
  └─ calls ConversationRepository.WriteQuery [L51]
  └─ write Conversation [L51]
    └─ reads_from Conversations
  └─ uses_service UserService
    └─ method GetUserId [L54]
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

