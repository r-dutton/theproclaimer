[web] GET /api/super/teams/  (Dataverse.Api.Controllers.Super.Core.TeamsController.GetAll)  [L30–L36] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TeamLightDto [L33]
    └─ automapper.registration DataverseMappingProfile (Team->TeamLightDto) [L150]
  └─ calls TeamRepository.ReadQuery [L33]
  └─ query Team [L33]
    └─ reads_from Teams
  └─ uses_service TeamRepository
    └─ method ReadQuery [L33]
      └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Team writes=0 reads=1
    └─ services 1
      └─ TeamRepository
    └─ mappings 1
      └─ TeamLightDto

