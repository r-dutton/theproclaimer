[web] GET /api/productsets  (Workpapers.Next.API.Controllers.ProductSetsController.GetProductSets)  [L43–L62] status=200
  └─ maps_to ProductSetDto [L58]
  └─ uses_service UnitOfWork
    └─ method Table [L58]
      └─ implementation UnitOfWork.Table
  └─ sends_request GetProductSetsQuery -> GetProductSetsQueryHandler [L54]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Licensing.ProductSets.GetProductSetsQueryHandler.Handle [L33–L71]
      └─ uses_service UnitOfWork
        └─ method Table [L46]
          └─ implementation UnitOfWork.Table (see previous expansion)
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ requests 1
      └─ GetProductSetsQuery
    └─ handlers 1
      └─ GetProductSetsQueryHandler
    └─ mappings 1
      └─ ProductSetDto

