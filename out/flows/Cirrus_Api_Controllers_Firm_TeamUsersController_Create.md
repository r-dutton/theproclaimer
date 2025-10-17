[web] POST /api/team-users/  (Cirrus.Api.Controllers.Firm.TeamUsersController.Create)  [L74–L80] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TeamUserRepository.Add [L78]
  └─ insert TeamUser [L78]
    └─ reads_from TeamUsers
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ TeamUser writes=1 reads=0

