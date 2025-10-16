[web] GET /core/v1/clients/audits  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetAuditsQuery)  [L242–L248] status=200
  └─ maps_to EntityAuditDto [L245]
  └─ calls ClientRepository.ReadQuery [L245]
  └─ query Client [L245]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L245]
      └─ ... (no implementation details available)

