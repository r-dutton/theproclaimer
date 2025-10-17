[web] GET /audit/find  (Dataverse.Api.Controllers.External.TeamsController.FindAudit)  [L46–L50] status=200
  └─ calls TeamRepository.ReadQuery [L49]
  └─ query Team [L49]
    └─ reads_from Teams
  └─ uses_service TeamRepository
    └─ method ReadQuery [L49]
      └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Team writes=0 reads=1
    └─ services 1
      └─ TeamRepository

