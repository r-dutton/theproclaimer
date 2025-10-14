[web] GET /api/users/  (Cirrus.Api.Controllers.Firm.UsersController.Search)  [L114–L123] [auth=Authentication.UserPolicy]
  └─ sends_request FindUsersQuery [L122]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindUsersQueryHandler.Handle [L60–L143]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L74]

