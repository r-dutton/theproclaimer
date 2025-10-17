[web] GET /api/products/{id:Guid}  (Workpapers.Next.API.Controllers.ProductsController.Get)  [L70–L83] status=200
  └─ uses_service UserService
    └─ method GetFirmId [L73]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request AllProductsForUserQuery -> AllProductsForUserQueryHandler [L73]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.AllProductsForUserQueryHandler.Handle [L31–L65]
      └─ maps_to FirmWithSubscriptionsDto [L49]
        └─ converts_to FirmWithStatsDto [L170]
      └─ maps_to ProductLightDto [L45]
      └─ uses_service UnitOfWork
        └─ method Table [L45]
          └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UserService
    └─ requests 1
      └─ AllProductsForUserQuery
    └─ handlers 1
      └─ AllProductsForUserQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 2
      └─ FirmWithSubscriptionsDto
      └─ ProductLightDto

