[web] GET /api/ui/tax-agents/{id}  (Dataverse.Api.Controllers.UI.Core.TaxAgentController.Get)  [L40–L48] [auth=Authentication.UserPolicy]
  └─ maps_to TaxAgentDto [L43]
    └─ automapper.registration DataverseMappingProfile (TaxAgent->TaxAgentDto) [L252]
    └─ automapper.registration ExternalApiMappingProfile (TaxAgent->TaxAgentDto) [L79]
  └─ calls TaxAgentRepository.ReadQuery [L43]
  └─ queries TaxAgent [L43]
    └─ reads_from TaxAgents
  └─ uses_service IControlledRepository<TaxAgent>
    └─ method ReadQuery [L43]

