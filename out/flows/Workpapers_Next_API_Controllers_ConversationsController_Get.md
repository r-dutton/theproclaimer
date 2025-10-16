[web] GET /api/conversations/{id:guid}  (Workpapers.Next.API.Controllers.ConversationsController.Get)  [L34–L46] status=200
  └─ maps_to ConversationDto [L37]
    └─ automapper.registration WorkpapersMappingProfile (Conversation->ConversationDto) [L394]
  └─ calls ConversationRepository.ReadQuery [L37]
  └─ query Conversation [L37]
    └─ reads_from Conversations
  └─ uses_service UserService
    └─ method GetUserId [L42]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Conversation writes=0 reads=1
    └─ services 1
      └─ UserService
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ ConversationDto

