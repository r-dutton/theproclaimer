[web] GET /api/users/{id}  (Cirrus.Api.Controllers.Firm.UsersController.Get)  [L99–L105] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to UserDto [L102]
    └─ automapper.registration CirrusMappingProfile (User->UserDto) [L110]
  └─ calls UserRepository.ReadQuery [L102]
  └─ query User [L102]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ mappings 1
      └─ UserDto

