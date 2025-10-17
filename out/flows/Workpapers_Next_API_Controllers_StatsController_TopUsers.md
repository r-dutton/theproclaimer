[web] GET /api/stats/topusers  (Workpapers.Next.API.Controllers.StatsController.TopUsers)  [L215–L226] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=TopUsers-type{*}-period{*}) [L218]
  └─ sends_request GetTopUsersQuery -> GetTopUsersQueryHandler [L222]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.GetTopUsersQueryHandler.Handle [L32–L91]
      └─ uses_client KeenClient [L64]
      └─ uses_service UnitOfWork
        └─ method Table [L74]
          └─ implementation UnitOfWork.Table
      └─ uses_service KeenQueryBuilder
        └─ method Build [L65]
          └─ ... (no implementation details available)
      └─ uses_service KeenClient
        └─ method QueryAsync [L64]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ clients 1
      └─ KeenClient
    └─ requests 1
      └─ GetTopUsersQuery
    └─ handlers 1
      └─ GetTopUsersQueryHandler
    └─ caches 1
      └─ IMemoryCache

