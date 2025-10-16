[web] GET /api/teams/  (Workpapers.Next.API.Controllers.TeamController.GetTeams)  [L58–L68] status=200
  └─ maps_to TeamDto [L63]
  └─ uses_service UnitOfWork
    └─ method Table [L63]
      └─ implementation UnitOfWork.Table
  └─ uses_service UserService
    └─ method GetFirmId [L61]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]
        └─ uses_service Firm>
          └─ method GetFirmId [L139]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ services 2
      └─ UnitOfWork
      └─ UserService
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ TeamDto

