[web] POST /api/users/  (Cirrus.Api.Controllers.Firm.UsersController.Create)  [L129–L135] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls UserRepository.Add [L133]
  └─ insert User [L133]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ User writes=1 reads=0

