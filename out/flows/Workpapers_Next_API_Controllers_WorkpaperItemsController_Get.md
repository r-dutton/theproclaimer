[web] GET /api/workpaperitems/{id:guid}  (Workpapers.Next.API.Controllers.WorkpaperItemsController.Get)  [L58–L70] status=200
  └─ maps_to WorkpaperItemWithConversationDto [L61]
  └─ uses_service UnitOfWork
    └─ method Table [L61]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ WorkpaperItemWithConversationDto

