[web] GET /api/users/{id:Guid}  (Workpapers.Next.API.Controllers.UsersController.Get)  [L108–L117]
  └─ maps_to UserDto [L111]
    └─ automapper.registration ExternalApiMappingProfile (User->UserDto) [L61]
    └─ automapper.registration CirrusMappingProfile (User->UserDto) [L110]
    └─ automapper.registration WorkpapersMappingProfile (User->UserDto) [L106]
    └─ automapper.registration DataverseMappingProfile (User->UserDto) [L76]
  └─ calls UserRepository.ReadQuery [L111]
  └─ queries User [L111]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L111]

