[web] POST /api/workpaperitems/{workpaperItemId:Guid}/postmessage  (Workpapers.Next.API.Controllers.WorkpaperItemsController.Post)  [L174–L188] status=201
  └─ calls ConversationRepository.WriteQuery [L182]
  └─ write Conversation [L182]
    └─ reads_from Conversations
  └─ uses_service UserService
    └─ method GetUserId [L185]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ uses_service UnitOfWork
    └─ method Table [L177]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Conversation writes=1 reads=0
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

