[web] GET /api/ui/clients/{id:guid}/tax-agent-info  (Dataverse.Api.Controllers.UI.Core.ClientsController.GetTaxAgentInfo)  [L222–L227] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TaxAgentInfoDto [L225]
    └─ automapper.registration DataverseMappingProfile (Client->TaxAgentInfoDto) [L229]
  └─ uses_client ClientRepository [L225]
  └─ impact_summary
    └─ clients 1
      └─ ClientRepository
    └─ mappings 1
      └─ TaxAgentInfoDto

