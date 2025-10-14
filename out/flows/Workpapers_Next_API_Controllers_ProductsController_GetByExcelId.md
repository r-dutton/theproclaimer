[web] GET /api/products/by-excel-id/{excelId:int}  (Workpapers.Next.API.Controllers.ProductsController.GetByExcelId)  [L85–L98]
  └─ uses_service UserService
    └─ method GetFirmId [L88]
  └─ sends_request AllProductsForUserQuery [L88]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.Products.AllProductsForUserQueryHandler.Handle [L31–L65]
      └─ maps_to FirmWithSubscriptionsDto [L49]
        └─ converts_to FirmWithStatsDto [L170]
      └─ maps_to ProductLightDto [L45]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L46]
      └─ uses_service UnitOfWork
        └─ method Table [L45]

