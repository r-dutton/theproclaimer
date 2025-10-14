[web] POST /api/productsets  (Workpapers.Next.API.Controllers.ProductSetsController.PostProductSet)  [L90–L99] [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to ProductSetDto [L98]
  └─ uses_service IMapper
    └─ method Map [L98]
  └─ uses_service UnitOfWork
    └─ method Add [L96]

