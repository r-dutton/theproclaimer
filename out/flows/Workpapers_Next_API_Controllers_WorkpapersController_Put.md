[web] PUT /api/workpapers/  (Workpapers.Next.API.Controllers.WorkpapersController.Put)  [L83–L89]
  └─ uses_service UserService
    └─ method GetFirmId [L86]
  └─ sends_request CreateUpdateWorkpaperCommand [L86]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.Data.Commands.Workpapers.CreateUpdateWorkpaperCommandHandler.Handle [L14–L65]
      └─ uses_service UnitOfWork
        └─ method Table [L29]

