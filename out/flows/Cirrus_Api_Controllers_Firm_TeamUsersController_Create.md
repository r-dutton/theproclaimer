[web] POST /api/team-users/  (Cirrus.Api.Controllers.Firm.TeamUsersController.Create)  [L74–L80] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TeamUserRepository.Add [L78]
  └─ insert TeamUser [L78]
    └─ reads_from TeamUsers
  └─ uses_service IControlledRepository<TeamUser>
    └─ method Add [L78]
      └─ ... (no implementation details available)

