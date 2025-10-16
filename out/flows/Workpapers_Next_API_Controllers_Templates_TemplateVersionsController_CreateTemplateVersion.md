[web] POST /api/template-versions  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.CreateTemplateVersion)  [L95–L110] status=201 [auth=AuthorizationPolicies.User,AuthorizationPolicies.Administrator]
  └─ maps_to TemplateVersionDto [L109]
  └─ uses_service UnitOfWork
    └─ method Add [L107]
      └─ implementation UnitOfWork.Add
  └─ uses_service UserService
    └─ method GetUserId [L103]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ TemplateVersionDto

