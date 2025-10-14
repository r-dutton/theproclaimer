[web] PUT /api/team-users/{id}  (Cirrus.Api.Controllers.Firm.TeamUsersController.Update)  [L85–L91] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TeamUserRepository.WriteQuery [L88]
  └─ writes_to TeamUser [L88]
    └─ reads_from TeamUsers
  └─ uses_service IControlledRepository<TeamUser>
    └─ method WriteQuery [L88]

