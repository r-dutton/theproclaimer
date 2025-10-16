[web] PUT /api/workpaperitems/rollover  (Workpapers.Next.API.Controllers.WorkpaperItemsController.Rollover)  [L225–L256] status=200
  └─ uses_service UnitOfWork
    └─ method Table [L228]
      └─ implementation UnitOfWork.Table
  └─ sends_request RolloverWorkpaperItemCommand -> RolloverWorkpaperItemCommandHandler [L252]
    └─ handled_by Workpapers.Next.Data.Commands.WorkpaperItems.RolloverWorkpaperItemCommandHandler.Handle [L11–L29]
      └─ uses_service UnitOfWork
        └─ method Add [L25]
          └─ implementation UnitOfWork.Add
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ requests 1
      └─ RolloverWorkpaperItemCommand
    └─ handlers 1
      └─ RolloverWorkpaperItemCommandHandler

