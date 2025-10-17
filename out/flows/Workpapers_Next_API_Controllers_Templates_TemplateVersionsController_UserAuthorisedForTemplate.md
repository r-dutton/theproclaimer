[web] GET /api/template-versions  (Workpapers.Next.API.Controllers.Templates.TemplateVersionsController.UserAuthorisedForTemplate)  [L161–L176] status=200 [auth=AuthorizationPolicies.User]
  └─ uses_service UserService
    └─ method IsSuperUser [L171]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
        └─ uses_service bool?
          └─ method IsSuperUser [L174]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ uses_service UnitOfWork
    └─ method Table [L163]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

