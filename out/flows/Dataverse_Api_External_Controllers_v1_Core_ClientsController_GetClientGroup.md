[web] GET /core/v1/clients/groups/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.ClientsController.GetClientGroup)  [L155–L161]
  └─ maps_to ClientGroupDto [L158]
    └─ automapper.registration ExternalApiMappingProfile (Client->ClientGroupDto) [L104]
  └─ calls ClientRepository.ReadQuery [L158]
  └─ queries Client [L158]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L158]

