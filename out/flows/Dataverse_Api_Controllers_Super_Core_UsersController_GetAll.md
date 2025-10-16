[web] GET /api/super/users/getAll  (Dataverse.Api.Controllers.Super.Core.UsersController.GetAll)  [L104–L113] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to UserLightDto [L108]
    └─ automapper.registration CirrusMappingProfile (User->UserLightDto) [L104]
    └─ automapper.registration DataverseMappingProfile (User->UserLightDto) [L84]
    └─ automapper.registration WorkpapersMappingProfile (User->UserLightDto) [L97]
  └─ calls UserRepository.ReadQuery [L108]
  └─ query User [L108]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L108]
      └─ ... (no implementation details available)

