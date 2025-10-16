[web] GET /core/v1/clients/groups/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientGroup)  [L155–L161] status=200
  └─ maps_to ClientGroupDto [L158]
    └─ automapper.registration ExternalApiMappingProfile (Client->ClientGroupDto) [L104]
  └─ uses_client ClientRepository [L158]
  └─ impact_summary
    └─ clients 1
      └─ ClientRepository
    └─ mappings 1
      └─ ClientGroupDto

