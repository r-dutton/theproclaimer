[web] GET /api/internal/teams/{id}  (Dataverse.Api.Controllers.Internal.Core.TeamsController.Get)  [L48–L54] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TeamDto [L51]
    └─ automapper.registration ExternalApiMappingProfile (Team->TeamDto) [L75]
    └─ automapper.registration DataverseMappingProfile (Team->TeamDto) [L149]
  └─ calls TeamRepository.ReadQuery [L51]
  └─ query Team [L51]
    └─ reads_from Teams
  └─ uses_service TeamRepository
    └─ method ReadQuery [L51]
      └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Team writes=0 reads=1
    └─ services 1
      └─ TeamRepository
    └─ mappings 1
      └─ TeamDto

