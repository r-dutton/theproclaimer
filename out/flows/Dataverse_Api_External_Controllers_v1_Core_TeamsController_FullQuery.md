[web] GET /core/v1/teams/detailed  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.FullQuery)  [L85–L90] status=200
  └─ calls TeamRepository.ReadQuery [L88]
  └─ query Team [L88]
    └─ reads_from Teams
  └─ uses_service TeamRepository
    └─ method ReadQuery [L88]
      └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Team writes=0 reads=1
    └─ services 1
      └─ TeamRepository

