[web] GET /api/internal/teams/{id}  (Dataverse.Api.Controllers.Internal.Core.TeamsController.Get)  [L48–L54] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TeamDto [L51]
    └─ automapper.registration DataverseMappingProfile (Team->TeamDto) [L148]
    └─ automapper.registration ExternalApiMappingProfile (Team->TeamDto) [L75]
    └─ automapper.registration CirrusMappingProfile (Team->TeamDto) [L138]
    └─ automapper.registration WorkpapersMappingProfile (Team->TeamDto) [L190]
  └─ calls TeamRepository.ReadQuery [L51]
  └─ queries Team [L51]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L51]

