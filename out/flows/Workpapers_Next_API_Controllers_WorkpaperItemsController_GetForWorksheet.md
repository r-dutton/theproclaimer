[web] GET /api/workpaperitems/byworkbook/{workbookId}/{worksheetId}  (Workpapers.Next.API.Controllers.WorkpaperItemsController.GetForWorksheet)  [L139–L149] status=200
  └─ maps_to WorkpaperItemDto [L142]
  └─ uses_service UnitOfWork
    └─ method Table [L142]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ WorkpaperItemDto

