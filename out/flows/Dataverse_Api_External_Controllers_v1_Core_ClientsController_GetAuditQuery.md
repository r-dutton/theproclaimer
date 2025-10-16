[web] GET /core/v1/clients/audits/{entityId:Guid}  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetAuditQuery)  [L256–L261] status=200
  └─ maps_to EntityAuditDto [L259]
  └─ calls ClientRepository.ReadQuery [L259]
  └─ query Client [L259]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L259]
      └─ ... (no implementation details available)

