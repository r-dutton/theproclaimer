[web] GET /api/ui/users/getAll  (Dataverse.Api.Controllers.UI.Core.UsersController.GetAll)  [L169–L177] [auth=Authentication.UserPolicy]
  └─ maps_to UserLightDto [L172]
    └─ automapper.registration CirrusMappingProfile (User->UserLightDto) [L104]
    └─ automapper.registration DataverseMappingProfile (User->UserLightDto) [L83]
    └─ automapper.registration WorkpapersMappingProfile (User->UserLightDto) [L97]
  └─ calls UserRepository.ReadQuery [L172]
  └─ queries User [L172]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L172]

