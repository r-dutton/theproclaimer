[web] POST /api/stats/workpapernousage  (Workpapers.Next.API.Controllers.StatsController.WorkpaperNoUsage)  [L241–L250] status=201 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetWorkpaperNoUsageQuery -> GetWorkpaperNoUsageQueryHandler [L247]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.GetWorkpaperNoUsageQueryHandler.Handle [L32–L120]
      └─ uses_service UnitOfWork
        └─ method Table [L45]
          └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ requests 1
      └─ GetWorkpaperNoUsageQuery
    └─ handlers 1
      └─ GetWorkpaperNoUsageQueryHandler

