[web] GET /api/stats/topusers  (Workpapers.Next.API.Controllers.StatsController.TopUsers)  [L215–L226] [auth=AuthorizationPolicies.SuperUser]
  └─ uses_cache IMemoryCache [L218]
    └─ method GetOrCreateAsync [read] (key=TopUsers-type{*}-period{*}) [L218]
  └─ sends_request GetTopUsersQuery [L222]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.GetTopUsersQueryHandler.Handle [L32–L91]
      └─ uses_client KeenClient [L64]
      └─ uses_service KeenClient
        └─ method QueryAsync [L64]
      └─ uses_service KeenQueryBuilder
        └─ method Build [L65]
      └─ uses_service UnitOfWork
        └─ method Table [L74]

