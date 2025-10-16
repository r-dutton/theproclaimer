[web] GET /api/reportance/cirrus/starters  (Workpapers.Next.API.Controllers.Reportance.CirrusController.GetStarters)  [L58–L69] status=200 [auth=AuthorizationPolicies.M2M]
  └─ maps_to StarterDto [L68]
  └─ sends_request AllStartersForProductQuery -> AllStartersForProductQueryHandler [L65]
    └─ handled_by Workpapers.Next.Data.Queries.Templates.Starters.AllStartersForProductQueryHandler.Handle [L17–L63]
      └─ uses_service UnitOfWork
        └─ method Table [L40]
          └─ implementation UnitOfWork.Table
      └─ uses_service UserService
        └─ method IsSuperUser [L35]
          └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
            └─ uses_service bool?
              └─ method IsSuperUser [L174]
                └─ ... (no implementation details available)
            └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L35]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ requests 1
      └─ AllStartersForProductQuery
    └─ handlers 1
      └─ AllStartersForProductQueryHandler
    └─ caches 1
      └─ IMemoryCache
    └─ mappings 1
      └─ StarterDto

