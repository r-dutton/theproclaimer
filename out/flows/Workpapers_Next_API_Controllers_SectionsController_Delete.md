[web] DELETE /api/sections/{id:Guid}  (Workpapers.Next.API.Controllers.SectionsController.Delete)  [L107–L119] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_service UnitOfWork
    └─ method Table [L111]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

