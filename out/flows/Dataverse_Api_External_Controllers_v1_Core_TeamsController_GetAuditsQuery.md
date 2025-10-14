[web] GET /core/v1/teams/audits  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.GetAuditsQuery)  [L97–L103]
  └─ maps_to EntityAuditDto [L100]
  └─ calls TeamRepository.ReadQuery [L100]
  └─ queries Team [L100]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L100]

