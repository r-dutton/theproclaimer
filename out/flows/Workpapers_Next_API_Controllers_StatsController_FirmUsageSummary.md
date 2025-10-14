[web] GET /api/stats/firmusage/{firmId}  (Workpapers.Next.API.Controllers.StatsController.FirmUsageSummary)  [L57–L69] [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetFirmUsageSummaryQuery [L66]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetFirmUsageSummaryQueryHandler.Handle [L47–L155]
      └─ uses_client KeenClient [L93]
      └─ uses_service KeenClient
        └─ method QueryAsync [L93]
      └─ uses_service KeenQueryBuilder
        └─ method Build [L94]
      └─ uses_service UnitOfWork
        └─ method Table [L88]
      └─ uses_cache IMemoryCache [L70]
        └─ method GetOrCreateAsync [read] (key=FirmUsage-period{*}-firmid{*}) [L70]

