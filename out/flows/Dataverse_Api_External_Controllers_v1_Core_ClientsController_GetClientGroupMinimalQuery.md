[web] GET /core/v1/clients/groups  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientGroupMinimalQuery)  [L187–L195]
  └─ calls ClientRepository.ReadQuery [L190]
  └─ queries Client [L190]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L190]

