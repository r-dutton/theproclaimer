[web] GET /api/ui/teams/  (Dataverse.Api.Controllers.UI.Core.TeamsController.GetAll)  [L84–L104] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TeamLightDto [L98]
    └─ automapper.registration CirrusMappingProfile (Team->TeamLightDto) [L139]
    └─ automapper.registration WorkpapersMappingProfile (Team->TeamLightDto) [L188]
    └─ automapper.registration DataverseMappingProfile (Team->TeamLightDto) [L150]
  └─ calls TeamRepository.ReadQuery [L98]
  └─ query Team [L98]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L98]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsInRole [L90]
      └─ implementation Dataverse.ApplicationService.Services.UserService.IsInRole [L15-L185]

