[web] GET /api/firms/{id:Guid}  (Workpapers.Next.API.Controllers.Firms.FirmsController.GetFirm)  [L80–L104]
  └─ maps_to FirmDto [L88]
    └─ converts_to FirmWithStatsDto [L162]
  └─ maps_to FirmWithStatsDto [L88]
  └─ maps_to FirmWithStatsDto [L87]
  └─ maps_to FirmWithSubscriptionsDto [L87]
    └─ converts_to FirmWithStatsDto [L170]
  └─ uses_service IMapper
    └─ method Map [L87]
  └─ uses_service UnitOfWork
    └─ method Table [L87]
  └─ uses_service UserService
    └─ method IsSuperUser [L83]

