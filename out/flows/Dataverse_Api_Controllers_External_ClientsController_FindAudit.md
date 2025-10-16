[web] GET /audit/find  (Dataverse.Api.Controllers.External.ClientsController.FindAudit)  [L46–L50] status=200
  └─ calls ClientRepository.ReadQuery [L49]
  └─ query Client [L49]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L49]
      └─ ... (no implementation details available)

