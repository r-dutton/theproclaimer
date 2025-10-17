[web] GET /api/ui/tax-agents/{id}  (Dataverse.Api.Controllers.UI.Core.TaxAgentController.Get)  [L40–L48] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to TaxAgentDto [L43]
    └─ automapper.registration ExternalApiMappingProfile (TaxAgent->TaxAgentDto) [L79]
    └─ automapper.registration DataverseMappingProfile (TaxAgent->TaxAgentDto) [L253]
  └─ calls TaxAgentRepository.ReadQuery [L43]
  └─ query TaxAgent [L43]
    └─ reads_from TaxAgents
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TaxAgent writes=0 reads=1
    └─ mappings 1
      └─ TaxAgentDto

