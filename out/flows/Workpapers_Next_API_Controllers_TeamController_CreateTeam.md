[web] POST /api/teams  (Workpapers.Next.API.Controllers.TeamController.CreateTeam)  [L94–L104] [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TeamDto [L103]
  └─ uses_service IMapper
    └─ method Map [L103]
  └─ uses_service UnitOfWork
    └─ method Add [L101]
  └─ uses_service UserService
    └─ method GetFirmId [L98]

