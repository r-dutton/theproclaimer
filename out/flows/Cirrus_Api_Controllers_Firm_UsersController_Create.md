[web] POST /api/users/  (Cirrus.Api.Controllers.Firm.UsersController.Create)  [L129–L135] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls UserRepository.Add [L133]
  └─ writes_to User [L133]
  └─ uses_service IControlledRepository<User>
    └─ method Add [L133]

