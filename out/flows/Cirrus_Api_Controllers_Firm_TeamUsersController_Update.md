[web] PUT /api/team-users/{id}  (Cirrus.Api.Controllers.Firm.TeamUsersController.Update)  [L85–L91] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TeamUserRepository.WriteQuery [L88]
  └─ write TeamUser [L88]
    └─ reads_from TeamUsers
  └─ uses_service IControlledRepository<TeamUser>
    └─ method WriteQuery [L88]
      └─ ... (no implementation details available)

