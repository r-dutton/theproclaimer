[web] GET /core/v1/teams/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.GetAuditQuery)  [L111–L116] status=200
  └─ maps_to EntityAuditDto [L114]
  └─ calls TeamRepository.ReadQuery [L114]
  └─ query Team [L114]
    └─ reads_from Teams
  └─ uses_service TeamRepository
    └─ method ReadQuery [L114]
      └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Team writes=0 reads=1
    └─ services 1
      └─ TeamRepository
    └─ mappings 1
      └─ EntityAuditDto

