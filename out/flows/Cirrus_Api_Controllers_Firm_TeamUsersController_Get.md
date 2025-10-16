[web] GET /api/team-users/{id}  (Cirrus.Api.Controllers.Firm.TeamUsersController.Get)  [L42–L48] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TeamUserDto [L45]
    └─ automapper.registration CirrusMappingProfile (TeamUser->TeamUserDto) [L140]
  └─ calls TeamUserRepository.ReadQuery [L45]
  └─ query TeamUser [L45]
    └─ reads_from TeamUsers
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TeamUser writes=0 reads=1
    └─ mappings 1
      └─ TeamUserDto

