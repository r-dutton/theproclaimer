[web] GET /api/clients/{id:guid}  (Workpapers.Next.API.Controllers.ClientsController.Get)  [L68–L76] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to ClientDto [L73]
    └─ automapper.registration DataverseMappingProfile (Client->ClientDto) [L165]
    └─ automapper.registration WorkpapersMappingProfile (Client->ClientDto) [L69]
    └─ automapper.registration CirrusMappingProfile (Client->ClientDto) [L146]
  └─ calls ClientRepository.ReadQuery [L73]
  └─ query Client [L73]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L73]
      └─ ... (no implementation details available)

