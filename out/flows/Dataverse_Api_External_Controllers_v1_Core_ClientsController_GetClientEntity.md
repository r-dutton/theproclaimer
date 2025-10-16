[web] GET /core/v1/clients/entities/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientEntity)  [L73–L79] status=200
  └─ maps_to ClientEntityDto [L76]
    └─ automapper.registration ExternalApiMappingProfile (Client->ClientEntityDto) [L98]
  └─ uses_client ClientRepository [L76]
  └─ impact_summary
    └─ clients 1
      └─ ClientRepository
    └─ mappings 1
      └─ ClientEntityDto

