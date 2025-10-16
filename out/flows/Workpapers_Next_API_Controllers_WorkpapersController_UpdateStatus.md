[web] PUT /api/workpapers/{id:Guid}/status  (Workpapers.Next.API.Controllers.WorkpapersController.UpdateStatus)  [L107–L121] status=200
  └─ uses_service UnitOfWork
    └─ method Table [L110]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetFirmId [L115]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]

