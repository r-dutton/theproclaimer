[web] GET /api/offices/{id}/teams  (Workpapers.Next.API.Controllers.OfficeController.GetOfficeTeams)  [L93–L100] status=200
  └─ maps_to TeamDto [L96]
  └─ uses_service UnitOfWork
    └─ method Table [L96]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ TeamDto

