[web] PUT /api/internal/users/user-updated  (Dataverse.Api.Controllers.Internal.Core.UsersController.UserUpdated)  [L181–L192] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to UserWithIdentityInfoDto [L191]
  └─ calls UserRepository.WriteQuery [L186]
  └─ writes_to User [L186]
  └─ uses_service IControlledRepository<User>
    └─ method WriteQuery [L186]
  └─ uses_service IMapper
    └─ method Map [L191]

