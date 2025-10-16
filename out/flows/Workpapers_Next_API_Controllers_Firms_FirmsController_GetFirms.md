[web] GET /api/firms  (Workpapers.Next.API.Controllers.Firms.FirmsController.GetFirms)  [L56–L74] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetFirmsQuery [L68]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Firms.Firms.GetFirmsQueryHandler.Handle [L21–L39]
      └─ maps_to FirmDto [L34]
        └─ converts_to FirmWithStatsDto [L162]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L35]
          └─ ... (no implementation details available)
      └─ uses_service UnitOfWork
        └─ method Table [L34]
          └─ ... (no implementation details available)
  └─ sends_request GetPagedFirmsQuery [L70]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Firms.Firms.GetPagedFirmsQueryHandler.Handle [L38–L81]
      └─ uses_service UnitOfWork
        └─ method Table [L51]
          └─ ... (no implementation details available)

