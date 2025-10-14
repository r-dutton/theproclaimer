[web] GET /api/team-users/{id}  (Cirrus.Api.Controllers.Firm.TeamUsersController.Get)  [L42–L48] [auth=Authentication.UserPolicy]
  └─ maps_to TeamUserDto [L45]
    └─ automapper.registration DataverseMappingProfile (TeamUser->TeamUserDto) [L150]
    └─ automapper.registration CirrusMappingProfile (TeamUser->TeamUserDto) [L140]
  └─ calls TeamUserRepository.ReadQuery [L45]
  └─ queries TeamUser [L45]
    └─ reads_from TeamUsers
  └─ uses_service IControlledRepository<TeamUser>
    └─ method ReadQuery [L45]

