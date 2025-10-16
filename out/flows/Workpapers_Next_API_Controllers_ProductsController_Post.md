[web] POST /api/products  (Workpapers.Next.API.Controllers.ProductsController.Post)  [L126–L136] status=201 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_service UnitOfWork
    └─ method Add [L133]
      └─ implementation UnitOfWork.Add
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

