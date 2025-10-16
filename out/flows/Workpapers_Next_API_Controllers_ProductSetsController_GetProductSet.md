[web] GET /api/productsets/{id:int}  (Workpapers.Next.API.Controllers.ProductSetsController.GetProductSet)  [L64–L74] status=200
  └─ maps_to ProductSetDto [L67]
  └─ uses_service UnitOfWork
    └─ method Table [L67]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ ProductSetDto

