[web] GET /api/workpaperitems/byworkbook/{workbookId}/detailed  (Workpapers.Next.API.Controllers.WorkpaperItemsController.GetForWorkbookDetailed)  [L107–L117] status=200
  └─ maps_to WorkpaperItemWithConversationDto [L110]
  └─ uses_service UnitOfWork
    └─ method Table [L110]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ WorkpaperItemWithConversationDto

