[web] GET /api/internal/clients/{id:guid}/tax-agent-info  (Dataverse.Api.Controllers.Internal.Core.ClientsController.GetTaxAgentInfo)  [L97–L102] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TaxAgentInfoDto [L100]
    └─ automapper.registration DataverseMappingProfile (Client->TaxAgentInfoDto) [L229]
  └─ uses_client ClientRepository [L100]
  └─ impact_summary
    └─ clients 1
      └─ ClientRepository
    └─ mappings 1
      └─ TaxAgentInfoDto

