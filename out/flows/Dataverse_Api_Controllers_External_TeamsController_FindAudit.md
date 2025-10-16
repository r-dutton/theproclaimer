[web] GET /audit/find  (Dataverse.Api.Controllers.External.TeamsController.FindAudit)  [L46–L50] status=200
  └─ calls TeamRepository.ReadQuery [L49]
  └─ query Team [L49]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L49]
      └─ ... (no implementation details available)

