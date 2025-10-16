[web] PUT /api/workpapers/  (Workpapers.Next.API.Controllers.WorkpapersController.Put)  [L83–L89] status=200
  └─ uses_service UserService
    └─ method GetFirmId [L86]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
  └─ sends_request CreateUpdateWorkpaperCommand [L86]
    └─ handled_by Workpapers.Next.Data.Commands.Workpapers.CreateUpdateWorkpaperCommandHandler.Handle [L14–L65]
      └─ uses_service UnitOfWork
        └─ method Table [L29]
          └─ ... (no implementation details available)

