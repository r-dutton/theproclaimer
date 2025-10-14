[web] GET /core/v1/clients/groups/include-children  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientGroupsWithChildrenQuery)  [L227–L235]
  └─ calls ClientRepository.ReadQuery [L230]
  └─ queries Client [L230]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L230]

