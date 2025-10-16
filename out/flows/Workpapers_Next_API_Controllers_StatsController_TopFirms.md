[web] GET /api/stats/topfirms  (Workpapers.Next.API.Controllers.StatsController.TopFirms)  [L228–L239] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=TopFirms-type{*}-period{*}) [L231]
  └─ sends_request GetTopFirmsQuery -> GetTopFirmsQueryHandler [L235]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.GetTopFirmsQueryHandler.Handle [L31–L105]
      └─ uses_client KeenClient [L84]
      └─ uses_service UnitOfWork
        └─ method Table [L93]
          └─ implementation UnitOfWork.Table
      └─ uses_service KeenQueryBuilder
        └─ method Build [L85]
          └─ ... (no implementation details available)
      └─ uses_service KeenClient
        └─ method QueryAsync [L84]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ clients 1
      └─ KeenClient
    └─ requests 1
      └─ GetTopFirmsQuery
    └─ handlers 1
      └─ GetTopFirmsQueryHandler
    └─ caches 1
      └─ IMemoryCache

