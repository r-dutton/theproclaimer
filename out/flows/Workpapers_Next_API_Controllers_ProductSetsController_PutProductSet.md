[web] PUT /api/productsets/{id:int}  (Workpapers.Next.API.Controllers.ProductSetsController.PutProductSet)  [L76–L88] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_service UnitOfWork
    └─ method Table [L80]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

