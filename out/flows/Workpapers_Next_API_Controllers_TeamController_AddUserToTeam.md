[web] PUT /api/teams/{id:Guid}/adduser/{userId:Guid}  (Workpapers.Next.API.Controllers.TeamController.AddUserToTeam)  [L121–L137] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L125]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork

