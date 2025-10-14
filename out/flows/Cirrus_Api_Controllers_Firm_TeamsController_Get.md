[web] GET /api/teams/{id}  (Cirrus.Api.Controllers.Firm.TeamsController.Get)  [L40–L46] [auth=user]
  └─ maps_to TeamDto [L43]
    └─ automapper.registration DataverseMappingProfile (Team->TeamDto) [L148]
    └─ automapper.registration ExternalApiMappingProfile (Team->TeamDto) [L75]
    └─ automapper.registration CirrusMappingProfile (Team->TeamDto) [L138]
    └─ automapper.registration WorkpapersMappingProfile (Team->TeamDto) [L190]
  └─ calls TeamRepository.ReadQuery [L43]
  └─ queries Team [L43]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L43]

