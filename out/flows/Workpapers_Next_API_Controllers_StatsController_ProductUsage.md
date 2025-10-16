[web] GET /api/stats/productusage/{firmId}  (Workpapers.Next.API.Controllers.StatsController.ProductUsage)  [L101–L114] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetProductUsageQuery [L111]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetProductUsageQueryHandler.Handle [L47–L133]
      └─ uses_client KeenClient [L96]
      └─ uses_service KeenClient
        └─ method QueryAsync [L96]
          └─ ... (no implementation details available)
      └─ uses_service KeenQueryBuilder
        └─ method Build [L97]
          └─ ... (no implementation details available)
      └─ uses_service UnitOfWork
        └─ method Table [L102]
          └─ ... (no implementation details available)
      └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=ProductUsage-period{*}-firmid{*}) [L67]

