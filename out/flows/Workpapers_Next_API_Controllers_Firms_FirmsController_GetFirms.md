[web] GET /api/firms  (Workpapers.Next.API.Controllers.Firms.FirmsController.GetFirms)  [L56–L66] [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetFirmsQuery [L61]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Firms.Firms.GetFirmsQueryHandler.Handle [L21–L39]
      └─ maps_to FirmDto [L34]
        └─ converts_to FirmWithStatsDto [L162]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L35]
      └─ uses_service UnitOfWork
        └─ method Table [L34]
  └─ sends_request GetPagedFirmsQuery [L63]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Firms.Firms.GetPagedFirmsQueryHandler.Handle [L35–L77]
      └─ uses_service UnitOfWork
        └─ method Table [L48]

