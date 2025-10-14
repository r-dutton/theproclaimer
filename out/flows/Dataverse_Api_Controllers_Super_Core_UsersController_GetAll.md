[web] GET /api/super/users/getAll  (Dataverse.Api.Controllers.Super.Core.UsersController.GetAll)  [L104–L113] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to UserLightDto [L108]
    └─ automapper.registration CirrusMappingProfile (User->UserLightDto) [L104]
    └─ automapper.registration DataverseMappingProfile (User->UserLightDto) [L83]
    └─ automapper.registration WorkpapersMappingProfile (User->UserLightDto) [L97]
  └─ calls UserRepository.ReadQuery [L108]
  └─ queries User [L108]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L108]

