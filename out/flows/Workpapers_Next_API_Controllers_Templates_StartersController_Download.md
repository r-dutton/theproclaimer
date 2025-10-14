[web] GET /api/starters/{id:Guid}/download  (Workpapers.Next.API.Controllers.Templates.StartersController.Download)  [L131–L157]
  └─ uses_client KeenClient [L148]
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L155]
  └─ uses_service UnitOfWork
    └─ method Table [L134]
  └─ uses_service UserService
    └─ method GetFirmId [L140]
  └─ sends_request GetProductQuery [L140]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.GetProductQueryHandler.Handle [L57–L101]
      └─ uses_service UnitOfWork
        └─ method Table [L71]

