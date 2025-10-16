[web] POST /api/workpaperitems/{workpaperItemId:Guid}/postmessage  (Workpapers.Next.API.Controllers.WorkpaperItemsController.Post)  [L174–L188] status=201
  └─ calls ConversationRepository.WriteQuery [L182]
  └─ write Conversation [L182]
    └─ reads_from Conversations
  └─ uses_service IControlledRepository<Conversation>
    └─ method WriteQuery [L182]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Table [L177]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L185]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]

