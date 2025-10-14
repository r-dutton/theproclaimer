[web] GET /core/v1/teams/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.Get)  [L51–L56]
  └─ maps_to TeamDto [L54]
    └─ automapper.registration DataverseMappingProfile (Team->TeamDto) [L148]
    └─ automapper.registration ExternalApiMappingProfile (Team->TeamDto) [L75]
    └─ automapper.registration CirrusMappingProfile (Team->TeamDto) [L138]
    └─ automapper.registration WorkpapersMappingProfile (Team->TeamDto) [L190]
  └─ calls TeamRepository.ReadQuery [L54]
  └─ queries Team [L54]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L54]

