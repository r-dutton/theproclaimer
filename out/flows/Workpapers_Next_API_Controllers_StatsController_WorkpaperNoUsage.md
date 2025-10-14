[web] POST /api/stats/workpapernousage  (Workpapers.Next.API.Controllers.StatsController.WorkpaperNoUsage)  [L241–L250] [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetWorkpaperNoUsageQuery [L247]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.GetWorkpaperNoUsageQueryHandler.Handle [L32–L120]
      └─ uses_service UnitOfWork
        └─ method Table [L45]

