[web] GET /api/ui/teams/{id}/team-users  (Dataverse.Api.Controllers.UI.Core.TeamsController.GetTeamUsers)  [L66–L75] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TeamUserWithUserDto [L69]
  └─ calls TeamRepository.ReadQuery [L69]
  └─ query Team [L69]
    └─ reads_from Teams
  └─ uses_service TeamRepository
    └─ method ReadQuery [L69]
      └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Team writes=0 reads=1
    └─ services 1
      └─ TeamRepository
    └─ mappings 1
      └─ TeamUserWithUserDto

