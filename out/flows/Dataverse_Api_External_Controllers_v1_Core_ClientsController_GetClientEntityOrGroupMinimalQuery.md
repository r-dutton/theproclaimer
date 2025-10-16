[web] GET /core/v1/clients/  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientEntityOrGroupMinimalQuery)  [L58–L65] status=200
  └─ calls ClientRepository.ReadQuery [L61]
  └─ query Client [L61]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L61]
      └─ ... (no implementation details available)

