[web] GET /api/teams/{id:Guid}/users  (Workpapers.Next.API.Controllers.TeamController.GetTeamUsers)  [L82–L92] status=200
  └─ maps_to UserDto [L87]
  └─ uses_service UnitOfWork
    └─ method Table [L87]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ UserDto

