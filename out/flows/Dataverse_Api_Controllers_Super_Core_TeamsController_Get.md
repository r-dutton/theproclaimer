[web] GET /api/super/teams/{id}  (Dataverse.Api.Controllers.Super.Core.TeamsController.Get)  [L38–L43] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TeamDto [L41]
    └─ automapper.registration DataverseMappingProfile (Team->TeamDto) [L149]
    └─ automapper.registration ExternalApiMappingProfile (Team->TeamDto) [L75]
    └─ automapper.registration CirrusMappingProfile (Team->TeamDto) [L138]
    └─ automapper.registration WorkpapersMappingProfile (Team->TeamDto) [L190]
  └─ calls TeamRepository.ReadQuery [L41]
  └─ query Team [L41]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L41]
      └─ ... (no implementation details available)

