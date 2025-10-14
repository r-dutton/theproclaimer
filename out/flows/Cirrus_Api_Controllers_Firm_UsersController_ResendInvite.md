[web] POST /api/users/{userId}/resendInvite  (Cirrus.Api.Controllers.Firm.UsersController.ResendInvite)  [L174–L183] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls UserRepository.ReadQuery [L177]
  └─ queries User [L177]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L177]
  └─ uses_service ITenantService (AddScoped)
    └─ method GetCurrentTenant [L181]

