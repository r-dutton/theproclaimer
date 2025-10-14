[web] GET /core/v1/tax-agents/  (Dataverse.Api.External.Controllers.v1.Core.TaxAgentsController.GetAll)  [L55–L79]
  └─ maps_to TaxAgentDto [L70]
    └─ automapper.registration DataverseMappingProfile (TaxAgent->TaxAgentDto) [L252]
    └─ automapper.registration ExternalApiMappingProfile (TaxAgent->TaxAgentDto) [L79]
  └─ maps_to TaxAgentDto [L63]
    └─ automapper.registration DataverseMappingProfile (TaxAgent->TaxAgentDto) [L252]
    └─ automapper.registration ExternalApiMappingProfile (TaxAgent->TaxAgentDto) [L79]
  └─ calls TaxAgentRepository.ReadQuery [L63]
  └─ queries TaxAgent [L63]
    └─ reads_from TaxAgents
  └─ uses_service IControlledRepository<TaxAgent>
    └─ method ReadQuery [L63]

