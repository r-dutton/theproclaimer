[web] GET /core/v1/teams/audits  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.GetAuditsQuery)  [L97–L103] status=200
  └─ maps_to EntityAuditDto [L100]
  └─ calls TeamRepository.ReadQuery [L100]
  └─ query Team [L100]
    └─ reads_from Teams
  └─ uses_service TeamRepository
    └─ method ReadQuery [L100]
      └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Team writes=0 reads=1
    └─ services 1
      └─ TeamRepository
    └─ mappings 1
      └─ EntityAuditDto

