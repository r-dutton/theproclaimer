[web] GET /api/reportsettings/reporttemplates/{id:Guid}/download  (Workpapers.Next.API.Controllers.Reportance.ReportTemplatesController.Download)  [L129–L142] status=200
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L140]
      └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl [L17-L282]
  └─ uses_service UnitOfWork
    └─ method Table [L132]
      └─ implementation UnitOfWork.Table
  └─ uses_storage StorageService.CreateDownloadUrl [L140]
  └─ impact_summary
    └─ services 2
      └─ StorageService
      └─ UnitOfWork
    └─ storages 1
      └─ StorageService

