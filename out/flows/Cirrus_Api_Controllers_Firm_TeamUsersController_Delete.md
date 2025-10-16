[web] DELETE /api/team-users/{id:int}  (Cirrus.Api.Controllers.Firm.TeamUsersController.Delete)  [L96–L102] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TeamUserRepository (methods: Remove,WriteQuery) [L100]
  └─ delete TeamUser [L100]
    └─ reads_from TeamUsers
  └─ write TeamUser [L99]
    └─ reads_from TeamUsers
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ TeamUser writes=2 reads=0

