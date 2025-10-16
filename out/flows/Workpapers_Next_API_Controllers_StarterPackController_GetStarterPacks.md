[web] GET /api/starterPacks/  (Workpapers.Next.API.Controllers.StarterPackController.GetStarterPacks)  [L31–L38] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to StarterPackDto [L34]
  └─ uses_service UnitOfWork
    └─ method Table [L34]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ StarterPackDto

