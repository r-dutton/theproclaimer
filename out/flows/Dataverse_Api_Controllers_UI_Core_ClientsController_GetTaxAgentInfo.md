[web] GET /api/ui/clients/{id:guid}/tax-agent-info  (Dataverse.Api.Controllers.UI.Core.ClientsController.GetTaxAgentInfo)  [L222–L227] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TaxAgentInfoDto [L225]
    └─ automapper.registration DataverseMappingProfile (Client->TaxAgentInfoDto) [L229]
  └─ calls ClientRepository.ReadQuery [L225]
  └─ query Client [L225]
  └─ uses_service IControlledRepository<Client>
    └─ method ReadQuery [L225]
      └─ ... (no implementation details available)

