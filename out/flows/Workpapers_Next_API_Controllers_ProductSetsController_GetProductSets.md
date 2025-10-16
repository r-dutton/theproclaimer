[web] GET /api/productsets  (Workpapers.Next.API.Controllers.ProductSetsController.GetProductSets)  [L43–L62] status=200
  └─ maps_to ProductSetDto [L58]
  └─ uses_service UnitOfWork
    └─ method Table [L58]
      └─ ... (no implementation details available)
  └─ sends_request GetProductSetsQuery [L54]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.ProductSets.GetProductSetsQueryHandler.Handle [L33–L71]
      └─ uses_service UnitOfWork
        └─ method Table [L46]
          └─ ... (no implementation details available)

