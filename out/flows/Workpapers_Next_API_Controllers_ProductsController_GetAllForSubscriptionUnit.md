[web] GET /api/products/forsubscriptionunit/{subscriptionUnit}  (Workpapers.Next.API.Controllers.ProductsController.GetAllForSubscriptionUnit)  [L100–L108] status=200
  └─ maps_to ProductLightDto [L103]
  └─ uses_service UnitOfWork
    └─ method Table [L103]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ ProductLightDto

