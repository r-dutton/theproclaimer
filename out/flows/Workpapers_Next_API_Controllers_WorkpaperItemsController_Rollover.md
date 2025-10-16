[web] PUT /api/workpaperitems/rollover  (Workpapers.Next.API.Controllers.WorkpaperItemsController.Rollover)  [L225–L256] status=200
  └─ uses_service UnitOfWork
    └─ method Table [L228]
      └─ ... (no implementation details available)
  └─ sends_request RolloverWorkpaperItemCommand [L252]
    └─ handled_by Workpapers.Next.Data.Commands.WorkpaperItems.RolloverWorkpaperItemCommandHandler.Handle [L11–L29]
      └─ uses_service UnitOfWork
        └─ method Add [L25]
          └─ ... (no implementation details available)

