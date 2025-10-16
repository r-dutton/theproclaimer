[web] POST /api/users/{userId}/resendInvite  (Cirrus.Api.Controllers.Firm.UsersController.ResendInvite)  [L174–L183] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls UserRepository.ReadQuery [L177]
  └─ query User [L177]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L177]
      └─ ... (no implementation details available)
  └─ uses_service ITenantService (AddScoped)
    └─ method GetCurrentTenant [L181]
      └─ implementation ITenantService.GetCurrentTenant [L14-L14]
      └─ ... (no implementation details available)

