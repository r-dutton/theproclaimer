[web] GET /core/v1/teams/  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.MinimalQuery)  [L68–L73] status=200
  └─ calls TeamRepository.ReadQuery [L71]
  └─ query Team [L71]
    └─ reads_from Teams
  └─ uses_service TeamRepository
    └─ method ReadQuery [L71]
      └─ implementation Dataverse.Data.Repository.Firm.TeamRepository.ReadQuery [L8-L40]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ Team writes=0 reads=1
    └─ services 1
      └─ TeamRepository

