[web] GET /api/super/users/{id:guid}  (Dataverse.Api.Controllers.Super.Core.UsersController.GetByTenant)  [L91–L96] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
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

