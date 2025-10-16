[web] GET /api/stats/starterusage  (Workpapers.Next.API.Controllers.StatsController.StarterUsageAllFirms)  [L174–L186] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetStarterUsageQuery [L183]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetStarterUsageQueryHandler.Handle [L47–L128]
      └─ uses_client KeenClient [L97]
      └─ uses_service KeenClient
        └─ method QueryAsync [L97]
          └─ ... (no implementation details available)
      └─ uses_service KeenQueryBuilder
        └─ method Build [L98]
          └─ ... (no implementation details available)
      └─ uses_service UnitOfWork
        └─ method Table [L106]
          └─ ... (no implementation details available)
      └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=StarterUsage-period{*}-firmid{*}) [L68]

