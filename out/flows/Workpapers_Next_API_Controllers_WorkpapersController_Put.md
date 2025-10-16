[web] PUT /api/workpapers/  (Workpapers.Next.API.Controllers.WorkpapersController.Put)  [L83–L89] status=200
  └─ uses_service UserService
    └─ method GetFirmId [L86]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request CreateUpdateWorkpaperCommand -> CreateUpdateWorkpaperCommandHandler [L86]
    └─ handled_by Workpapers.Next.Data.Commands.Workpapers.CreateUpdateWorkpaperCommandHandler.Handle [L14–L65]
      └─ uses_service UnitOfWork
        └─ method Table [L29]
          └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UserService
    └─ requests 1
      └─ CreateUpdateWorkpaperCommand
    └─ handlers 1
      └─ CreateUpdateWorkpaperCommandHandler
    └─ caches 1
      └─ IMemoryCache

