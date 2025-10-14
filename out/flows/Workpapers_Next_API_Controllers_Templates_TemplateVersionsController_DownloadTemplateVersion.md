[web] GET /api/template-versions/{id:Guid}/download  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.DownloadTemplateVersion)  [L74–L82] [auth=AuthorizationPolicies.User,AuthorizationPolicies.Administrator]
  └─ uses_service StorageService
    └─ method CreateDownloadUrl [L81]
  └─ uses_service UnitOfWork
    └─ method Table [L78]

