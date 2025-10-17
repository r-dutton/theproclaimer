[web] PUT /api/products/{id:Guid}  (Workpapers.Next.API.Controllers.ProductsController.Put)  [L110–L124] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_service UnitOfWork
    └─ method Table [L114]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

