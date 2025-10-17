[web] POST /api/ui/tax-agents/  (Dataverse.Api.Controllers.UI.Core.TaxAgentController.Create)  [L50–L57] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TaxAgentRepository.Add [L54]
  └─ insert TaxAgent [L54]
    └─ reads_from TaxAgents
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ TaxAgent writes=1 reads=0

