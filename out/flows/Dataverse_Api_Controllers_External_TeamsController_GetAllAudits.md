[web] GET /audit  (Dataverse.Api.Controllers.External.TeamsController.GetAllAudits)  [L39–L44] status=200
  └─ calls TeamRepository.ReadQuery [L43]
  └─ query Team [L43]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L43]
      └─ ... (no implementation details available)

