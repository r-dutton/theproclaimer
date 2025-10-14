[web] GET /api/stats/newusers  (Workpapers.Next.API.Controllers.StatsController.NewUsers)  [L188–L200] [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetNewUsersQuery [L197]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetNewUsersQueryHandler.Handle [L37–L91]
      └─ uses_service UnitOfWork
        └─ method Table [L53]

