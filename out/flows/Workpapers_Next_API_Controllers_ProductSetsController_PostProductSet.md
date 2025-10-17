[web] POST /api/productsets  (Workpapers.Next.API.Controllers.ProductSetsController.PostProductSet)  [L90–L99] status=201 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to ProductSetDto [L98]
  └─ uses_service UnitOfWork
    └─ method Add [L96]
      └─ implementation UnitOfWork.Add
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ ProductSetDto

