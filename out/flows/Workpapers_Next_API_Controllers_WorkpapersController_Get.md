[web] GET /api/workpapers/{id:Guid}  (Workpapers.Next.API.Controllers.WorkpapersController.Get)  [L45–L49] status=200
  └─ sends_request GetWorkpaperQuery [L48]
    └─ handled_by Workpapers.Next.Data.Queries.Workpapers.GetWorkpaperQueryHandler.Handle [L30–L69]
      └─ maps_to WorkpaperDto [L43]
      └─ uses_service UnitOfWork
        └─ method Table [L43]
          └─ ... (no implementation details available)

