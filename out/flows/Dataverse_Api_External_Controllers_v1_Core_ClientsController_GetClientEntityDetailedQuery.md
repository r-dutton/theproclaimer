[web] GET /core/v1/clients/entities/detailed  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientEntityDetailedQuery)  [L113–L147] status=200
  └─ maps_to ClientEntityDto [L129]
    └─ automapper.registration ExternalApiMappingProfile (Client->ClientEntityDto) [L98]
  └─ calls ClientRepository.ReadQuery [L122]
  └─ query Client [L122]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L122]
      └─ ... (no implementation details available)

