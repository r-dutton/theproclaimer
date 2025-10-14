[web] GET /api/stats/starterusage  (Workpapers.Next.API.Controllers.StatsController.StarterUsageAllFirms)  [L174–L186] [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetStarterUsageQuery [L183]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetStarterUsageQueryHandler.Handle [L47–L128]
      └─ uses_client KeenClient [L97]
      └─ uses_service KeenClient
        └─ method QueryAsync [L97]
      └─ uses_service KeenQueryBuilder
        └─ method Build [L98]
      └─ uses_service UnitOfWork
        └─ method Table [L106]
      └─ uses_cache IMemoryCache [L68]
        └─ method GetOrCreateAsync [read] (key=StarterUsage-period{*}-firmid{*}) [L68]

