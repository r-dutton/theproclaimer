[web] GET /api/stats/binder-type-usage  (Workpapers.Next.API.Controllers.StatsController.BinderTypeUsage)  [L145–L157] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetBinderTypeUsageQuery -> GetBinderTypeUsageQueryHandler [L154]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetBinderTypeUsageQueryHandler.Handle [L36–L90]
      └─ uses_service IControlledRepository<BinderType> (Scoped (inferred))
        └─ method ReadQuery [L70]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.BinderTypeRepository.ReadQuery
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L54]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ GetBinderTypeUsageQuery
    └─ handlers 1
      └─ GetBinderTypeUsageQueryHandler

