[web] GET /core/v1/teams/  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.MinimalQuery)  [L68–L73] status=200
  └─ calls TeamRepository.ReadQuery [L71]
  └─ query Team [L71]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L71]
      └─ ... (no implementation details available)

