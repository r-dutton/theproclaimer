[web] GET /api/stats/licensesummary  (Workpapers.Next.API.Controllers.StatsController.LicenseSummary)  [L44–L55] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetLicenseSummaryQuery -> GetLicenseSummaryQueryHandler [L52]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetLicenseSummaryQueryHandler.Handle [L40–L156]
      └─ uses_service UnitOfWork
        └─ method Table [L72]
          └─ implementation UnitOfWork.Table
      └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=LicenseSummary) [L53]
  └─ impact_summary
    └─ requests 1
      └─ GetLicenseSummaryQuery
    └─ handlers 1
      └─ GetLicenseSummaryQueryHandler

