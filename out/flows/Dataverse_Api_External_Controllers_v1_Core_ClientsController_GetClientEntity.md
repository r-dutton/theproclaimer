[web] GET /core/v1/clients/entities/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientEntity)  [L73–L79] status=200
  └─ maps_to ClientEntityDto [L76]
    └─ automapper.registration ExternalApiMappingProfile (Client->ClientEntityDto) [L98]
  └─ calls ClientRepository.ReadQuery [L76]
  └─ query Client [L76]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L76]
      └─ ... (no implementation details available)

