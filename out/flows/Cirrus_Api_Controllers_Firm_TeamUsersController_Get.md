[web] GET /api/team-users/{id}  (Cirrus.Api.Controllers.Firm.TeamUsersController.Get)  [L42–L48] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TeamUserDto [L45]
    └─ automapper.registration DataverseMappingProfile (TeamUser->TeamUserDto) [L151]
    └─ automapper.registration CirrusMappingProfile (TeamUser->TeamUserDto) [L140]
  └─ calls TeamUserRepository.ReadQuery [L45]
  └─ query TeamUser [L45]
    └─ reads_from TeamUsers
  └─ uses_service IControlledRepository<TeamUser>
    └─ method ReadQuery [L45]
      └─ ... (no implementation details available)

