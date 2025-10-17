[web] GET /api/template-versions/{id:Guid}/download  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.DownloadTemplateVersion)  [L74–L82] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.Administrator]
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L81]
      └─ implementation Workpapers.Next.Data.Storage.StorageService.CreateDownloadUrl [L17-L282]
  └─ uses_service UnitOfWork
    └─ method Table [L78]
      └─ implementation UnitOfWork.Table
  └─ uses_storage StorageService.CreateDownloadUrl [L81]
  └─ impact_summary
    └─ services 2
      └─ StorageService
      └─ UnitOfWork
    └─ storages 1
      └─ StorageService

