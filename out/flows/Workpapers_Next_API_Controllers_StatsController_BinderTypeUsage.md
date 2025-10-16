[web] GET /api/stats/binder-type-usage  (Workpapers.Next.API.Controllers.StatsController.BinderTypeUsage)  [L145–L157] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetBinderTypeUsageQuery [L154]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetBinderTypeUsageQueryHandler.Handle [L36–L90]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L54]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<BinderType>
        └─ method ReadQuery [L70]
          └─ ... (no implementation details available)

