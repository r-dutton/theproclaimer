[web] GET /api/workpaperitems/byuser  (Workpapers.Next.API.Controllers.WorkpaperItemsController.GetForUser)  [L72–L83]
  └─ maps_to WorkpaperItemDto [L76]
  └─ uses_service UnitOfWork
    └─ method Table [L76]
  └─ uses_service UserService
    └─ method GetUserId [L77]

