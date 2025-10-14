[web] PUT /api/users/{id}  (Cirrus.Api.Controllers.Firm.UsersController.Update)  [L155–L163] [auth=Authentication.UserPolicy]
  └─ calls UserRepository.WriteQuery [L160]
  └─ writes_to User [L160]
  └─ uses_service IControlledRepository<User>
    └─ method WriteQuery [L160]
  └─ uses_service IUserService (InstancePerLifetimeScope)
    └─ method GetUserId [L158]

