[web] GET /api/reportsettings/reporttemplates/{id:Guid}/download  (Workpapers.Next.API.Controllers.Reportance.ReportTemplatesController.Download)  [L129–L142] status=200
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L140]
      └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl [L17-L282]
  └─ uses_service UnitOfWork
    └─ method Table [L132]
      └─ ... (no implementation details available)
  └─ uses_storage StorageService.CreateDownloadUrl [L140]

