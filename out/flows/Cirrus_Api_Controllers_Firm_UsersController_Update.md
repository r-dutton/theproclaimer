[web] PUT /api/users/{id}  (Cirrus.Api.Controllers.Firm.UsersController.Update)  [L155–L163] status=200 [auth=Authentication.UserPolicy]
  └─ calls UserRepository.WriteQuery [L160]
  └─ write User [L160]
  └─ uses_service IControlledRepository<User>
    └─ method WriteQuery [L160]
      └─ ... (no implementation details available)
  └─ uses_service IUserService (InstancePerLifetimeScope)
    └─ method GetUserId [L158]
      └─ implementation IUserService.GetUserId [L18-L18]
      └─ ... (no implementation details available)

