[web] GET /api/workpaperitems/byuser/count  (Workpapers.Next.API.Controllers.WorkpaperItemsController.GetCountForUser)  [L85–L93] status=200
  └─ uses_service UnitOfWork
    └─ method Table [L89]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L90]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]

