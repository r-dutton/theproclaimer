[web] PUT /api/ui/tax-agents/{id}  (Dataverse.Api.Controllers.UI.Core.TaxAgentController.Update)  [L59–L69] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TaxAgentRepository.WriteQuery [L62]
  └─ write TaxAgent [L62]
    └─ reads_from TaxAgents
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ TaxAgent writes=1 reads=0

