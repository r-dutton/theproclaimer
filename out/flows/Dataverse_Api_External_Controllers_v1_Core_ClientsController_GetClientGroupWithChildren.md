[web] GET /core/v1/clients/groups/{id:Guid}/include-children  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientGroupWithChildren)  [L169–L175] status=200
  └─ maps_to ClientGroupWithChildrenDto [L172]
    └─ automapper.registration ExternalApiMappingProfile (Client->ClientGroupWithChildrenDto) [L107]
  └─ uses_client ClientRepository [L172]
  └─ impact_summary
    └─ clients 1
      └─ ClientRepository
    └─ mappings 1
      └─ ClientGroupWithChildrenDto

