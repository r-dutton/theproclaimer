[web] GET /api/stats/quarterlysummary  (Workpapers.Next.API.Controllers.StatsController.QuarterlySummary)  [L202–L213] [auth=AuthorizationPolicies.SuperUser]
  └─ uses_cache IMemoryCache [L205]
    └─ method GetOrCreateAsync [read] (key=QuarterlySummary) [L205]
  └─ sends_request GetQuarterlySummaryQuery [L209]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetQuarterlySummaryQueryHandler.Handle [L25–L155]
      └─ uses_client KeenClient [L82]
      └─ uses_service KeenClient
        └─ method QueryAsync [L82]
      └─ uses_service KeenQueryBuilder
        └─ method Build [L83]
      └─ uses_service UnitOfWork
        └─ method Table [L45]

