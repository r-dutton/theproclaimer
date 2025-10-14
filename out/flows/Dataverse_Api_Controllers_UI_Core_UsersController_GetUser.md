[web] GET /api/ui/users/{id:guid}  (Dataverse.Api.Controllers.UI.Core.UsersController.GetUser)  [L78–L85] [auth=Authentication.UserPolicy]
  └─ sends_request GetUserQuery [L82]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.GetUserQueryHandler.Handle [L22–L40]
      └─ maps_to UserDto [L35]
        └─ automapper.registration ExternalApiMappingProfile (User->UserDto) [L61]
        └─ automapper.registration CirrusMappingProfile (User->UserDto) [L110]
        └─ automapper.registration WorkpapersMappingProfile (User->UserDto) [L106]
        └─ automapper.registration DataverseMappingProfile (User->UserDto) [L76]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L35]

