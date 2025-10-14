[web] GET /audit/find  (Dataverse.Api.Controllers.External.TeamsController.FindAudit)  [L46–L50]
  └─ calls TeamRepository.ReadQuery [L49]
  └─ queries Team [L49]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L49]

