[web] PUT /api/productsets/{id:int}/addproduct/{productId:Guid}  (Workpapers.Next.API.Controllers.ProductSetsController.AddProduct)  [L117–L134] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_service UnitOfWork
    └─ method Table [L121]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

