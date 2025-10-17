[web] GET /api/internal/clients/{id}  (Dataverse.Api.Controllers.Internal.Core.ClientsController.Get)  [L50–L56] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to ClientDto [L53]
    └─ automapper.registration DataverseMappingProfile (Client->ClientDto) [L165]
  └─ uses_client ClientRepository [L53]
  └─ impact_summary
    └─ clients 1
      └─ ClientRepository
    └─ mappings 1
      └─ ClientDto

