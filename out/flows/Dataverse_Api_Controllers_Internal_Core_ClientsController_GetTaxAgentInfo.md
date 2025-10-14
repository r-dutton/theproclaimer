[web] GET /api/internal/clients/{id:guid}/tax-agent-info  (Dataverse.Api.Controllers.Internal.Core.ClientsController.GetTaxAgentInfo)  [L97–L102] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TaxAgentInfoDto [L100]
    └─ automapper.registration DataverseMappingProfile (Client->TaxAgentInfoDto) [L228]
  └─ calls ClientRepository.ReadQuery [L100]
  └─ queries Client [L100]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L100]

