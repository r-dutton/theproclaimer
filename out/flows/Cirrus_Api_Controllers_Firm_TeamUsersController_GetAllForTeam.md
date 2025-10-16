[web] GET /api/team-users/for-team/{teamId:Guid}  (Cirrus.Api.Controllers.Firm.TeamUsersController.GetAllForTeam)  [L57–L68] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TeamUserWithUserDto [L62]
    └─ automapper.registration CirrusMappingProfile (TeamUser->TeamUserWithUserDto) [L141]
  └─ calls TeamUserRepository.ReadQuery [L62]
  └─ query TeamUser [L62]
    └─ reads_from TeamUsers
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TeamUser writes=0 reads=1
    └─ mappings 1
      └─ TeamUserWithUserDto

