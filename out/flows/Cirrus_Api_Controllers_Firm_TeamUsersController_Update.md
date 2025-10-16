[web] PUT /api/team-users/{id}  (Cirrus.Api.Controllers.Firm.TeamUsersController.Update)  [L85–L91] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TeamUserRepository.WriteQuery [L88]
  └─ write TeamUser [L88]
    └─ reads_from TeamUsers
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ TeamUser writes=1 reads=0

