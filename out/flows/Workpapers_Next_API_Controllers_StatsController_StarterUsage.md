[web] GET /api/stats/starterusage/{firmId}  (Workpapers.Next.API.Controllers.StatsController.StarterUsage)  [L159–L172] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetStarterUsageQuery -> GetStarterUsageQueryHandler [L169]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetStarterUsageQueryHandler.Handle [L47–L128]
      └─ uses_client KeenClient [L97]
      └─ uses_service UnitOfWork
        └─ method Table [L106]
          └─ implementation UnitOfWork.Table
      └─ uses_service KeenQueryBuilder
        └─ method Build [L98]
          └─ ... (no implementation details available)
      └─ uses_service KeenClient
        └─ method QueryAsync [L97]
          └─ ... (no implementation details available)
      └─ uses_cache IMemoryCache.GetOrCreateAsync [read] (key=StarterUsage-period{*}-firmid{*}) [L68]
  └─ impact_summary
    └─ clients 1
      └─ KeenClient
    └─ requests 1
      └─ GetStarterUsageQuery
    └─ handlers 1
      └─ GetStarterUsageQueryHandler

