[web] GET /api/productsets  (Workpapers.Next.API.Controllers.ProductSetsController.GetProductSets)  [L43–L62]
  └─ maps_to ProductSetDto [L58]
  └─ uses_service UnitOfWork
    └─ method Table [L58]
  └─ sends_request GetProductSetsQuery [L54]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.ProductSets.GetProductSetsQueryHandler.Handle [L33–L71]
      └─ uses_service UnitOfWork
        └─ method Table [L46]

