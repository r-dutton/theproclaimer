[web] GET /api/workpapers/{id:Guid}  (Workpapers.Next.API.Controllers.WorkpapersController.Get)  [L45–L49] status=200
  └─ sends_request GetWorkpaperQuery -> GetWorkpaperQueryHandler [L48]
    └─ handled_by Workpapers.Next.Data.Queries.Workpapers.GetWorkpaperQueryHandler.Handle [L30–L69]
      └─ maps_to WorkpaperDto [L43]
      └─ uses_service UnitOfWork
        └─ method Table [L43]
          └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ requests 1
      └─ GetWorkpaperQuery
    └─ handlers 1
      └─ GetWorkpaperQueryHandler
    └─ mappings 1
      └─ WorkpaperDto

