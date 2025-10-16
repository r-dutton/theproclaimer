[web] PUT /api/sections/{id:Guid}  (Workpapers.Next.API.Controllers.SectionsController.Put)  [L93–L105] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_service UnitOfWork
    └─ method Table [L97]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

