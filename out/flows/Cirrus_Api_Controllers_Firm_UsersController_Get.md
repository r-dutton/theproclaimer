[web] GET /api/users/{id}  (Cirrus.Api.Controllers.Firm.UsersController.Get)  [L99–L105] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to UserDto [L102]
    └─ automapper.registration ExternalApiMappingProfile (User->UserDto) [L61]
    └─ automapper.registration CirrusMappingProfile (User->UserDto) [L110]
    └─ automapper.registration WorkpapersMappingProfile (User->UserDto) [L106]
    └─ automapper.registration DataverseMappingProfile (User->UserDto) [L77]
  └─ calls UserRepository.ReadQuery [L102]
  └─ query User [L102]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L102]
      └─ ... (no implementation details available)

