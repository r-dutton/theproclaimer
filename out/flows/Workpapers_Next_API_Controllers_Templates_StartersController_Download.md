[web] GET /api/starters/{id:Guid}/download  (Workpapers.Next.API.Controllers.Templates.StartersController.Download)  [L131–L157] status=200
  └─ uses_client KeenClient [L148]
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L155]
      └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl [L17-L282]
  └─ uses_service UserService
    └─ method GetFirmId [L140]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ uses_service UnitOfWork
    └─ method Table [L134]
      └─ implementation UnitOfWork.Table
  └─ uses_storage StorageService.CreateDownloadUrl [L155]
  └─ sends_request GetProductQuery -> GetProductQueryHandler [L140]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.GetProductQueryHandler.Handle [L57–L101]
      └─ uses_service UnitOfWork
        └─ method Table [L71]
          └─ implementation UnitOfWork.Table (see previous expansion)
  └─ impact_summary
    └─ clients 1
      └─ KeenClient
    └─ services 3
      └─ StorageService
      └─ UnitOfWork
      └─ UserService
    └─ requests 1
      └─ GetProductQuery
    └─ handlers 1
      └─ GetProductQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ storages 1
      └─ StorageService

