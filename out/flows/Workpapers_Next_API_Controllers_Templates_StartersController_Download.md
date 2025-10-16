[web] GET /api/starters/{id:Guid}/download  (Workpapers.Next.API.Controllers.Templates.StartersController.Download)  [L131–L157] status=200
  └─ uses_client KeenClient [L148]
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L155]
      └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl [L17-L282]
  └─ uses_service UnitOfWork
    └─ method Table [L134]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetFirmId [L140]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
  └─ uses_storage StorageService.CreateDownloadUrl [L155]
  └─ sends_request GetProductQuery [L140]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.GetProductQueryHandler.Handle [L57–L101]
      └─ uses_service UnitOfWork
        └─ method Table [L71]
          └─ ... (no implementation details available)

