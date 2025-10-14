[web] GET /core/v1/tax-agents/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.TaxAgentsController.Get)  [L41–L46]
  └─ maps_to TaxAgentDto [L44]
    └─ automapper.registration DataverseMappingProfile (TaxAgent->TaxAgentDto) [L252]
    └─ automapper.registration ExternalApiMappingProfile (TaxAgent->TaxAgentDto) [L79]
  └─ calls TaxAgentRepository.ReadQuery [L44]
  └─ queries TaxAgent [L44]
    └─ reads_from TaxAgents
  └─ uses_service IControlledRepository<TaxAgent>
    └─ method ReadQuery [L44]

