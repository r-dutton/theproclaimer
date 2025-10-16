[web] GET /api/firms  (Workpapers.Next.API.Controllers.Firms.FirmsController.GetFirms)  [L56–L74] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetPagedFirmsQuery -> GetPagedFirmsQueryHandler [L70]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Firms.Firms.GetPagedFirmsQueryHandler.Handle [L38–L81]
      └─ uses_service UnitOfWork
        └─ method Table [L51]
          └─ implementation UnitOfWork.Table
  └─ sends_request GetFirmsQuery -> GetFirmsQueryHandler [L68]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Firms.Firms.GetFirmsQueryHandler.Handle [L21–L39]
      └─ maps_to FirmDto [L34]
        └─ converts_to FirmWithStatsDto [L162]
      └─ uses_service UnitOfWork
        └─ method Table [L34]
          └─ implementation UnitOfWork.Table (see previous expansion)
  └─ impact_summary
    └─ requests 2
      └─ GetFirmsQuery
      └─ GetPagedFirmsQuery
    └─ handlers 2
      └─ GetFirmsQueryHandler
      └─ GetPagedFirmsQueryHandler
    └─ mappings 1
      └─ FirmDto

