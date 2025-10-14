[web] GET /api/workpapers/{id:Guid}  (Workpapers.Next.API.Controllers.WorkpapersController.Get)  [L45–L49]
  └─ sends_request GetWorkpaperQuery [L48]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Data.Queries.Workpapers.GetWorkpaperQueryHandler.Handle [L30–L69]
      └─ maps_to WorkpaperDto [L43]
      └─ uses_service UnitOfWork
        └─ method Table [L43]

