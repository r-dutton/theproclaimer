[web] GET /api/workpaperitems/byuser  (Workpapers.Next.API.Controllers.WorkpaperItemsController.GetForUser)  [L72–L83] status=200
  └─ maps_to WorkpaperItemDto [L76]
  └─ uses_service UnitOfWork
    └─ method Table [L76]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUserId [L77]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]

