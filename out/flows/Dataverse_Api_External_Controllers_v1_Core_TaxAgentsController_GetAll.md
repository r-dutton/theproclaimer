[web] GET /core/v1/tax-agents/  (Dataverse.Api.External.Controllers.v1.Core.TaxAgentsController.GetAll)  [L55–L79] status=200
  └─ maps_to TaxAgentDto [L70]
    └─ automapper.registration ExternalApiMappingProfile (TaxAgent->TaxAgentDto) [L79]
    └─ automapper.registration DataverseMappingProfile (TaxAgent->TaxAgentDto) [L253]
  └─ calls TaxAgentRepository.ReadQuery [L63]
  └─ query TaxAgent [L63]
    └─ reads_from TaxAgents
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TaxAgent writes=0 reads=1
    └─ mappings 1
      └─ TaxAgentDto

