[web] GET /api/team-users/for-team/{teamId:Guid}  (Cirrus.Api.Controllers.Firm.TeamUsersController.GetAllForTeam)  [L57–L68] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TeamUserWithUserDto [L62]
    └─ automapper.registration DataverseMappingProfile (TeamUser->TeamUserWithUserDto) [L152]
    └─ automapper.registration CirrusMappingProfile (TeamUser->TeamUserWithUserDto) [L141]
  └─ calls TeamUserRepository.ReadQuery [L62]
  └─ query TeamUser [L62]
    └─ reads_from TeamUsers
  └─ uses_service IControlledRepository<TeamUser>
    └─ method ReadQuery [L62]
      └─ ... (no implementation details available)

