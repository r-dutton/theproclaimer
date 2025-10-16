[web] GET /api/ui/teams/{id}  (Dataverse.Api.Controllers.UI.Core.TeamsController.Get)  [L51–L57] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TeamDto [L54]
    └─ automapper.registration DataverseMappingProfile (Team->TeamDto) [L149]
    └─ automapper.registration ExternalApiMappingProfile (Team->TeamDto) [L75]
    └─ automapper.registration CirrusMappingProfile (Team->TeamDto) [L138]
    └─ automapper.registration WorkpapersMappingProfile (Team->TeamDto) [L190]
  └─ calls TeamRepository.ReadQuery [L54]
  └─ query Team [L54]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L54]
      └─ ... (no implementation details available)

