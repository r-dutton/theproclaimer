[web] DELETE /api/teams/{id}  (Workpapers.Next.API.Controllers.TeamController.DeleteTeam)  [L157–L170] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L161]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

