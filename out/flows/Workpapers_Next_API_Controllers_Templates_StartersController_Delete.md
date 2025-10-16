[web] DELETE /api/starters/{id}  (Workpapers.Next.API.Controllers.Templates.StartersController.Delete)  [L113–L129] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L117]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

