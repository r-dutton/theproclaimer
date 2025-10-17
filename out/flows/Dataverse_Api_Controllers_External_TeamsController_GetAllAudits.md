[web] GET /audit  (Dataverse.Api.Controllers.External.TeamsController.GetAllAudits)  [L39–L44] status=200
  └─ calls TeamRepository.ReadQuery [L43]
  └─ query Team [L43]
    └─ reads_from Teams
  └─ uses_service TeamRepository
    └─ method ReadQuery [L43]
      └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Team writes=0 reads=1
    └─ services 1
      └─ TeamRepository

