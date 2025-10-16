[web] POST /api/workpapers/  (Workpapers.Next.API.Controllers.WorkpapersController.Post)  [L75–L81] status=201
  └─ uses_service UserService
    └─ method GetFirmId [L78]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
  └─ sends_request CreateUpdateWorkpaperCommand [L78]
    └─ handled_by Workpapers.Next.Data.Commands.Workpapers.CreateUpdateWorkpaperCommandHandler.Handle [L14–L65]
      └─ uses_service UnitOfWork
        └─ method Table [L29]
          └─ ... (no implementation details available)

