[web] PUT /api/template-versions/{id:Guid}  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.UpdateTemplateVersion)  [L126–L141] status=200 [auth=AuthorizationPolicies.User,AuthorizationPolicies.Administrator]
  └─ maps_to TemplateVersionDto [L140]
  └─ uses_service UserService
    └─ method GetUserId [L136]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ uses_service UnitOfWork
    └─ method Table [L130]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ TemplateVersionDto

