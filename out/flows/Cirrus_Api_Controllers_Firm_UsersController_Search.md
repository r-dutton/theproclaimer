[web] GET /api/users/  (Cirrus.Api.Controllers.Firm.UsersController.Search)  [L114–L123] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request FindUsersQuery [L122]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindUsersQueryHandler.Handle [L60–L143]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L74]
          └─ ... (no implementation details available)

