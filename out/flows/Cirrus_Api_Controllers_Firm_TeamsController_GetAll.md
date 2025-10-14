[web] GET /api/teams/  (Cirrus.Api.Controllers.Firm.TeamsController.GetAll)  [L55–L62] [auth=user]
  └─ maps_to TeamLightDto [L58]
    └─ automapper.registration CirrusMappingProfile (Team->TeamLightDto) [L139]
    └─ automapper.registration WorkpapersMappingProfile (Team->TeamLightDto) [L188]
    └─ automapper.registration DataverseMappingProfile (Team->TeamLightDto) [L149]
  └─ calls TeamRepository.ReadQuery [L58]
  └─ queries Team [L58]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L58]

