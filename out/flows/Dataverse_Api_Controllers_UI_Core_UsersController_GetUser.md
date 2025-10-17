[web] GET /api/ui/users/{id:guid}  (Dataverse.Api.Controllers.UI.Core.UsersController.GetUser)  [L78–L85] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetUserQuery -> GetUserQueryHandler [L82]
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.GetUserQueryHandler.Handle [L22–L40]
      └─ calls UserRepository.ReadQuery [L35]
      └─ maps_to UserDto [L35]
        └─ automapper.registration DataverseMappingProfile (User->UserDto) [L77]
        └─ automapper.registration ExternalApiMappingProfile (User->UserDto) [L61]
  └─ impact_summary
    └─ requests 1
      └─ GetUserQuery
    └─ handlers 1
      └─ GetUserQueryHandler
    └─ mappings 1
      └─ UserDto

