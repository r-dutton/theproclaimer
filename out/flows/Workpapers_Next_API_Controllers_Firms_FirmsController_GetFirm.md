[web] GET /api/firms/{id:Guid}  (Workpapers.Next.API.Controllers.Firms.FirmsController.GetFirm)  [L88–L112] status=200
  └─ maps_to FirmDto [L96]
    └─ converts_to FirmWithStatsDto [L162]
  └─ maps_to FirmWithStatsDto [L96]
  └─ maps_to FirmWithStatsDto [L95]
  └─ maps_to FirmWithSubscriptionsDto [L95]
    └─ converts_to FirmWithStatsDto [L170]
  └─ uses_service IMapper
    └─ method Map [L95]
      └─ ... (no implementation details available)
  └─ uses_service UnitOfWork
    └─ method Table [L95]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L91]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]

