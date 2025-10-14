[web] GET /api/internal/users  (Dataverse.Api.Controllers.Internal.Core.UsersController.GetAll)  [L83–L90] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to UserLightDto [L86]
    └─ automapper.registration CirrusMappingProfile (User->UserLightDto) [L104]
    └─ automapper.registration DataverseMappingProfile (User->UserLightDto) [L83]
    └─ automapper.registration WorkpapersMappingProfile (User->UserLightDto) [L97]
  └─ calls UserRepository.ReadQuery [L86]
  └─ queries User [L86]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L86]

