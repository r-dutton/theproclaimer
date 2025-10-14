[web] GET /api/reportance/cirrus/starters/{id:Guid}/download  (Workpapers.Next.API.Controllers.Reportance.CirrusController.StarterDownload)  [L71–L99]
  └─ uses_client KeenClient [L90]
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L97]
  └─ uses_service UnitOfWork
    └─ method Table [L75]
  └─ sends_request GetProductQuery [L82]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.GetProductQueryHandler.Handle [L57–L101]
      └─ uses_service UnitOfWork
        └─ method Table [L71]

