[web] DELETE /api/productsets/{id:int}  (Workpapers.Next.API.Controllers.ProductSetsController.DeleteProductSet)  [L101–L115] [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to ProductSetDto [L114]
  └─ uses_service IMapper
    └─ method Map [L114]
  └─ uses_service UnitOfWork
    └─ method Table [L105]

