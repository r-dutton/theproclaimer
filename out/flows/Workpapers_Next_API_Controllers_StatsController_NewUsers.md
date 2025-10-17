[web] GET /api/stats/newusers  (Workpapers.Next.API.Controllers.StatsController.NewUsers)  [L188–L200] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ sends_request GetNewUsersQuery -> GetNewUsersQueryHandler [L197]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Stats.GetNewUsersQueryHandler.Handle [L37–L91]
      └─ uses_service UnitOfWork
        └─ method Table [L53]
          └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ requests 1
      └─ GetNewUsersQuery
    └─ handlers 1
      └─ GetNewUsersQueryHandler

