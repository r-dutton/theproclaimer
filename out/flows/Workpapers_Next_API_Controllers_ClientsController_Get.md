[web] GET /api/clients/{id:guid}  (Workpapers.Next.API.Controllers.ClientsController.Get)  [L68–L76] status=200 [auth=AuthorizationPolicies.User]
  └─ maps_to ClientDto [L73]
    └─ automapper.registration WorkpapersMappingProfile (Client->ClientDto) [L69]
  └─ uses_client ClientRepository [L73]
  └─ impact_summary
    └─ clients 1
      └─ ClientRepository
    └─ mappings 1
      └─ ClientDto

