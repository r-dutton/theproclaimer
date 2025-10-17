[web] POST /api/sources/{type}/create-access-token  (Workpapers.Next.API.Controllers.Workpapers.SourcesController.CreateAccessToken)  [L179–L185] status=201 [auth=AuthorizationPolicies.User]
  └─ uses_service UserService
    └─ method GetUserId [L182]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ uses_service IConnectionApiService (AddSingleton)
    └─ method GetApiMethods [L182]
      └─ implementation Workpapers.Next.ApplicationService.Features.Connections.ConnectionApiService.GetApiMethods [L20-L75]
  └─ impact_summary
    └─ services 2
      └─ IConnectionApiService
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

