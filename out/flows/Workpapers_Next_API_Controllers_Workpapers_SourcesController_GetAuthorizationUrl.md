[web] GET /api/sources/{type}/authorization-url  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.GetAuthorizationUrl)  [L171–L177] status=200 [auth=AuthorizationPolicies.User]
  └─ uses_service UserService
    └─ method GetUserId [L174]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ uses_service IConnectionApiService (AddSingleton)
    └─ method GetApiMethods [L174]
      └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods [L20-L75]
  └─ impact_summary
    └─ services 2
      └─ IConnectionApiService
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

