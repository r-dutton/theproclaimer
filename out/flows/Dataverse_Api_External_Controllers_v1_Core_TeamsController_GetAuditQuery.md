[web] GET /core/v1/teams/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Core.TeamsController.GetAuditQuery)  [L111–L116]
  └─ maps_to EntityAuditDto [L114]
  └─ calls TeamRepository.ReadQuery [L114]
  └─ queries Team [L114]
    └─ reads_from Teams
  └─ uses_service IControlledRepository<Team>
    └─ method ReadQuery [L114]

