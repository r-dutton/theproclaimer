[web] PUT /api/workpaperitems/  (Workpapers.Next.API.Controllers.WorkpaperItemsController.Put)  [L190–L196] status=200
  └─ sends_request CreateUpdateWorkpaperItemCommand [L193]
    └─ handled_by Workpapers.Next.Data.Commands.WorkpaperItems.CreateUpdateWorkpaperItemCommandHandler.Handle [L15–L58]
      └─ uses_service UserService
        └─ method GetUserId [L34]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
      └─ uses_service UnitOfWork
        └─ method Add [L36]
          └─ ... (no implementation details available)

