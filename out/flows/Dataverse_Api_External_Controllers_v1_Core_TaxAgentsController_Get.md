[web] GET /core/v1/tax-agents/{id:Guid}  (Dataverse.Api.External.Controllers.v1.Core.TaxAgentsController.Get)  [L41–L46] status=200
  └─ maps_to TaxAgentDto [L44]
    └─ automapper.registration ExternalApiMappingProfile (TaxAgent->TaxAgentDto) [L79]
    └─ automapper.registration DataverseMappingProfile (TaxAgent->TaxAgentDto) [L253]
  └─ calls TaxAgentRepository.ReadQuery [L44]
  └─ query TaxAgent [L44]
    └─ reads_from TaxAgents
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TaxAgent writes=0 reads=1
    └─ mappings 1
      └─ TaxAgentDto

