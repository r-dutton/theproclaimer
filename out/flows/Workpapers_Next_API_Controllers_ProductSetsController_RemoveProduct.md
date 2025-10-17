[web] PUT /api/productsets/{id:int}/removeproduct/{productId:guid}  (Workpapers.Next.API.Controllers.ProductSetsController.RemoveProduct)  [L136–L153] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_service UnitOfWork
    └─ method Table [L140]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

