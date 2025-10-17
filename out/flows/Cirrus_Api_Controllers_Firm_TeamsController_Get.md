[web] GET /api/teams/{id}  (Cirrus.Api.Controllers.Firm.TeamsController.Get)  [L40–L46] status=200 [auth=user]
  └─ maps_to TeamDto [L43]
    └─ automapper.registration CirrusMappingProfile (Team->TeamDto) [L138]
  └─ calls TeamRepository.ReadQuery [L43]
  └─ query Team [L43]
    └─ reads_from Teams
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Team writes=0 reads=1
    └─ mappings 1
      └─ TeamDto

