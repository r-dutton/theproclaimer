[web] GET /core/v1/clients/entities/detailed  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientEntityDetailedQuery)  [L113–L147] status=200
  └─ maps_to ClientEntityDto [L129]
    └─ automapper.registration ExternalApiMappingProfile (Client->ClientEntityDto) [L98]
  └─ uses_client ClientRepository [L129]
  └─ uses_client ClientRepository [L122]
  └─ impact_summary
    └─ clients 1
      └─ ClientRepository
    └─ mappings 1
      └─ ClientEntityDto

