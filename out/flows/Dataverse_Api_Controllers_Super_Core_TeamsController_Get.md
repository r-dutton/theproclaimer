[web] GET /api/super/teams/{id}  (Dataverse.Api.Controllers.Super.Core.TeamsController.Get)  [L38–L43] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TeamDto [L41]
    └─ automapper.registration ExternalApiMappingProfile (Team->TeamDto) [L75]
    └─ automapper.registration DataverseMappingProfile (Team->TeamDto) [L149]
  └─ calls TeamRepository.ReadQuery [L41]
  └─ query Team [L41]
    └─ reads_from Teams
  └─ uses_service TeamRepository
    └─ method ReadQuery [L41]
      └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Team writes=0 reads=1
    └─ services 1
      └─ TeamRepository
    └─ mappings 1
      └─ TeamDto

