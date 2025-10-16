[web] GET /core/v1/teams/audits  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.GetAuditsQuery)  [L97–L103] status=200
  └─ maps_to EntityAuditDto [L100]
  └─ calls TeamRepository.ReadQuery [L100]
  └─ query Team [L100]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L100]
      └─ ... (no implementation details available)

