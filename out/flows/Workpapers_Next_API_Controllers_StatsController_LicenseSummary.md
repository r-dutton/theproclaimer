[web] GET /api/stats/licensesummary  (Workpapers.Next.API.Controllers.StatsController.LicenseSummary)  [L44–L55] [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetLicenseSummaryQuery [L52]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetLicenseSummaryQueryHandler.Handle [L40–L156]
      └─ uses_service UnitOfWork
        └─ method Table [L72]
      └─ uses_cache IMemoryCache [L53]
        └─ method GetOrCreateAsync [read] (key=LicenseSummary) [L53]

