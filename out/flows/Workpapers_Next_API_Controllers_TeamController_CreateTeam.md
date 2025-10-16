[web] POST /api/teams  (Workpapers.Next.API.Controllers.TeamController.CreateTeam)  [L94–L104] status=201 [auth=AuthorizationPolicies.Administrator]
  └─ maps_to TeamDto [L103]
  └─ uses_service IMapper
    └─ method Map [L103]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Add [L101]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetFirmId [L98]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]

