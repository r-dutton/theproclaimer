[web] DELETE /api/productsets/{id:int}  (Workpapers.Next.API.Controllers.ProductSetsController.DeleteProductSet)  [L101–L115] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to ProductSetDto [L114]
  └─ uses_service UnitOfWork
    └─ method Table [L105]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ ProductSetDto

