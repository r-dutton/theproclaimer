[web] DELETE /api/ui/tax-agents/{id}  (Dataverse.Api.Controllers.UI.Core.TaxAgentController.Delete)  [L71–L81] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TaxAgentRepository (methods: Remove,WriteQuery) [L78]
  └─ delete TaxAgent [L78]
    └─ reads_from TaxAgents
  └─ write TaxAgent [L74]
    └─ reads_from TaxAgents
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ TaxAgent writes=2 reads=0

