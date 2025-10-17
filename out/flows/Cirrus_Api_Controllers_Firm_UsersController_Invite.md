[web] POST /api/users/{userId}/request-identity-account  (Cirrus.Api.Controllers.Firm.UsersController.Invite)  [L165–L172] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls UserRepository.WriteQuery [L168]
  └─ write User [L168]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ User writes=1 reads=0

