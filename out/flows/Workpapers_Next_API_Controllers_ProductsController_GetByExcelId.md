[web] GET /api/products/by-excel-id/{excelId:int}  (Workpapers.Next.API.Controllers.ProductsController.GetByExcelId)  [L85–L98] status=200
  └─ uses_service UserService
    └─ method GetFirmId [L88]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ sends_request AllProductsForUserQuery -> AllProductsForUserQueryHandler [L88]
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

