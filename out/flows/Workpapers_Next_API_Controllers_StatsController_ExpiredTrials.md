[web] GET /api/stats/expiredtrials  (Workpapers.Next.API.Controllers.StatsController.ExpiredTrials)  [L30–L42] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetTrialExpiryQuery -> GetTrialExpiryQueryHandler [L39]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetTrialExpiryQueryHandler.Handle [L38–L84]
      └─ uses_service UnitOfWork
        └─ method Table [L54]
          └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ requests 1
      └─ GetTrialExpiryQuery
    └─ handlers 1
      └─ GetTrialExpiryQueryHandler

