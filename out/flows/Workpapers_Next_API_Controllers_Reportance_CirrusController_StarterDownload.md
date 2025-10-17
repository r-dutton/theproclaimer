[web] GET /api/reportance/cirrus/starters/{id:Guid}/download  (Workpapers.Next.API.Controllers.Reportance.CirrusController.StarterDownload)  [L71–L99] status=200
  └─ uses_client KeenClient [L90]
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L97]
      └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl [L17-L282]
  └─ uses_service UnitOfWork
    └─ method Table [L75]
      └─ implementation UnitOfWork.Table
  └─ uses_storage StorageService.CreateDownloadUrl [L97]
  └─ sends_request GetProductQuery -> GetProductQueryHandler [L82]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.GetProductQueryHandler.Handle [L57–L101]
      └─ uses_service UnitOfWork
        └─ method Table [L71]
          └─ implementation UnitOfWork.Table (see previous expansion)
  └─ impact_summary
    └─ clients 1
      └─ KeenClient
    └─ services 2
      └─ StorageService
      └─ UnitOfWork
    └─ requests 1
      └─ GetProductQuery
    └─ handlers 1
      └─ GetProductQueryHandler
    └─ storages 1
      └─ StorageService

