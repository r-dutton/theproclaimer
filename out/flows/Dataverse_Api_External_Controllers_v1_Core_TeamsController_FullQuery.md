[web] GET /core/v1/teams/detailed  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.FullQuery)  [L85–L90]
  └─ calls TeamRepository.ReadQuery [L88]
  └─ queries Team [L88]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L88]

