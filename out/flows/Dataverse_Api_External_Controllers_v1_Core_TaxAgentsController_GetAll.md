[web] GET /core/v1/tax-agents/  (Dataverse.Api.External.Controllers.v1.Core.TaxAgentsController.GetAll)  [L55–L79] status=200
  └─ maps_to TaxAgentDto [L70]
    └─ automapper.registration DataverseMappingProfile (TaxAgent->TaxAgentDto) [L253]
    └─ automapper.registration ExternalApiMappingProfile (TaxAgent->TaxAgentDto) [L79]
  └─ maps_to TaxAgentDto [L63]
    └─ automapper.registration DataverseMappingProfile (TaxAgent->TaxAgentDto) [L253]
    └─ automapper.registration ExternalApiMappingProfile (TaxAgent->TaxAgentDto) [L79]
  └─ calls TaxAgentRepository.ReadQuery [L63]
  └─ query TaxAgent [L63]
    └─ reads_from TaxAgents
  └─ uses_service IControlledRepository<TaxAgent>
    └─ method ReadQuery [L63]
      └─ ... (no implementation details available)

