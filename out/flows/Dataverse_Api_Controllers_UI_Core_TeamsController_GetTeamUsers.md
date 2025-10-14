[web] GET /api/ui/teams/{id}/team-users  (Dataverse.Api.Controllers.UI.Core.TeamsController.GetTeamUsers)  [L66–L75] [auth=Authentication.UserPolicy]
  └─ maps_to TeamUserWithUserDto [L69]
  └─ calls TeamRepository.ReadQuery [L69]
  └─ queries Team [L69]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L69]

