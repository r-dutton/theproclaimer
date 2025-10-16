[web] GET /api/teams/{id:Guid}  (Workpapers.Next.API.Controllers.TeamController.GetTeam)  [L70–L80] status=200
  └─ maps_to TeamDto [L79]
  └─ uses_service UnitOfWork
    └─ method Table [L73]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ TeamDto

