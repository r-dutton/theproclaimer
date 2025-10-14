[web] GET /api/stats/topfirms  (Workpapers.Next.API.Controllers.StatsController.TopFirms)  [L228–L239] [auth=AuthorizationPolicies.SuperUser]
  └─ uses_cache IMemoryCache [L231]
    └─ method GetOrCreateAsync [read] (key=TopFirms-type{*}-period{*}) [L231]
  └─ sends_request GetTopFirmsQuery [L235]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.GetTopFirmsQueryHandler.Handle [L31–L105]
      └─ uses_client KeenClient [L84]
      └─ uses_service KeenClient
        └─ method QueryAsync [L84]
      └─ uses_service KeenQueryBuilder
        └─ method Build [L85]
      └─ uses_service UnitOfWork
        └─ method Table [L93]

