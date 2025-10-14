[web] POST /api/firms  (Workpapers.Next.API.Controllers.Firms.FirmsController.AddFirm)  [L114–L139] [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to FirmWithSubscriptionsDto [L138]
    └─ converts_to FirmWithStatsDto [L170]
  └─ uses_service IMapper
    └─ method Map [L138]
  └─ uses_service UnitOfWork
    └─ method Table [L122]

