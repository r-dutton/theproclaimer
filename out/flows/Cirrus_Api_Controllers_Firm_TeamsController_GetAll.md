[web] GET /api/teams/  (Cirrus.Api.Controllers.Firm.TeamsController.GetAll)  [L55–L62] status=200 [auth=user]
  └─ maps_to TeamLightDto [L58]
    └─ automapper.registration CirrusMappingProfile (Team->TeamLightDto) [L139]
  └─ calls TeamRepository.ReadQuery [L58]
  └─ query Team [L58]
    └─ reads_from Teams
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Team writes=0 reads=1
    └─ mappings 1
      └─ TeamLightDto

