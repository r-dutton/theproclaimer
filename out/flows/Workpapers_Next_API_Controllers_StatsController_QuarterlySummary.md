[web] GET /api/stats/quarterlysummary  (Workpapers.Next.API.Controllers.StatsController.QuarterlySummary)  [L202–L213] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=QuarterlySummary) [L205]
  └─ sends_request GetQuarterlySummaryQuery -> GetQuarterlySummaryQueryHandler [L209]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetQuarterlySummaryQueryHandler.Handle [L25–L155]
      └─ uses_client KeenClient [L82]
      └─ uses_service KeenQueryBuilder
        └─ method Build [L83]
          └─ ... (no implementation details available)
      └─ uses_service KeenClient
        └─ method QueryAsync [L82]
          └─ ... (no implementation details available)
      └─ uses_service UnitOfWork
        └─ method Table [L45]
          └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ clients 1
      └─ KeenClient
    └─ requests 1
      └─ GetQuarterlySummaryQuery
    └─ handlers 1
      └─ GetQuarterlySummaryQueryHandler
    └─ caches 1
      └─ IMemoryCache

