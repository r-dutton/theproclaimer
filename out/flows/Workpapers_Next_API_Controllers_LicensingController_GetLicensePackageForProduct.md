[web] GET /api/licensing/{productExcelId:int}  (Workpapers.Next.API.Controllers.LicensingController.GetLicensePackageForProduct)  [L97–L130] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to FirmDto (var Firm) [L106]
    └─ converts_to FirmWithStatsDto [L162]
  └─ maps_to UserDto (var User) [L105]
  └─ uses_service UserService
    └─ method GetUserId [L100]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ uses_service UnitOfWork
    └─ method Table [L100]
      └─ implementation UnitOfWork.Table
  └─ sends_request GetProductQuery -> GetProductQueryHandler [L113]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.GetProductQueryHandler.Handle [L57–L101]
      └─ uses_service UnitOfWork
        └─ method Table [L71]
          └─ implementation UnitOfWork.Table (see previous expansion)
  └─ impact_summary
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ requests 1
      └─ GetProductQuery
    └─ handlers 1
      └─ GetProductQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 2
      └─ FirmDto
      └─ UserDto

