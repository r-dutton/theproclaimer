[web] GET /api/workpaperitems/byworkbook/{workbookId}  (Workpapers.Next.API.Controllers.WorkpaperItemsController.GetForWorkbook)  [L95–L105] status=200
  └─ maps_to WorkpaperItemDto [L98]
  └─ uses_service UnitOfWork
    └─ method Table [L98]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ WorkpaperItemDto

