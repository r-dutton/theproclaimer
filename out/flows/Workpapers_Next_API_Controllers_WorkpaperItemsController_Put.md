[web] PUT /api/workpaperitems/  (Workpapers.Next.API.Controllers.WorkpaperItemsController.Put)  [L190–L196] status=200
  └─ sends_request CreateUpdateWorkpaperItemCommand -> CreateUpdateWorkpaperItemCommandHandler [L193]
    └─ handled_by Workpapers.Next.Data.Commands.WorkpaperItems.CreateUpdateWorkpaperItemCommandHandler.Handle [L15–L58]
      └─ uses_service UnitOfWork
        └─ method Add [L36]
          └─ implementation UnitOfWork.Add
      └─ uses_service UserService
        └─ method GetUserId [L34]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
            └─ uses_service User
              └─ method GetUserId [L67]
                └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
            └─ uses_service Guid?
              └─ method GetUserId [L64]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ requests 1
      └─ CreateUpdateWorkpaperItemCommand
    └─ handlers 1
      └─ CreateUpdateWorkpaperItemCommandHandler
    └─ caches 1
      └─ IMemoryCache

