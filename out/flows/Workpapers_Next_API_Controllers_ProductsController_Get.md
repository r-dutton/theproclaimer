[web] GET /api/products/{id:Guid}  (Workpapers.Next.API.Controllers.ProductsController.Get)  [L70–L83]
  └─ uses_service UserService
    └─ method GetFirmId [L73]
  └─ sends_request AllProductsForUserQuery [L73]
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

