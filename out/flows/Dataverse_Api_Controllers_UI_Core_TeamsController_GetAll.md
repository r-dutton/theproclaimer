[web] GET /api/ui/teams/  (Dataverse.Api.Controllers.UI.Core.TeamsController.GetAll)  [L84–L104] [auth=Authentication.UserPolicy]
  └─ maps_to TeamLightDto [L98]
    └─ automapper.registration CirrusMappingProfile (Team->TeamLightDto) [L139]
    └─ automapper.registration WorkpapersMappingProfile (Team->TeamLightDto) [L188]
    └─ automapper.registration DataverseMappingProfile (Team->TeamLightDto) [L149]
  └─ calls TeamRepository.ReadQuery [L98]
  └─ queries Team [L98]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L98]
  └─ uses_service UserService
    └─ method IsInRole [L90]

