[web] DELETE /api/products/{id:Guid}  (Workpapers.Next.API.Controllers.ProductsController.Delete)  [L138–L152] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_service UnitOfWork
    └─ method Table [L142]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

