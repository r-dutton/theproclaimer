[web] DELETE /api/workpapers/{id:Guid}  (Workpapers.Next.API.Controllers.WorkpapersController.Delete)  [L91–L105] status=200
  └─ uses_service UnitOfWork
    └─ method Table [L94]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetFirmId [L99]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]

