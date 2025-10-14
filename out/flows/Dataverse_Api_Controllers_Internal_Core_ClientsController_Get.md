[web] GET /api/internal/clients/{id}  (Dataverse.Api.Controllers.Internal.Core.ClientsController.Get)  [L50–L56] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to ClientDto [L53]
    └─ automapper.registration DataverseMappingProfile (Client->ClientDto) [L164]
    └─ automapper.registration WorkpapersMappingProfile (Client->ClientDto) [L69]
    └─ automapper.registration CirrusMappingProfile (Client->ClientDto) [L146]
  └─ calls ClientRepository.ReadQuery [L53]
  └─ queries Client [L53]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L53]

