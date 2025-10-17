[web] GET /api/matters  (Workpapers.Next.API.Controllers.Workpapers.MattersController.GetParams)  [L232–L240] status=200 [auth=AuthorizationPolicies.User]
  └─ uses_service UserService
    └─ method GetUserId [L239]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ uses_service IControlledRepository<MatterStatus> (AddScoped)
    └─ method ReadQuery [L235]
      └─ ... (no implementation details available)
  └─ impact_summary
    └─ services 2
      └─ IControlledRepository<MatterStatus>
      └─ UserService
    └─ caches 1
      └─ IMemoryCache

