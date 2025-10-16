[web] GET /api/internal/users/{id}  (Dataverse.Api.Controllers.Internal.Core.UsersController.Get)  [L92–L98] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to UserDto [L95]
    └─ automapper.registration ExternalApiMappingProfile (User->UserDto) [L61]
    └─ automapper.registration CirrusMappingProfile (User->UserDto) [L110]
    └─ automapper.registration WorkpapersMappingProfile (User->UserDto) [L106]
    └─ automapper.registration DataverseMappingProfile (User->UserDto) [L77]
  └─ calls UserRepository.ReadQuery [L95]
  └─ query User [L95]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L95]
      └─ ... (no implementation details available)

