[web] DELETE /api/team-users/{id:int}  (Cirrus.Api.Controllers.Firm.TeamUsersController.Delete)  [L96–L102] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TeamUserRepository.Remove [L100]
  └─ calls TeamUserRepository.WriteQuery [L99]
  └─ writes_to TeamUser [L100]
    └─ reads_from TeamUsers
  └─ writes_to TeamUser [L99]
    └─ reads_from TeamUsers
  └─ uses_service IControlledRepository<TeamUser>
    └─ method WriteQuery [L99]

