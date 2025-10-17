[web] GET /api/stats/productusage  (Workpapers.Next.API.Controllers.StatsController.ProductUsageAllFirms)  [L116–L128] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetProductUsageQuery -> GetProductUsageQueryHandler [L125]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetProductUsageQueryHandler.Handle [L47–L133]
      └─ uses_client KeenClient [L96]
      └─ uses_service UnitOfWork
        └─ method Table [L102]
          └─ implementation UnitOfWork.Table
      └─ uses_service KeenQueryBuilder
        └─ method Build [L97]
          └─ ... (no implementation details available)
      └─ uses_service KeenClient
        └─ method QueryAsync [L96]
          └─ ... (no implementation details available)
      └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=ProductUsage-period{*}-firmid{*}) [L67]
  └─ impact_summary
    └─ clients 1
      └─ KeenClient
    └─ requests 1
      └─ GetProductUsageQuery
    └─ handlers 1
      └─ GetProductUsageQueryHandler

