[web] GET /core/v1/clients/groups/{id:Guid}/include-children  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientGroupWithChildren)  [L169–L175] status=200
  └─ maps_to ClientGroupWithChildrenDto [L172]
    └─ automapper.registration ExternalApiMappingProfile (Client->ClientGroupWithChildrenDto) [L107]
  └─ calls ClientRepository.ReadQuery [L172]
  └─ query Client [L172]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L172]
      └─ ... (no implementation details available)

