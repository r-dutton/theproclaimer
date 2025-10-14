[web] GET /api/stats/binder-type-usage/{firmId}  (Workpapers.Next.API.Controllers.StatsController.FirmBinderTypeUsage)  [L130–L143] [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetBinderTypeUsageQuery [L140]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetBinderTypeUsageQueryHandler.Handle [L36–L90]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L54]
      └─ uses_service IControlledRepository<BinderType>
        └─ method ReadQuery [L70]

