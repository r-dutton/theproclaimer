[web] GET /api/stats/expiredtrials  (Workpapers.Next.API.Controllers.StatsController.ExpiredTrials)  [L30–L42] [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetTrialExpiryQuery [L39]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetTrialExpiryQueryHandler.Handle [L38–L84]
      └─ uses_service UnitOfWork
        └─ method Table [L54]

