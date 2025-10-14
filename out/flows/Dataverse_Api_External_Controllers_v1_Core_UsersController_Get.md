[web] GET /core/v1/users/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.UsersController.Get)  [L67–L72]
  └─ maps_to UserDto [L70]
    └─ automapper.registration ExternalApiMappingProfile (User->UserDto) [L61]
    └─ automapper.registration CirrusMappingProfile (User->UserDto) [L110]
    └─ automapper.registration WorkpapersMappingProfile (User->UserDto) [L106]
    └─ automapper.registration DataverseMappingProfile (User->UserDto) [L76]
  └─ calls UserRepository.ReadQuery [L70]
  └─ queries User [L70]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L70]

