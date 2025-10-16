[web] GET /core/v1/teams/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.Get)  [L51–L56] status=200
  └─ maps_to TeamDto [L54]
    └─ automapper.registration ExternalApiMappingProfile (Team->TeamDto) [L75]
    └─ automapper.registration DataverseMappingProfile (Team->TeamDto) [L149]
  └─ calls TeamRepository.ReadQuery [L54]
  └─ query Team [L54]
    └─ reads_from Teams
  └─ uses_service TeamRepository
    └─ method ReadQuery [L54]
      └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Team writes=0 reads=1
    └─ services 1
      └─ TeamRepository
    └─ mappings 1
      └─ TeamDto

