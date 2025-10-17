[web] POST /api/teams  (Workpapers.Next.API.Controllers.TeamController.CreateTeam)  [L94–L104] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TeamDto [L103]
  └─ uses_service UnitOfWork
    └─ method Add [L101]
      └─ implementation UnitOfWork.Add
  └─ uses_service UserService
    └─ method GetFirmId [L98]
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

