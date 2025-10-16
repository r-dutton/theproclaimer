[web] GET /api/stats/newusers  (Workpapers.Next.API.Controllers.StatsController.NewUsers)  [L188–L200] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetNewUsersQuery [L197]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetNewUsersQueryHandler.Handle [L37–L91]
      └─ uses_service UnitOfWork
        └─ method Table [L53]
          └─ ... (no implementation details available)

