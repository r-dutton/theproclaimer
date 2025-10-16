[web] DELETE /api/team-users/{id:int}  (Cirrus.Api.Controllers.Firm.TeamUsersController.Delete)  [L96–L102] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TeamUserRepository.Remove [L100]
  └─ calls TeamUserRepository.WriteQuery [L99]
  └─ delete TeamUser [L100]
    └─ reads_from TeamUsers
  └─ write TeamUser [L99]
    └─ reads_from TeamUsers
  └─ uses_service IControlledRepository<TeamUser>
    └─ method WriteQuery [L99]
      └─ ... (no implementation details available)

