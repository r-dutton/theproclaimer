[web] GET /core/v1/clients/groups/include-children  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientGroupsWithChildrenQuery)  [L227–L235] status=200
  └─ calls ClientRepository.ReadQuery [L230]
  └─ query Client [L230]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L230]
      └─ ... (no implementation details available)

