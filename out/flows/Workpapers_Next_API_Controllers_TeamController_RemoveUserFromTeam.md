[web] PUT /api/teams/{id}/removeuser/{userId}  (Workpapers.Next.API.Controllers.TeamController.RemoveUserFromTeam)  [L139–L155] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L143]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

