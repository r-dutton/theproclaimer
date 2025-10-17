[web] GET /api/teams/byfirm/{id:Guid}  (Workpapers.Next.API.Controllers.TeamController.GetTeamsForFirm)  [L45–L56] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to TeamDto [L51]
  └─ uses_service UnitOfWork
    └─ method Table [L51]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ TeamDto

