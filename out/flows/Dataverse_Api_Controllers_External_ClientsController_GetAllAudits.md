[web] GET /audit  (Dataverse.Api.Controllers.External.ClientsController.GetAllAudits)  [L39–L44] status=200
  └─ calls ClientRepository.ReadQuery [L43]
  └─ query Client [L43]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L43]
      └─ ... (no implementation details available)

