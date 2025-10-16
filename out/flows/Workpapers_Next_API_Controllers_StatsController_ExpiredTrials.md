[web] GET /api/stats/expiredtrials  (Workpapers.Next.API.Controllers.StatsController.ExpiredTrials)  [L30–L42] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetTrialExpiryQuery [L39]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetTrialExpiryQueryHandler.Handle [L38–L84]
      └─ uses_service UnitOfWork
        └─ method Table [L54]
          └─ ... (no implementation details available)

