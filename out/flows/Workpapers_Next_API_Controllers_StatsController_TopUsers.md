[web] GET /api/stats/topusers  (Workpapers.Next.API.Controllers.StatsController.TopUsers)  [L215–L226] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=TopUsers-type{*}-period{*}) [L218]
  └─ sends_request GetTopUsersQuery [L222]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.GetTopUsersQueryHandler.Handle [L32–L91]
      └─ uses_client KeenClient [L64]
      └─ uses_service KeenClient
        └─ method QueryAsync [L64]
          └─ ... (no implementation details available)
      └─ uses_service KeenQueryBuilder
        └─ method Build [L65]
          └─ ... (no implementation details available)
      └─ uses_service UnitOfWork
        └─ method Table [L74]
          └─ ... (no implementation details available)

