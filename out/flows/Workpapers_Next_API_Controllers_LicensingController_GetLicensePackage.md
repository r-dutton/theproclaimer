[web] GET /api/licensing  (Workpapers.Next.API.Controllers.LicensingController.GetLicensePackage)  [L49–L95] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to FirmWithSubscriptionsDto (var Firm) [L86]
    └─ converts_to FirmWithStatsDto [L170]
  └─ maps_to UserDto (var User) [L85]
  └─ uses_service UserService
    └─ method GetUserId [L53]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ uses_service UnitOfWork
    └─ method Table [L52]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 2
      └─ FirmWithSubscriptionsDto
      └─ UserDto

