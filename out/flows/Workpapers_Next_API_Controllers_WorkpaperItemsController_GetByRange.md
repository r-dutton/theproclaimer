[web] GET /api/workpaperitems/byworkbook/{workbookId}/{worksheetId}/byrangename/{rangeName}  (Workpapers.Next.API.Controllers.WorkpaperItemsController.GetByRange)  [L151–L164] status=200
  └─ maps_to WorkpaperItemDto [L154]
  └─ uses_service UnitOfWork
    └─ method Table [L154]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ WorkpaperItemDto

