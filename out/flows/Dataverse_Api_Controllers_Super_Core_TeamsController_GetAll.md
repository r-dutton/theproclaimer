[web] GET /api/super/teams/  (Dataverse.Api.Controllers.Super.Core.TeamsController.GetAll)  [L30–L36] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TeamLightDto [L33]
    └─ automapper.registration CirrusMappingProfile (Team->TeamLightDto) [L139]
    └─ automapper.registration WorkpapersMappingProfile (Team->TeamLightDto) [L188]
    └─ automapper.registration DataverseMappingProfile (Team->TeamLightDto) [L150]
  └─ calls TeamRepository.ReadQuery [L33]
  └─ query Team [L33]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L33]
      └─ ... (no implementation details available)

