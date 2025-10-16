[web] GET /api/teams/  (Workpapers.Next.API.Controllers.TeamController.GetTeams)  [L58–L68] status=200
  └─ maps_to TeamDto [L63]
  └─ uses_service UnitOfWork
    └─ method Table [L63]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetFirmId [L61]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]

