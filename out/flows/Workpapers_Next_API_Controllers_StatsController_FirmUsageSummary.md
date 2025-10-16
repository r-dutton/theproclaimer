[web] GET /api/stats/firmusage/{firmId}  (Workpapers.Next.API.Controllers.StatsController.FirmUsageSummary)  [L57–L69] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetFirmUsageSummaryQuery -> GetFirmUsageSummaryQueryHandler [L66]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetFirmUsageSummaryQueryHandler.Handle [L47–L155]
      └─ uses_client KeenClient [L93]
      └─ uses_service KeenQueryBuilder
        └─ method Build [L94]
          └─ ... (no implementation details available)
      └─ uses_service KeenClient
        └─ method QueryAsync [L93]
          └─ ... (no implementation details available)
      └─ uses_service UnitOfWork
        └─ method Table [L88]
          └─ implementation UnitOfWork.Table
      └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=FirmUsage-period{*}-firmid{*}) [L70]
  └─ impact_summary
    └─ clients 1
      └─ KeenClient
    └─ requests 1
      └─ GetFirmUsageSummaryQuery
    └─ handlers 1
      └─ GetFirmUsageSummaryQueryHandler

