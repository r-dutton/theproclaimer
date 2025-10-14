[web] POST /api/workpapers/  (Workpapers.Next.API.Controllers.WorkpapersController.Post)  [L75–L81]
  └─ uses_service UserService
    └─ method GetFirmId [L78]
  └─ sends_request CreateUpdateWorkpaperCommand [L78]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Data.Commands.Workpapers.CreateUpdateWorkpaperCommandHandler.Handle [L14–L65]
      └─ uses_service UnitOfWork
        └─ method Table [L29]

