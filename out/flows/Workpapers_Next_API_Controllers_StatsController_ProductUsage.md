[web] GET /api/stats/productusage/{firmId}  (Workpapers.Next.API.Controllers.StatsController.ProductUsage)  [L101–L114] [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetProductUsageQuery [L111]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetProductUsageQueryHandler.Handle [L47–L133]
      └─ uses_client KeenClient [L96]
      └─ uses_service KeenClient
        └─ method QueryAsync [L96]
      └─ uses_service KeenQueryBuilder
        └─ method Build [L97]
      └─ uses_service UnitOfWork
        └─ method Table [L102]
      └─ uses_cache IMemoryCache [L67]
        └─ method GetOrCreateAsync [read] (key=ProductUsage-period{*}-firmid{*}) [L67]

